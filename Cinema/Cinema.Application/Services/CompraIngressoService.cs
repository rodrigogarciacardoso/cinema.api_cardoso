using Cinema.Domain.Entidades;
using Cinema.Domain.Interfaces;

namespace Cinema.Application.Services
{
    public class CompraIngressoService(ISessaoRepository sessaoRepository, IClienteRepository clienteRepository)
    {
        private readonly ISessaoRepository _sessaoRepository = sessaoRepository;
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task ComprarIngressoAsync(string clienteId, string sessaoId, string assento)
        {
            var cliente = await _clienteRepository.GetClienteAsync(clienteId);
            var sessao = await _sessaoRepository.GetSessaoAsync(sessaoId);

            if (sessao.IngressosDisponiveis <= 0)
            {
                throw new Exception("Ingressos esgotados para esta sessão.");
            }

            var ingresso = new Ingresso
            {
                Sessao = sessao,
                Preco = sessao.Filme.Preco,
                Assento = assento,
                Cliente = cliente
            };

            cliente.ComprarIngresso(ingresso);            
            sessao.ComprarIngresso();
                        
            await _clienteRepository.UpdateClienteAsync(cliente);
            await _sessaoRepository.UpdateSessaoAsync(sessao);
        }
    }
}