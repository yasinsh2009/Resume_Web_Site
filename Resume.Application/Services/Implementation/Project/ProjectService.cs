using MarketPlace.Application.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resume.Application.Services.Interface.Project;
using Resume.Domain.Dtos.Project.Project;
using Resume.Domain.Dtos.Project.ProjectCategory;
using Resume.Domain.Entities.Project;
using Resume.Domain.Repository;

namespace Resume.Application.Services.Implementation.Project
{
    public class ProjectService : IProjectService
    {
        #region Fields

        private readonly IGenericRepository<Domain.Entities.Project.Project> _projectRepository;
        private readonly IGenericRepository<ProjectCategory> _projectCategoryRepository;

        #endregion

        #region Constructor

        public ProjectService(IGenericRepository<Domain.Entities.Project.Project> projectRepository, IGenericRepository<ProjectCategory> projectCategoryRepository)
        {
            _projectRepository = projectRepository;
            _projectCategoryRepository = projectCategoryRepository;
        }

        #endregion

        #region Project - Category

        #region Filter - Project - Categories

        public async Task<List<FilterProjectCategoriesDto>> GetAllProjectCategories()
        {
            return await _projectCategoryRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Select(x => new FilterProjectCategoriesDto
                {
                    Id = x.Id,
                    Image = x.Image,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        #endregion

        #region Create - Project - Category

        public async Task<CreateProjectCategoryResult> CreateProjectCategory(CreateProjectCategoryDto command, IFormFile? image)
        {
            try
            {
                string fileName = null;
                if (image != null)
                {
                    var uploadDirectory =
                        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "projectCategory");
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    fileName = $"{Guid.NewGuid()}{Path.GetExtension(image?.FileName)}";
                    var filePath = Path.Combine(uploadDirectory, fileName);

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                }

                var newProjectCategory = new ProjectCategory(command.Title, command.Description, fileName);

                await _projectCategoryRepository.AddEntity(newProjectCategory);
                await _projectCategoryRepository.SaveChanges();

                return CreateProjectCategoryResult.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating project category : {ex.Message}");
                return CreateProjectCategoryResult.Failed;
            }
        }

        #endregion

        #region Edit - Project - Category

        public async Task<EditProjectCategoryDto> GetProjectCategoryForEdit(long id)
        {
            var projectCategory = await _projectCategoryRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (projectCategory == null)
            {
                return new EditProjectCategoryDto
                {
                    Title = null,
                    Description = null,
                };
            }

            return new EditProjectCategoryDto
            {
                Title = projectCategory.Title,
                Description = projectCategory.Description,
            };
        }

        public async Task<EditProjectCategoryResult> EditProjectCategory(EditProjectCategoryDto command, IFormFile? image)
        {
            var existingProjectCategory = await _projectCategoryRepository
                .GetQuery()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == command.Id);

            if (existingProjectCategory == null)
            {
                return EditProjectCategoryResult.Failed("اطلاعات مورد نظر یافت نشد.");
            }

            if (image == null || image.Length == 0)
            {
                return EditProjectCategoryResult.Failed("لطفا تصویر را اصلاح کنید.");
            }

            var uploadDirectory =
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "projectCategory");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(uploadDirectory, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            existingProjectCategory.Title = command.Title;
            existingProjectCategory.Description = command.Description;
            existingProjectCategory.Image = fileName;

            _projectCategoryRepository.UpdateEntity(existingProjectCategory);
            await _projectCategoryRepository.SaveChanges();

            return EditProjectCategoryResult.Success();
        }

        #endregion

        #region Get - Project - Categories

        public async Task<object> GetProjectCategories()
        {
            return await _projectCategoryRepository
                .GetQuery()
                .Where(x => !x.IsDelete)
                .Select(x => new { x.Id, x.Title })
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        #endregion

        #endregion

        #region Project

        #region Filter - Projects

        public async Task<List<FilterProjectsDto>> GetAllProjects()
        {
            return await _projectRepository
                .GetQuery()
                .AsQueryable()
                .Where(x => !x.IsDelete)
                .Include(x => x.ProjectCategory)
                .Select(x => new FilterProjectsDto
                {
                    Id = x.Id,
                    ProjectCategoryTitle = x.ProjectCategory.Title,
                    ProjectTitle = x.ProjectTitle,
                    ProjectImage = x.ProjectImage,
                    Description = x.Description,
                    CreateDate = x.CreateDate.ToStringShamsiDate()
                }).OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        #endregion

        #region Create - Project

        public async Task<CreateProjectResult> CreateProject(CreateProjectDto command, IFormFile? projectImage)
        {
            if (command == null)
            {
                return CreateProjectResult.Failed("داده های ورودی نامعتبر است.");
            }

            try
            {
                string fileName = null;
                if (projectImage is not null)
                {
                    var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", "project");
                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    fileName = $"{Guid.NewGuid()}{Path.GetExtension(projectImage.FileName)}";
                    var filePath = Path.Combine(uploadDirectory, fileName);

                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await projectImage.CopyToAsync(stream);
                }

                var newProject = new Domain.Entities.Project.Project
                {
                    ProjectCategoryId = command.ProjectCategoryId,
                    ProjectTitle = command.ProjectTitle,
                    Description = command.Description,
                    ProjectImage = fileName
                };

                await _projectRepository.AddEntity(newProject);
                await _projectRepository.SaveChanges();

                return CreateProjectResult.Success();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Creating project ; {ex.Message}");
                return CreateProjectResult.Failed("خطایی در ایجاد پروژه رخ داد.");
            }
        }

        #endregion

        #region Edit - Project

        public async Task<EditProjectDto> GetProjectForEdit(long id)
        {
            var project = await _projectRepository
                .GetQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
            {
                return new EditProjectDto
                {
                    ProjectTitle = null,
                    Description = null
                };
            }

            return new EditProjectDto
            {
                Id = id,
                ProjectTitle = project.ProjectTitle,
                Description = project.Description,
            };
        }

        public async Task<EditProjectResult> EditProject(EditProjectDto command, IFormFile? image)
        {
            var existingProject = await _projectRepository
                .GetQuery()
                .FirstOrDefaultAsync(x => x.Id == command.Id);

            if (existingProject == null)
            {
                return EditProjectResult.NotFound("در یافتن نمونه کار مورد نظر خطایی رخ داد.");
            }

            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "content", "project");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image?.FileName)}";
            var filePath = Path.Combine(uploadDirectory, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            existingProject.ProjectCategoryId = command.Id;
            existingProject.ProjectTitle = command.ProjectTitle;
            existingProject.Description = command.Description;
            existingProject.ProjectImage = fileName;

            _projectRepository.UpdateEntity(existingProject);
            await _projectRepository.SaveChanges();

            return EditProjectResult.Success();
        }

        #endregion

        #endregion

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_projectRepository != null && _projectCategoryRepository != null)
            {
                await _projectRepository.DisposeAsync();
                await _projectCategoryRepository.DisposeAsync();
            }
        }

        #endregion
    }
}
