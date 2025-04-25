using Microsoft.AspNetCore.Http;
using Resume.Domain.Dtos.Project.Project;
using Resume.Domain.Dtos.Project.ProjectCategory;

namespace Resume.Application.Services.Interface.Project;

public interface IProjectService : IAsyncDisposable
{
    #region Project

    Task<List<FilterProjectsDto>> GetAllProjects();
    Task<CreateProjectResult> CreateProject(CreateProjectDto command, IFormFile projectImage);
    Task<EditProjectDto> GetProjectForEdit(long id);
    Task<EditProjectResult> EditProject(EditProjectDto command);

    #endregion

    #region Project - Category

    Task<List<FilterProjectCategoriesDto>> GetAllProjectCategories();
    Task<CreateProjectCategoryResult> CreateProjectCategory(CreateProjectCategoryDto command, IFormFile? image);
    Task<EditProjectCategoryDto> GetProjectCategoryForEdit(long id);
    Task<EditProjectCategoryResult> EditProjectCategory(EditProjectCategoryDto command, IFormFile? image);
    Task<List<GetAllProjectCategoriesDto>> GetProjectCategories();

    #endregion
}