using CrudMvc.Models;
using CrudMvc.Models.DapperModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CrudMvc.Repository.Interfaces.IRepository;

namespace CrudMvc.Repository.Interfaces
{
    public interface IImmobileRepository : IRepository<Immobile>
    {
        Task<IEnumerable<Client>> FindActiveClients();

        Task<IEnumerable<ImmobileDapper>> FindAllImmobileAsync();

    }
}
