using Application.Interfaces;
using Application.SearchParams;
using Application.Services;
using Application.ViewModels.Contact;
using Application.ViewModels.PhoneContact;
using Application.ViewModels.PhoneContactType;
using Domain.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhonebookController : ControllerBase
    {
        private readonly IPhonebookService _agendaService;

        public PhonebookController(IPhonebookService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpGet("phone-types")]
        public IEnumerable<PhoneContactTypeResponse> PhoneTypes()
        {
            return _agendaService.PhoneContactTypes();
        }

        [HttpGet("contacts")]
        public Task<PaginationResult<ContactResponse>> SearchContacts([FromQuery] ContactQueryParam queryParams)
        {
            return _agendaService.SaerchContacts(queryParams);
        }

        [HttpGet("phones")]
        public Task<IEnumerable<PhoneContactResponse>> SearchPhones([FromQuery] ContactQueryParam queryParams)
        {
            return _agendaService.SearchPhones(queryParams);
        }

        [HttpGet("contact/{id:int}")]
        public Task<ContactResponse> GetById([FromRoute] int id)
        {
            return _agendaService.GetContact(id);
        }

        [HttpPost("contact")]
        public async Task<ActionResult<ContactResponse>> Add([FromBody] ContactRequest model)
        {
            var created = await _agendaService.AddContact(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("contact/{id:int}")]
        public Task<ContactResponse> Update([FromRoute] int id, [FromBody] ContactRequest model)
        {
            return _agendaService.UpdateContact(id, model);
        }

        [HttpDelete("contact/{id:int}")]
        public Task Remove([FromRoute] int id)
        {
            return _agendaService.RemoveContact(id);
        }
    }
}
