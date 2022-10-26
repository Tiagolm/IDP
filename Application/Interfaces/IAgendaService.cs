using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAgendaService
    {
        Task<PaginationResult<ContatoResponse>> BuscarContatos(ContatoParametroBusca viewModel);
        Task<IEnumerable<ContatoTelefoneResponse>> BuscarTelefones(ContatoParametroBusca viewModel);
        IEnumerable<TipoContatoTelefoneResponse> TiposContatoTelefone();
        Task<ContatoResponse> ObterContato(int id);
        Task<ContatoResponse> AdicionarContato(ContatoRequest contatoViewModel);
        Task<ContatoResponse> AtualizarContato(int id, ContatoRequest contatoViewModel);
        Task RemoverContato(int id);
    }
}
