using Application.Interfaces;
using Application.SearchParams;
using Application.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService ContactService)
        {
            _contactService = ContactService;
        }

        [HttpGet()]
        public Task<IEnumerable<AdminContactResponse>> SearchContacts([FromQuery] AdminContactQueryParam parametroBusca)
        {
            return _contactService.SearchContacts(parametroBusca);
        }

        [HttpGet("{id:int}")]
        public Task<AdminContactResponse> ObterPorId([FromRoute] int id)
        {
            return _contactService.GetContact(id);
        }

        [HttpPost()]
        public async Task<ActionResult<AdminContactResponse>> Adicionar([FromBody] AdminContactRequest model)
        {
            var created = await _contactService.AddContact(model);
            return CreatedAtAction(nameof(ObterPorId), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public Task<AdminContactResponse> Atualizar([FromRoute] int id, [FromBody] AdminContactRequest model)
        {
            return _contactService.UpdateContact(id, model);
        }

        [HttpDelete("{id:int}")]
        public Task Remover([FromRoute] int id)
        {
            return _contactService.DeleteContact(id);
        }
    }
}
