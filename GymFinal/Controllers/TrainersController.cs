using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymFinal.Data;
using GymFinal.Models;
using System.Dynamic;
using Facebook;

namespace GymFinal.Controllers
{
    public class TrainersController : Controller
    {
        private readonly GymFinalContext _context;

        public TrainersController(GymFinalContext context)
        {
            _context = context;
        }

        // GET: Trainers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trainers.ToListAsync());

        }
        //Search
        public async Task<IActionResult> Search(string name, int id)
        {
            var results = from tr in _context.Trainers
                          where tr.TrainerName.Contains(name) || tr.TrainersID == id
                          select tr;
            //var results2 = _context.StudioClass.Where(s => s.ClassName.Contains(name));
            return View("Index", await results.ToListAsync());
        }
        
        // GET: Trainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainers = await _context.Trainers
                .FirstOrDefaultAsync(m => m.TrainersID == id);
            if (trainers == null)
            {
                return NotFound();
            }

            return View(trainers);
        }

        // GET: Trainers/Create
        public IActionResult Create()
        {
            return View();
        }
        //FacebookAPI
        public void facebook(string TrainerName)
        {
            dynamic messagePost = new ExpandoObject();
            messagePost.message = "Manager Update:" +
                "Hey Everyone! We just got a new trainer to our gym! It's " + TrainerName;
            string acccessToken = "EAALojoyfCFsBAIhDN28AWr7esGZBTSVLuogPfGx1QKSBEMGZAUicy3FGfonVGTg7pZABirZBCqjZCbt8JSAZC6L6e78CyAxlR92gDKbMnWpEBZCYrB8p8IF0k5cTCGZA1zl1M7XoksOhfTmL6j47z2ACfZBwkfJXpFp0v5pfldh2UeFM4wL4rhv4j9vsL94OeHyqSbzipSdak4CCohQNIHUBx";
            FacebookClient appp = new FacebookClient(acccessToken);
            try
            {
                var postId = appp.Post("118041190105913" + "/feed", messagePost);
            }
            catch (FacebookOAuthException ex)
            {
            }

        }
        // POST: Trainers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainersID,TrainerName,Seniority,PhoneNumber,Email")] Trainers trainers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainers);
                //FacebookAPI
                facebook(trainers.TrainerName);
                //FacebookAPI
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainers);
        }

        // GET: Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainers = await _context.Trainers.FindAsync(id);
            if (trainers == null)
            {
                return NotFound();
            }
            return View(trainers);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainersID,TrainerName,Seniority,PhoneNumber,Email")] Trainers trainers)
        {
            if (id != trainers.TrainersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainersExists(trainers.TrainersID))
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
            return View(trainers);
        }

        // GET: Trainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainers = await _context.Trainers
                .FirstOrDefaultAsync(m => m.TrainersID == id);
            if (trainers == null)
            {
                return NotFound();
            }

            return View(trainers);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainers = await _context.Trainers.FindAsync(id);
            _context.Trainers.Remove(trainers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainersExists(int id)
        {
            return _context.Trainers.Any(e => e.TrainersID == id);
        }
    }
}
