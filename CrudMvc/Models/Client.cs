using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace CrudMvc.Models
{
    public class Client : Entity
    {
        
        [Required(ErrorMessage = "O Campo nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo CPF é obrigatório.")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }
        
        public List<Immobile> Immobiles { get; set; }
    }
}
