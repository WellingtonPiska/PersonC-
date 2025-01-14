namespace Person.API.Repositories;

using Microsoft.EntityFrameworkCore;
using Person.API.Data;
using Person.API.Models;

public class PersonRepository : IPersonRepository
{
    private readonly PersonContext _context;

    public PersonRepository(PersonContext context)
    {
        _context = context;
    }

    public async Task Create(PersonModel person)
    {
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();
        
    }

    public async Task<PersonModel?> FindById(Guid id)
    {
        return await _context.People.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PersonModel?> FindByName(string name)
    {
        return await _context.People.FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<PersonModel>> FindAll()
    {
        return await _context.People.ToListAsync();
    }

    public async Task Update(PersonModel person)
    {
        _context.People.Update(person);
        await _context.SaveChangesAsync();
    }
}