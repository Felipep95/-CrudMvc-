using System;
using System.ComponentModel.DataAnnotations;
using CrudMvc.Enums;

namespace CrudMvc.Models
{
    public class Immobile : Entity
    {
        [Required(ErrorMessage = "Selecione o tipo de negócio")]
        [Display(Name = "Tipo do Negócio")]
        public TypeBusiness TypeBusiness { get; set; }

        [Required(ErrorMessage = "Informe o valor do imóvel")]
        [Display(Name = "Valor")]
        public decimal Value { get; set; }

        [Display(Name = "Descrição")]
        public string? Description { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Selecione o cliente")]
        public Guid ClientId { get; set; }

        [Display(Name = "Cliente")]
        public Client Client { get; set; }
    }
}
