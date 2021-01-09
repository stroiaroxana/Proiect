using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Produse
{
    public class EditModel : CategoriiProdusPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produs Produs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Produs = await _context.Produs.Include(b => b.Producator)
                .Include(b => b.CategoriiProdus)
                .ThenInclude(b => b.Categorie)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            Produs = await _context.Produs.FirstOrDefaultAsync(m => m.ID == id);

            if (Produs == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Produs);

            ViewData["ProducatorID"] = new SelectList(_context.Set<Producator>(), "ID", "NumeProducator");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] 
            selectedCategorii)
        {
            if (id == null)
            { 
                return NotFound(); 
            }
            var produsToUpdate = await _context.Produs
                .Include(i => i.Producator)
                .Include(i => i.CategoriiProdus)
                .ThenInclude(i => i.Categorie)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (produsToUpdate == null) 
            { 
                return NotFound(); 
            }
            if (await TryUpdateModelAsync<Produs>(
                produsToUpdate, "Produs", i => i.DenumireProdus, i => i.Descriere, i => i.Pret, i => i.PublishingDate, i => i.Producator))
            { 
                UpdateCategoriiProdus(_context, selectedCategorii, produsToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); 
            }
            UpdateCategoriiProdus(_context, selectedCategorii, produsToUpdate); 
            PopulateAssignedCategoryData(_context, produsToUpdate); 
            return Page();
        }
    }
}

