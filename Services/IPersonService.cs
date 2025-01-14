using Person.API.Models;

namespace Person.API.Services;

public interface IPersonService
{
    Task<PersonModel> Create(string name);
    Task<IEnumerable<PersonModel>> FindAll();
    Task<PersonModel?> FindById(Guid id);
    Task<PersonModel?> Update(Guid id, string name);
    Task<PersonModel?> SetInactiveAsync(Guid id);
}