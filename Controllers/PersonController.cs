using Microsoft.AspNetCore.Mvc;
using Person.API.Exceptions;
using Person.API.Models;
using Person.API.Services;

namespace Person.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonModel>>> FindAll()
        {
            var people = await _personService.FindAll();
            return Ok(people);
        }

        // GET: api/person/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PersonModel>> FindById(Guid id)
        {
            var person = await _personService.FindById(id);
            if (person == null)
                throw new NotFoundException("Pessoa não encontrada");

            return Ok(person);
        }

        // POST: api/person
        [HttpPost]
        public async Task<ActionResult<PersonModel>> Create(PersonRequest request)
        {
            try
            {
                var newPerson = await _personService.Create(request.name);
                return CreatedAtAction(nameof(FindById), new { id = newPerson.Id }, newPerson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/person/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PersonModel>> Update(Guid id, PersonRequest request)
        {
            var updatedPerson = await _personService.Update(id, request.name);
            if (updatedPerson == null)
                throw new NotFoundException("Pessoa não encontrada");

            return Ok(updatedPerson);
        }

        // DELETE: api/person/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PersonModel>> Delete(Guid id)
        {
            var person = await _personService.SetInactiveAsync(id);
            if (person == null)
                throw new NotFoundException("Pessoa não encontrada");

            return Ok(person);
        }
    }
}
