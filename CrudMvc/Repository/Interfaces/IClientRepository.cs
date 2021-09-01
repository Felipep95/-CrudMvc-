using CrudMvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;
using static CrudMvc.Repository.Interfaces.IRepository;

namespace CrudMvc.Repository.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        public IEnumerable<Client> FindActiveClients();
        public Client FindByEmail(string email);
        public Client FindByCpf(string cpf);

        public Task<IPagedList<Client>> Pagination(int? page, int itensByPage);

        //IQueryable<TEntity> Table { get; } //TODO: configurar método

    }
}
