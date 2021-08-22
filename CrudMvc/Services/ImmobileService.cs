using CrudMvc.Database.EntityFramework;
using CrudMvc.Exceptions;
using CrudMvc.Models;
using CrudMvc.Models.DapperModels;
using CrudMvc.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudMvc.Services
{
    public class ImmobileService
    {
        private readonly IImmobileRepository _immobileRepository;
        private readonly IClientRepository _clientRepository;
        private readonly AppDbContext _context;

        public ImmobileService(
            IImmobileRepository immobileRepository,
            IClientRepository clientRepository,
            AppDbContext context)
        {
            _immobileRepository = immobileRepository;
            _clientRepository = clientRepository;
            _context = context;
        }

        public async Task<IEnumerable<ImmobileDapper>> FindAllAsync()
        {
            return await _immobileRepository.FindAllImmobileAsync();
        }

        public async Task<Immobile> FindByIdAsync(Guid id)
        {
            return await _immobileRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Client>> FindActiveClients()
        {
            return await _clientRepository.FindAsync(c => c.IsActive == true);
        }

        public async Task InsertAsync(Immobile immobile)
        {
            immobile.Id = Guid.NewGuid();

            var newImmobile = await _context.Immobiles
                .Include(c => c.Client)
                .FirstOrDefaultAsync(i => i.Id == immobile.Id);

            await _immobileRepository.InsertAsync(immobile);
        }

        public async Task EditAsync(Immobile immobile)
        {
            var immobileToEdit = await _immobileRepository.FindByIdAsync(immobile.Id);

            if (immobileToEdit == null)
                throw new NotFoundException("Imóvel não encontrado");
            
            immobileToEdit.Id = immobile.Id;
            immobileToEdit.TypeBusiness = immobile.TypeBusiness;
            immobileToEdit.Value = immobile.Value;
            immobileToEdit.Description = immobile.Description;
            immobileToEdit.IsActive = immobile.IsActive;
            immobileToEdit.ClientId = immobile.ClientId;

            try
            {
                await _immobileRepository.UpdateAsync(immobileToEdit);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var immobileToRemove = await _immobileRepository.FindByIdAsync(id);

            if (immobileToRemove == null)
                throw new NotFoundException("Imóvel não encontrado");

            try
            {
                await _immobileRepository.DeleteAsync(immobileToRemove);
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não foi possível deletar o Imóvel, Imóvel associado a um cliente.");
            }
        }

        public async Task<IEnumerable<Immobile>> FindAsync(Expression<Func<Immobile, bool>> predicate)
        {
            return await _immobileRepository.FindAsync(predicate);
        }
    }
}
