using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Tarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor coloque uma descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor coloque uma data de vencimento")]
        [Display(Name = "Data de Vencimento")]
	    [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DataDeVencimento { get; set; }

        [Required(ErrorMessage = "Por favor insira uma categoria")]
        public string CategoriaId { get; set; } = string.Empty;

        [ValidateNever]
        public Categoria Categoria { get; set; } = null!;

        [Required(ErrorMessage = "Por favor selecione um status")]
        public string CondicaoId { get; set; } = string.Empty;

        [ValidateNever]
        public Condicao Condicao { get; set; } = null!;

        public bool Atrasada => CondicaoId == "aberto" && DataDeVencimento < DateTime.Today;

    }
}
