using CrudMvc.Database.EntityFramework;
using CrudMvc.Models;
using CrudMvc.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudMvc.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ClientRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task InsertAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Client>> FindAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> FindByIdAsync(Guid id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            return client;
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Client>> FindAsync(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Set<Client>().Where(predicate).ToListAsync();
        }

        public IEnumerable<Client> FindActiveClients()
        {
            return _context.Clients.Where(c => c.IsActive == true);
        }

        public Client FindByEmail(string email)
        {
            return _context.Clients.FirstOrDefault(x => x.Email == email);
        }

        public Client FindByCpf(string cpf)
        {
            return _context.Clients.FirstOrDefault(x => x.Cpf == cpf);
        }
    }
}
