using CrudMvc.Database.EntityFramework;
using CrudMvc.Exceptions;
using CrudMvc.Models;
using CrudMvc.Repository.Interfaces;
using CrudMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMvc.Controllers
{
    public class ImmobilesController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly ImmobileService _immobileService;
        private readonly AppDbContext _context;

        public ImmobilesController(IClientRepository clientRepository, ImmobileService immobileService, AppDbContext context)
        {
            _immobileService = immobileService;
            _clientRepository = clientRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var immobiles = _immobileService.FindAllAsync();
            return View(await immobiles);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == Guid.Empty)
                return RedirectToAction(nameof(Error), new { message = "Id não providenciado" });

            var immobile = await _immobileService.FindByIdAsync(id.Value);

            if (immobile == null)
                return RedirectToAction(nameof(Error), new { message = "Cliente não encontrado" });

            return View(immobile);
        }

        public async Task<IActionResult> Create()
        {
            var activeClients = await _immobileService.FindActiveClients();
            ViewData["ClientId"] = new SelectList(activeClients, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeBusiness,Value,Description,IsActive,ClientId,Id")] Immobile immobile)
        {
            if (ModelState.IsValid)
            {
                await _immobileService.InsertAsync(immobile);
                return RedirectToAction(nameof(Index));
            }

            var activeClients = await _immobileService.FindActiveClients();
            ViewData["ClientId"] = new SelectList(activeClients, "Id", "Name", immobile.ClientId);
            return View(immobile);
        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immobile = await _immobileService.FindByIdAsync(id.Value);

            if (immobile == null)
            {
                return NotFound();
            }

            var activeClients = await _immobileService.FindActiveClients();

            ViewData["ClientId"] = new SelectList(activeClients, "Id", "Name", immobile.ClientId);
            return View(immobile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TypeBusiness,Value,Description,IsActive,ClientId,Id")] Immobile immobile)
        {
            if (id != immobile.Id)
                return RedirectToAction(nameof(Error), new { message = "Id missmatch" });

            if (ModelState.IsValid)
            {
                try
                {
                    await _immobileService.EditAsync(immobile);
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
                return RedirectToAction(nameof(Index));
            }
            var activeClients = await _immobileService.FindActiveClients();
            ViewData["ClientId"] = new SelectList(activeClients, "Id", "Name", immobile.ClientId);
            return View(immobile);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var immobile = await _context.Immobiles
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (immobile == null)
            {
                return NotFound();
            }

            return View(immobile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _immobileService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
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

        private bool ImmobileExists(Guid id)
        {
            return _context.Immobiles.Any(e => e.Id == id);
        }
    }
}
