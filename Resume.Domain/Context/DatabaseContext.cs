using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.Blog;
using Resume.Domain.Entities.Contact;
using Resume.Domain.Entities.Project;
using Resume.Domain.Entities.Resume.Education;
using Resume.Domain.Entities.Resume.Experience;
using Resume.Domain.Entities.Resume.Skill;
using Resume.Domain.Entities.User;

namespace Resume.Domain.Context
{
    public class DatabaseContext : DbContext
    {
        #region Constructor

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion

        #region User

        public DbSet<User> Users { get; set; }

        #endregion

        #region Resume

        #region Educations

        public DbSet<Education> Educations { get; set; }

        #endregion

        #region Experiences

        public DbSet<Experience> Experiences { get; set; }

        #endregion

        #region Skills

        public DbSet<Skill> Skills { get; set; }

        #endregion

        #endregion

        #region Project

        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<Project> Projects { get; set; }

        #endregion

        #region Conatct

        public DbSet<Contact> Contacts { get; set; }

        #endregion

        #region Blog

        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Article> Articles { get; set; }

        #endregion

        #region On - Model - Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Cascade;
            }
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
