using CrudMvc.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrudMvc.Models.DapperModels
{
    public class ImmobileDapper
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de negócio")]
        [Display(Name = "Tipo do Negócio")]
        public TypeBusiness TypeBusiness { get; set; }

        [Required(ErrorMessage = "Informe o valor do imóvel")]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public decimal Value { get; set; }

        [Display(Name = "Descrição")]
        public string? Description { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Selecione o cliente")]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "Selecione o cliente")]
        [Display(Name = "Cliente")]
        public string Name { get; set; }

    }
}
