using CrudMvc.Database.EntityFramework;
using CrudMvc.Models;
using CrudMvc.Models.DapperModels;
using CrudMvc.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudMvc.Repository
{
    public class ImmobileRepository : IImmobileRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ImmobileRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task InsertAsync(Immobile immobile)
        {
            _context.Immobiles.Add(immobile);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Immobile>> FindAllAsync()
        {
            return await _context.Immobiles.Include(c => c.Client).ToListAsync();
        }

        public async Task<IEnumerable<ImmobileDapper>> FindAllImmobileAsync()
        {
            var conn = _configuration.GetConnectionString("CrudMvcContext");
            var query = "SELECT Immobiles.*, Clients.Name FROM Immobiles INNER JOIN Clients ON Immobiles.ClientId = Clients.Id;";

            using (var db = new SqlConnection(conn))
            {
                return await db.QueryAsync<ImmobileDapper>(query);
            }
        }

        public async Task<Immobile> FindByIdAsync(Guid id)
        {
            return await _context.Immobiles.Include(c => c.Client).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Client>> FindActiveClients()
        {
            var conn = _configuration.GetConnectionString("CrudMvcContext");
            var query = "SELECT * FROM Clients WHERE IsActive = 1;";

            using (var db = new SqlConnection(conn))
            {
                return await db.QueryAsync<Client>(query);
            }
        }

        public async Task UpdateAsync(Immobile immobile)
        {
            _context.Immobiles.Update(immobile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Immobile immobile)
        {
            _context.Immobiles.Remove(immobile);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Immobile>> FindAsync(Expression<Func<Immobile, bool>> predicate)
        {
            return await _context.Set<Immobile>().Where(predicate).ToListAsync();
        }
    }
}
