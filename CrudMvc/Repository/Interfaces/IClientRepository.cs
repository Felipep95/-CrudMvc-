using CrudMvc.Models;
using System.Collections.Generic;
using static CrudMvc.Repository.Interfaces.IRepository;

namespace CrudMvc.Repository.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        public IEnumerable<Client> FindActiveClients();
        public Client FindByEmail(string email);
        public Client FindByCpf(string cpf);

    }
}
