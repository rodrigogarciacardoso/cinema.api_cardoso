namespace Cinema.Domain.Entidades
{
    public class Sala(string id, int numero, int capacidade, List<Sessao> sessoes)
    {
        public string Id { get; set; } = id;
        public int Numero { get; set; } = numero;
        public int Capacidade { get; set; } = capacidade;
        public List<Sessao> Sessoes { get; set; } = sessoes;

        public void AdicionarSessao(Sessao sessao)
        {
            Sessoes.Add(sessao);
        }
    }
}