using System.ComponentModel.DataAnnotations;

namespace CrudMvc.Enums
{
    public enum TypeBusiness
    {
        [Display (Name = "Venda")]
        Sale = 0,

        [Display(Name = "Aluguel")]
        Rent = 1
    }
}
