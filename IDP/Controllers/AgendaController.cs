using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/agenda")]
    [Authorize]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;

        public AgendaController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpGet("tipos-telefones")]
        public IEnumerable<TipoContatoTelefoneResponse> TiposTelefone()
        {
            return _agendaService.TiposContatoTelefone();
        }

        [HttpGet("contatos")]
        public Task<PaginationResult<ContatoResponse>> BuscarContatos([FromQuery] ContatoParametroBusca parametroBusca)
        {
            return _agendaService.BuscarContatos(parametroBusca);
        }

        [HttpGet("telefones")]
        public Task<IEnumerable<ContatoTelefoneResponse>> BuscarTelefones([FromQuery] ContatoParametroBusca parametroBusca)
        {
            return _agendaService.BuscarTelefones(parametroBusca);
        }

        [HttpGet("contato/{id:int}")]
        public Task<ContatoResponse> ObterPorId([FromRoute] int id)
        {
            return _agendaService.ObterContato(id);
        }

        [HttpPost("contato")]
        public async Task<ActionResult<ContatoResponse>> Adicionar([FromBody] ContatoRequest model)
        {
            var created = await _agendaService.AdicionarContato(model);
            return CreatedAtAction(nameof(ObterPorId), new { id = created.Id }, created);
        }

        [HttpPut("contato/{id:int}")]
        public Task<ContatoResponse> Atualizar([FromRoute] int id, [FromBody] ContatoRequest model)
        {
            return _agendaService.AtualizarContato(id, model);
        }

        [HttpDelete("contato/{id:int}")]
        public Task Remover([FromRoute] int id)
        {
            return _agendaService.RemoverContato(id);
        }
    }
}
