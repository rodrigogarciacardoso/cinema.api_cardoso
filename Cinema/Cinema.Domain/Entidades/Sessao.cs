namespace Cinema.Domain.Entidades
{
    public class Sessao(string id, Filme filme, DateTime horarioInicio, DateTime horarioFim, Sala sala, int ingressosDisponiveis)
    {
        public string Id { get; set; } = id;
        public Filme Filme { get; set; } = filme;
        public DateTime HorarioInicio { get; set; } = horarioInicio;
        public DateTime HorarioFim { get; set; } = horarioFim;
        public Sala Sala { get; set; } = sala;
        public int IngressosDisponiveis { get; set; } = ingressosDisponiveis;

        public void ComprarIngresso()
        {
            if (IngressosDisponiveis > 0)
            {
                IngressosDisponiveis--;
            }
            else
            {
                throw new Exception("Ingressos esgotados para esta sessão.");
            }
        }
    }
}