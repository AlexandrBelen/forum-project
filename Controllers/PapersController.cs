using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class PapersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PapersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Papers
        public async Task<IActionResult> Index()
        {
            var papersList = await _context.Paper.Include(p => p.Author).OrderByDescending(p => p.DateTime).ToListAsync();
            
            return View(papersList);
        }



        // GET: Papers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper
                .Include(p => p.Author)
                .Include(p => p.Comments).ThenInclude(p => p.Author)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        // GET: Papers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Papers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Intro,Body,Themе")] Paper paper)
        {
            if (ModelState.IsValid)
            {
                paper.Author = await GetCurrentUserAsync();
                _context.Add(paper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paper);
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string comment, int paperId, string userId)
        {
            var author = await _userManager.FindByEmailAsync(userId);
            var currentUser = await GetCurrentUserAsync();

            if (ModelState.IsValid && author == currentUser)
            {
                _context.Add(new Comment() { Body = comment, Author = author, PaperID = paperId });
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
            
            
        }

        // GET: Papers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper.FindAsync(id);
            if (paper == null)
            {
                return NotFound();
            }
            return View(paper);
        }

        // POST: Papers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Intro,Body,Themе")] Paper paper)
        {
            if (id != paper.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaperExists(paper.ID))
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
            return View(paper);
        }

        // GET: Papers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paper = await _context.Paper
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paper == null)
            {
                return NotFound();
            }

            return View(paper);
        }

        // POST: Papers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paper = await _context.Paper.FindAsync(id);
            _context.Paper.Remove(paper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaperExists(int id)
        {
            return _context.Paper.Any(e => e.ID == id);
        }
    }
}
