namespace Cinema.Domain.Entidades
{
    public class Avaliacao
    {
        public string Id { get; set; }
        public Cliente Cliente { get; set; }
        public Filme Filme { get; set; }
        public int Pontuacao { get; set; }
        public string Comentario { get; set; }

        public bool ValidarPontuacao()
        {
            if (Pontuacao < 1 || Pontuacao > 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}