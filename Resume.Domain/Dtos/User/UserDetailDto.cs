namespace Resume.Domain.Dtos.User
{
    public class UserDetailDto
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BirthPlace { get; set; }
        public string BirthDate { get; set; }
        public string Description { get; set; }
        public string? Avatar { get; set; }
    }
}
