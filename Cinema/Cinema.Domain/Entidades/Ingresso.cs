namespace Cinema.Domain.Entidades
{
    public class Ingresso(string id, Sessao sessao, decimal preco, string assento, Cliente cliente)
    {
        public string Id { get; set; } = id;
        public Sessao Sessao { get; set; } = sessao;
        public decimal Preco { get; set; } = preco;
        public string Assento { get; set; } = assento;
        public Cliente Cliente { get; set; } = cliente;

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