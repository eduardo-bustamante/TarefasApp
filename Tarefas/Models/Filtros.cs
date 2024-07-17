namespace Tarefas.Models
{
    public class Filtros
    {
        public Filtros(string filtrostring)
        {
            FiltroString = filtrostring ?? "todos-todos-todos";
            string[]filtros = FiltroString.Split('-');
            CategoriaId = filtros[0];
            Vencimento = filtros[1];
            CondicaoId = filtros[2];
        }

        public string FiltroString { get; }
        public string CategoriaId { get; }

        public string Vencimento { get; }
        public string CondicaoId { get; }
        public bool TemCategoria => CategoriaId.ToLower() != "todos";
        public bool TemVencimento => Vencimento.ToLower() != "todos";
        public bool TemCondicao => CondicaoId.ToLower() != "todos";
        public static Dictionary<string, string> VencimentoFiltroValores =>
            new Dictionary<string, string> {
            { "futuro", "Futuro"},
            { "passado", "Passado"},
            { "hoje", "Hoje" }
        };

        public bool EhPassado => Vencimento.ToLower() == "passado";
        public bool EhFuturo => Vencimento.ToLower() == "futuro";
        public bool EhHoje => Vencimento.ToLower() == "hoje";
    }
}
