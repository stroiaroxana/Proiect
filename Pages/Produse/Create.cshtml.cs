﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Produse
{
    public class CreateModel : CategoriiProdusPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ProducatorID"] = new SelectList(_context.Producator, "ID", "NumeProducator");
            var produs = new Produs();
            produs.CategoriiProdus = new List<CategorieProdus>();
            PopulateAssignedCategoryData(_context, produs);
            return Page();
        }


        [BindProperty]
        public Produs Produs { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategorii)
        {
            var newProdus = new Produs();
            if (selectedCategorii != null)
            {
                newProdus.CategoriiProdus = new List<CategorieProdus>();
                foreach (var cat in selectedCategorii)
                {
                    var catToAdd = new CategorieProdus
                    {
                        CategorieID = int.Parse(cat)
                    };
                    newProdus.CategoriiProdus.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Produs>(
                newProdus,
                "Produs",
                i => i.DenumireProdus, i => i.Descriere, i => i.Pret, i => i.PublishingDate, i => i.ProducatorID))
            {
                _context.Produs.Add(newProdus);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newProdus);
            return Page();
        }

    }
}
