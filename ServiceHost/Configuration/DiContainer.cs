using Resume.Application.Services.Implementation.Blog;
using Resume.Application.Services.Implementation.Contact;
using Resume.Application.Services.Implementation.Education;
using Resume.Application.Services.Implementation.Experience;
using Resume.Application.Services.Implementation.Project;
using Resume.Application.Services.Implementation.Skill;
using Resume.Application.Services.Implementation.User;
using Resume.Application.Services.Interface.Blog;
using Resume.Application.Services.Interface.Contact;
using Resume.Application.Services.Interface.Education;
using Resume.Application.Services.Interface.Experience;
using Resume.Application.Services.Interface.Project;
using Resume.Application.Services.Interface.Skill;
using Resume.Application.Services.Interface.User;
using Resume.Domain.Repository;

namespace ServiceHost.Configuration
{
    public static class DiContainer
    {
        public static void RegisterService(this IServiceCollection services)
        {
            #region Repaositories

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion

            #region General - Services

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEducationService, EducationService>();
            services.AddTransient<IExperienceService, ExperienceService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IBlogService, BlogService>();

            #endregion
        }
    }
}
