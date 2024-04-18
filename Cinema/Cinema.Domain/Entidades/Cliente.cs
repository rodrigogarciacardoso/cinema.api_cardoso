namespace Cinema.Domain.Entidades
{
    public class Cliente(string id, string nome, string email, string telefone, List<Ingresso> ingressos)
    {
        public string Id { get; set; } = id;
        public string Nome { get; set; } = nome;
        public string Email { get; set; } = email;
        public string Telefone { get; set; } = telefone;
        public List<Ingresso> IngressosComprados { get; set; } = ingressos;

        public void ComprarIngresso(Ingresso ingresso)
        {
            IngressosComprados.Add(ingresso);
        }
    }
}