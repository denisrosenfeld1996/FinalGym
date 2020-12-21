

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymFinal.Data;
using GymFinal.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Facebook;
using System.Dynamic;
using SQLitePCL;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.AspNetCore.Identity;


namespace GymFinal.Controllers
{
    public class StudioClassesController : Controller
    {
        private readonly GymFinalContext _context;

        public object Values { get; private set; }

        public StudioClassesController(GymFinalContext context)
        {
            _context = context;
        }


        //GET: StudioClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudioClass.ToListAsync());
        }
        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
        public async Task<IActionResult> BMI()
        {
            return View();
        }

        public async Task<IActionResult> Info()
        {
            return View();
        }
        //Search button
        public async Task<IActionResult> Search(string name, int calories, string type)
        {
            var results = from sc in _context.StudioClass
                          where sc.ClassName.Contains(name) || sc.BurnCalories <= calories || sc.Type.Equals(type)
                          select sc;
            return View("Index", await results.ToListAsync());
        }


        // GET: StudioClasses/Details/5
        //Learning algorithm
        public async Task<IActionResult> Details(int? id)
        {
            var temp = _context.StudioClass.Find(id);
            if (Best.mapOfClass == null)
            {
                Best.mapOfClass = new Dictionary<string, int>();
                Best.mapOfClass.Add(temp.Type, 1);

            }
            else
            {
                if (Best.mapOfClass.ContainsKey(temp.Type))
                {

                    Best.mapOfClass[temp.Type]++;
                }
                else
                {
                    Best.mapOfClass.Add(temp.Type, 1);
                }
            }

            Best.bestclass = Best.mapOfClass.FirstOrDefault(x => x.Value == Best.mapOfClass.Values.Max()).Key;
            if (id == null)
            {
                return NotFound();
            }

            var studioClass = await _context.StudioClass
                .Include(l => l.Lessons).ThenInclude(t => t.Trainer)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studioClass == null)
            {
                return NotFound();
            }

            return View(studioClass);
        }

        // GET: StudioClasses/Create
        public IActionResult Create()
        {
            return View();
        }
        //FacebookAPI
        public void facebook(string ClassName)
        {
            dynamic messagePost = new ExpandoObject();
            messagePost.message = "Manager Update:" +
                "Hey Everyone! We just got a new lesson to our gym! It's " + ClassName;
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

        // POST: StudioClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClassName,Type,DuringTime,BurnCalories")] StudioClass studioClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studioClass);
                //FacebookAPI
                facebook(studioClass.ClassName);
                //FacebookAPI
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studioClass);
        }

        // GET: StudioClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioClass = await _context.StudioClass.FindAsync(id);
            if (studioClass == null)
            {
                return NotFound();
            }
            return View(studioClass);
        }

        // POST: StudioClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClassName,Type,DuringTime,BurnCalories")] StudioClass studioClass)
        {
            if (id != studioClass.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studioClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudioClassExists(studioClass.ID))
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
            return View(studioClass);
        }

        // GET: StudioClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studioClass = await _context.StudioClass
                .FirstOrDefaultAsync(m => m.ID == id);
            if (studioClass == null)
            {
                return NotFound();
            }

            return View(studioClass);
        }

        public ActionResult groupBy()
        {
            var TypesCount = _context.StudioClass
                .GroupBy(p => p.Type)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToList();
            int types = TypesCount.Count();
            return View();
        }

        // POST: StudioClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studioClass = await _context.StudioClass.FindAsync(id);
            _context.StudioClass.Remove(studioClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudioClassExists(int id)
        {
            return _context.StudioClass.Any(e => e.ID == id);
        }
    }


}



