namespace Cinema.Domain.Entidades
{
    public class Funcionario
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public decimal Salario { get; set; }

        public void DarAumento(decimal valorAumento)
        {
            Salario += valorAumento;
        }
    }
}