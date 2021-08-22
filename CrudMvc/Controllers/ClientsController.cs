using CrudMvc.Database.EntityFramework;
using CrudMvc.Models;
using CrudMvc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMvc.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClientService _clientService;
        private readonly AppDbContext _context;

        public ClientsController(ClientService clientService, AppDbContext context)
        {
            _clientService = clientService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clientService.FindAllAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == Guid.Empty)
                return RedirectToAction(nameof(Error), new { message = "Id não providenciado" });

            var client = await _clientService.FindByIdAsync((Guid)id);

            if (client == null)
                return RedirectToAction(nameof(Error), new { message = "Cliente não encontrado." });

            return View(client);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Cpf,IsActive,Id")] Client client)
        {
            var hasAnyEmail = _clientService.FindByEmail(client.Email);
            var hasAnyCpf = _clientService.FindByCpf(client.Cpf);

            if (hasAnyEmail != null)
                ModelState.AddModelError("Email", "Email já está sendo utilizado");

            if (hasAnyCpf != null)
                ModelState.AddModelError("Cpf", "Cpf já está sendo utilizado");

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                client.Id = Guid.NewGuid();
                await _clientService.InsertAsync(client);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não providenciado" });

            var client = await _clientService.FindByIdAsync(id.Value);

            if (client == null)
                return RedirectToAction(nameof(Error), new { message = "Cliente não encontrado" });

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Client client)
        {
            var checkClientEmail = _context.Clients.FirstOrDefault(x => x.Email == client.Email);
            var checkClientCpf = _context.Clients.FirstOrDefault(x => x.Cpf == client.Cpf);

            if (checkClientEmail != null && checkClientEmail.Id != id)
            {
                ModelState.AddModelError("Email", "Email já está sendo utilizado");
            }

            if (checkClientCpf != null && checkClientCpf.Id != id)
            {
                ModelState.AddModelError("Cpf", "Cpf já está sendo utilizado");
            }


            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id != client.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Client not found." });
            }

            try
            {
                await _clientService.EditAsync(client);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToAction(nameof(Error), new { message = "Id não providenciado" });

            var client = await _clientService.FindByIdAsync(id);

            if (client == null)
                return NotFound();

            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
                return RedirectToAction(nameof(Error), new { message = "Id não providenciado" });

            var client = await _clientService.FindByIdAsync(id);

            if (client == null)
                return NotFound("Cliente não encontrado");

            await _clientService.RemoveAsync(client.Id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
