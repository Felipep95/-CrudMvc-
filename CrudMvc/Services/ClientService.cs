using CrudMvc.Exceptions;
using CrudMvc.Models;
using CrudMvc.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrudMvc.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> FindAllAsync()
        {
            return await _clientRepository.FindAllAsync();
        }

        public async Task InsertAsync(Client client)
        {
            var newClient = new Client();

            client.Id = Guid.NewGuid();

            newClient.Id = client.Id;
            newClient.Name = client.Name;
            newClient.Email = client.Email;
            newClient.Cpf = client.Cpf;
            newClient.Immobiles = client.Immobiles;
            newClient.IsActive = client.IsActive;
            
             await _clientRepository.InsertAsync(client);
        }

        public async Task EditAsync(Client client)
        {
            var clientToEdit = await _clientRepository.FindByIdAsync(client.Id);

            if (clientToEdit == null)
                throw new NotFoundException("cliente não encontrado");

            clientToEdit.Name = client.Name;
            clientToEdit.Email = client.Email;
            clientToEdit.Cpf = client.Cpf;
            clientToEdit.IsActive = client.IsActive;

            try
            {
                await _clientRepository.UpdateAsync(clientToEdit);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }

        public async Task<Client> FindByIdAsync(Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);

            if (client == null)
                throw new NotFoundException("cliente não encontrado");

            return client;
        }

        public async Task RemoveAsync(Guid id)
        {
            var client = await _clientRepository.FindByIdAsync(id);

            if (client == null)
                throw new NotFoundException("cliente não encontrado");

            try
            {
                await _clientRepository.DeleteAsync(client);
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não foi possível deletar o Imóvel, Imóvel associado a um cliente.");
            }
        }

        public async Task<IEnumerable<Client>> FindAsync(Expression<Func<Client, bool>> predicate)
        {
            return await _clientRepository.FindAsync(predicate);
        }

        public Client FindByEmail(string email)
        {
            return _clientRepository.FindByEmail(email);
        }

        public Client FindByCpf(string cpf)
        {
            return _clientRepository.FindByCpf(cpf);
        }
    }
}


