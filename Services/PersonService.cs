using System.Data;
using Person.API.Models;
using Person.API.Repositories;

namespace Person.API.Services;


public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<PersonModel> Create(string name)
    {
        var existingPerson = await _personRepository.FindByName(name);
        if (existingPerson != null)
            throw new DuplicateNameException($"Já existe uma pessoa com o nome '{name}'.");

        var newPerson = new PersonModel(name);
        await _personRepository.Create(newPerson);

        return newPerson;
    }

    public async Task<IEnumerable<PersonModel>> FindAll()
    {
        return await _personRepository.FindAll();
    }

    public async Task<PersonModel?> FindById(Guid id)
    {
        return await _personRepository.FindById(id);
    }

    public async Task<PersonModel?> Update(Guid id, string name)
    {
        var person = await _personRepository.FindById(id);
        if (person == null)
            return null;
        
        var existingPerson = await _personRepository.FindByName(name);
        if (existingPerson != null && existingPerson.Id != id)
        {
            throw new DuplicateNameException($"Já existe uma pessoa com o nome '{name}'.");
        }

        person.ChangeName(name);
        await _personRepository.Update(person);
        return person;
    }

    public async Task<PersonModel?> SetInactiveAsync(Guid id)
    {
        var person = await _personRepository.FindById(id);
        if (person == null)
            return null;

        person.SetInactive();
        await _personRepository.Update(person);
        return person;
    }
}