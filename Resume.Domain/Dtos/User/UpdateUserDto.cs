namespace Resume.Domain.Dtos.User
{
    public class UpdateUserDto : RegisterUserDto
    {
        public long Id { get; set; }
    }

    #region Update - User - Result

    public enum UpdateUserResult
    {
        Success,
        Error,
        NotFoundUser
    }

    #endregion
}
