using MarketPlace.Application.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.User;
using Resume.Application.Tools;
using Resume.Domain.Dtos.User;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.User
{
    public class UserService : IUserService
    {

        #region Fields

        private readonly IGenericRepository<Domain.Entities.User.User> _userRepository;

        #endregion

        #region Constructor

        public UserService(IGenericRepository<Domain.Entities.User.User> userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Duplicate - User

        public async Task<bool> IsUserExistByPn(string phoneNumber)
        {
            return await _userRepository.GetQuery().AsQueryable().AnyAsync(x => x.PhoneNumber == phoneNumber);
        }

        #endregion

        #region Filter - Users

        public async Task<List<FilterUserDto>> GetAllUsers(FilterUserDto filter)
        {
            var query = _userRepository.GetQuery().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.FullName))
            {
                query = query.Where(x => EF.Functions.Like(x.FullName, $"%{filter.FullName}%"));
            }

            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                query = query.Where(x => EF.Functions.Like(x.PhoneNumber, $"%{filter.PhoneNumber}%"));
            }

            var allUsers = query.Select(x => new FilterUserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                Email = x.EmailAddress,
                IsBlock = x.IsBlock,
                Avatar = x.Avatar,
                CreateDate = x.CreateDate.ToStringShamsiDate()
            }).ToListAsync();

            return await allUsers;
        }

        #endregion

        #region User - Detail

        public async Task<UserDetailDto> GetUserDetail()
        {
            var user = await _userRepository.GetQuery().AsQueryable().FirstOrDefaultAsync(x => !x.IsBlock);

            return new UserDetailDto
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.EmailAddress,
                BirthPlace = user.BirthPlace,
                BirthDate = user.BirthDate,
                Description = user.Description,
                Avatar = user.Avatar
            };
        }

        #endregion

        #region Create - User

        public async Task<CreateUserResult> CreateUser(RegisterUserDto user, IFormFile avatarImage)
        {
            // بررسی وجود شماره تلفن کاربر
            if (await IsUserExistByPn(user.PhoneNumber))
            {
                return CreateUserResult.DuplicatePhoneNumber;
            }

            string? fileName = null;

            // مدیریت تصویر آواتار (در صورت وجود)
            if (avatarImage != null && avatarImage.Length > 0)
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "user");

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                fileName = $"{Guid.NewGuid()}{Path.GetExtension(avatarImage.FileName)}";
                var filePath = Path.Combine(uploadDirectory, fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await avatarImage.CopyToAsync(stream);
            }

            // تولید Salt و رمزگذاری رمز عبور
            var salt = NewPasswordHasher.GenerateSalt();
            var hashedPassword = NewPasswordHasher.HashPassword(user.Password, salt);

            // ایجاد شی کاربر جدید
            var newUser = new Domain.Entities.User.User(user.FullName, user.PhoneNumber, user.EmailAddress,
                user.BirthPlace, user.BirthDate, user.Description, hashedPassword, hashedPassword, salt, user.IsBlock,
                fileName);

            try
            {
                // افزودن و ذخیره کاربر جدید در پایگاه داده
                await _userRepository.AddEntity(newUser);
                await _userRepository.SaveChanges();
                return CreateUserResult.Success;
            }
            catch (Exception ex)
            {
                // مدیریت خطا در عملیات ذخیره
                // می‌توانید لاگ‌گیری کنید یا اقدام مناسب انجام دهید
                Console.WriteLine($"Error creating user: {ex.Message}");
                return CreateUserResult.Error;
            }
        }

        #endregion

        #region Edit - User

        public async Task<UpdateUserDto> GetForEditUser(long Id)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                return new UpdateUserDto();
            }

            return new UpdateUserDto
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress,
                BirthPlace = user.BirthPlace,
                BirthDate = user.BirthDate,
                Description = user.Description,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
                PasswordSalt = user.PasswordSalt,
                IsBlock = user.IsBlock,
                Avatar = user.Avatar,
            };

        }

        async Task<UpdateUserResult> IUserService.EditUser(UpdateUserDto user, IFormFile avatarImage)
        {
            // دریافت کاربر از دیتابیس
            var existingUser = await _userRepository
                .GetQuery()
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser == null)
            {
                return UpdateUserResult.NotFoundUser;
            }

            // مدیریت تصویر آواتار
            if (avatarImage != null && avatarImage.Length > 0)
            {
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "user");

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(avatarImage.FileName)}";
                var filePath = Path.Combine(uploadDirectory, fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await avatarImage.CopyToAsync(stream);
                }

                existingUser.Avatar = fileName;
            }

            // بررسی تغییر رمز عبور (در صورت لزوم)
            if (!string.IsNullOrEmpty(user.Password))
            {
                var salt = NewPasswordHasher.GenerateSalt();
                var hashedPassword = NewPasswordHasher.HashPassword(user.Password, salt);

                existingUser.Password = hashedPassword;
                existingUser.ConfirmPassword = hashedPassword;
                existingUser.PasswordSalt = salt;
            }

            // بروزرسانی سایر اطلاعات کاربر
            existingUser.FullName = user.FullName;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.EmailAddress = user.EmailAddress;
            existingUser.BirthPlace = user.BirthPlace;
            existingUser.BirthDate = user.BirthDate;
            existingUser.Description = user.Description;
            existingUser.IsBlock = user.IsBlock;
            existingUser.UpdateDate = DateTime.UtcNow;

            // ذخیره تغییرات
            _userRepository.UpdateEntity(existingUser);
            await _userRepository.SaveChanges();

            return UpdateUserResult.Success;
        }

        #endregion

        #region Block - & - Unblock - User

        public async Task<bool> BlockUser(long Id)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable().
                FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                return false;
            }

            user.IsBlock = true;

            _userRepository.UpdateEntity(user);
            await _userRepository.SaveChanges();

            return true;
        }

        public async Task<bool> UnblockUser(long Id)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable().
                FirstOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                return false;
            }

            user.IsBlock = false;

            _userRepository.UpdateEntity(user);
            await _userRepository.SaveChanges();

            return false;
        }

        #endregion

        #region User - Login

        public async Task<UserLoginResult> UserLogin(LoginDto login)
        {
            var storedSalt = await GetStoredSalt(login.Identifier);
            var storedHashedPassword = await GetStoredPassword(login.Identifier);
            var hashedPassword = NewPasswordHasher.HashPassword(login.Password, storedSalt);
            var isValidPassword = NewPasswordHasher.CompareHashes(hashedPassword, storedHashedPassword);

            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync
                (
                x => x.PhoneNumber == login.Identifier ||
                x.EmailAddress == login.Identifier &&
                x.Password == hashedPassword
                );

            if (user == null)
            {
                return UserLoginResult.UserNotFound;
            }

            return isValidPassword ? UserLoginResult.Success : UserLoginResult.Error;
        }

        public async Task<Domain.Entities.User.User> GetUserByPn(string phoneNumber)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            return new Domain.Entities.User.User
            {
                FullName = user.FullName,
                PhoneNumber = phoneNumber,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                Avatar = user.Avatar
            };
        }

        public async Task<Domain.Entities.User.User> GetUserByEa(string emailAddress)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);

            return new Domain.Entities.User.User
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = emailAddress,
                Password = user.Password,
                Avatar = user.Avatar
            };
        }

        public async Task<UserDetailDto> GetUserById(long Id)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .SingleOrDefaultAsync(x => x.Id == Id);

            if (user == null)
            {
                return null;
            }

            return new UserDetailDto
            {
                Id = user.Id,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                BirthPlace = user.BirthPlace,
                Description = user.Description,
                Email = user.EmailAddress,
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<string> GetStoredSalt(string phoneNumber)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            if (user == null)
            {
                return string.Empty;
            }

            return user.PasswordSalt;


        }

        public async Task<string> GetStoredPassword(string phoneNumber)
        {
            var user = await _userRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            if (user == null)
            {
                return string.Empty;
            }

            return user.Password;
        }

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_userRepository != null)
            {
                await _userRepository.DisposeAsync();
            }
        }

        #endregion

    }
}