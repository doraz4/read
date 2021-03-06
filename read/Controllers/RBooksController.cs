﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using read.Models;
using Microsoft.AspNetCore.Authorization;


namespace read.Controllers
{
    public class RBooksController : Controller
    {
        private readonly readContext _context;

        public RBooksController(readContext context)
        {
            _context = context;
        }

        // GET: RBooks
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AllBooks.ToListAsync());
        }

        // GET: RBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rBooks = await _context.AllBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rBooks == null)
            {
                return NotFound();
            }

            return View(rBooks);
        }

        // GET: RBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> Create([Bind("Id,Name,text")] RBooks rBooks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rBooks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rBooks);
        }

        // GET: RBooks/Edit/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rBooks = await _context.AllBooks.FindAsync(id);
            if (rBooks == null)
            {
                return NotFound();
            }
            return View(rBooks);
        }

        // POST: RBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,text")] RBooks rBooks)
        {
            if (id != rBooks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rBooks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RBooksExists(rBooks.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rBooks);
        }

        // GET: RBooks/Delete/5
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rBooks = await _context.AllBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rBooks == null)
            {
                return NotFound();
            }

            return View(rBooks);
        }

        // POST: RBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rBooks = await _context.AllBooks.FindAsync(id);
            _context.AllBooks.Remove(rBooks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RBooksExists(int id)
        {
            return _context.AllBooks.Any(e => e.Id == id);
        }
    }
}
