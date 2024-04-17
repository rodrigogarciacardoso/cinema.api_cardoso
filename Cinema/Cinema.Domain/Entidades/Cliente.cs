namespace Cinema.Domain.Entidades
{
    public class Cliente
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<Ingresso> IngressosComprados { get; set; }

        public Cliente()
        {
            IngressosComprados = new List<Ingresso>();
        }

        public void ComprarIngresso(Ingresso ingresso)
        {
            IngressosComprados.Add(ingresso);
        }
    }
}