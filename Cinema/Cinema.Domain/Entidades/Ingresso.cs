namespace Cinema.Domain.Entidades
{
    public class Ingresso
    {
        public string Id { get; set; }
        public Sessao Sessao { get; set; }
        public decimal Preco { get; set; }
        public string Assento { get; set; }
        public Cliente Cliente { get; set; }

        public bool Validar()
        {
            if (DateTime.Now < Sessao.HorarioInicio)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}