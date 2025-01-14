namespace Person.API.Repositories;

using Person.API.Models;

public interface IPersonRepository
{
    Task Create(PersonModel person);
    Task<PersonModel?> FindById(Guid id);
    Task<PersonModel?> FindByName(string name);
    Task<IEnumerable<PersonModel>> FindAll();
    Task Update(PersonModel person);
}