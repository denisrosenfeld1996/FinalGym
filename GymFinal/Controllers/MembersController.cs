using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymFinal.Data;
using GymFinal.Models;
using Microsoft.AspNetCore.Http;
using System.Dynamic;

namespace GymFinal.Controllers
{
    public class MembersController : Controller
    {
        private readonly GymFinalContext _context;

        public object ISession { get; private set; }

        public MembersController(GymFinalContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }
        //Odeya&maya-Search
        public async Task<IActionResult> Search(int id, string name)
        {
            var results = from m in _context.Members
                          where m.MembersID.Equals(id) || m.MemberName.Contains(name)
                          select m;
            //var results2 = _context.Members.Where(me => me.MembersID.Equals(id));
            return View("Index", await results.ToListAsync());
        }
        //Odeya&maya-Search

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .Include(l => l.Lessons) //Join
                /*.ThenInclude(sc => sc.StudioClass)*/ //Join
                .AsNoTracking() //Join
                .FirstOrDefaultAsync(m => m.MembersID == id);
            if (members == null)
            {
                return NotFound();
            }

            ////Query Join-Odeya
            //var viewModel =
            //              from me in _context.Members
            //              join sc in _context.StudioClass on me.MembersID equals sc.ID
            //              //where sc. == this.HttpContext.User.Identity.Name
            //              //orderby p.Name, p.ProjectNo
            //              select new Lesson { Members = me, StudioClass = sc };
            ////Ouery-Join

            return View(members);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembersID,MemberName,PhoneNumber,Address,EmailAddress")] Members members)
        {
            if (ModelState.IsValid)
            {
                _context.Add(members);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(members);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MembersID,MemberName,PhoneNumber,Address,EmailAddress")] Members members)
        {
            if (id != members.MembersID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(members);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembersExists(members.MembersID))
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
            return View(members);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var members = await _context.Members
                .FirstOrDefaultAsync(m => m.MembersID == id);
            if (members == null)
            {
                return NotFound();
            }
            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var members = await _context.Members.FindAsync(id);
            _context.Members.Remove(members);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembersExists(int id)
        {
            return _context.Members.Any(e => e.MembersID == id);
        }
        //Login-Internet

        //////Login-Afik
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    var user = _context.Members.FirstOrDefault(u => u.MemberName == username);
        //    if (user != null)
        //    {
        //        if (user.Password == password)
        //        {
        //            SignIn(user);
        //            return RedirectToAction("Homepage", "Gym");
        //        }
        //        ViewBag.error = 400; // Bad password was enterd
        //        return View("ClientError");
        //    }
        //    ViewBag.error = 404; // Didn't found the user
        //    return View("ClientError");
        //}

        //private void SignIn(Members user)
        //{
        //    HttpContext.Session.SetString("Type", user.Type.ToString());
        //    HttpContext.Session.SetString("UserId", user.MembersID.ToString());
        //    HttpContext.Session.SetString("UserName", user.MemberName.ToString());
        //}
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Register(string username, string password, string Email, string Address = null, string type = "Customer")
        //{
        //    Members account = new Members() { MemberName = username, Password = password, Address = Address, EmailAddress = Email };

        //    if (type == "Customer")
        //        account.Type = UserType.Customer;
        //    else
        //        account.Type = UserType.Admin;
        //    var user = _context.Members.FirstOrDefault(u => u.MemberName == username);
        //    if (user == null) // If username doesn't exist
        //    {
        //        _context.Add(account);
        //        await _context.SaveChangesAsync();
        //        SignIn(account);
        //        return RedirectToAction("HomePage", "Movies");
        //    }
        //    ViewBag.error = 400; // Username exist
        //    return View("ClientError");
        //}

        //public IActionResult Logout()
        //{
        //    HttpContext.Session.Remove("Type");
        //    HttpContext.Session.Remove("UserId");
        //    HttpContext.Session.Remove("UserName");
        //    return RedirectToAction("HomePage", "Movies");
        //}

        //public async Task<IActionResult> Dashboard()
        //{
        //    if (HttpContext.Session.GetString("Type") == "Admin")
        //    {
        //        //Function to verify the user before get in the view)
        //        dynamic Multiple = new ExpandoObject();
        //        Multiple.actors = await _context.StudioClass.Include(a => a.ClassName).ToListAsync();
        //        Multiple.movies = await _context.Lesson.ToListAsync();
        //        Multiple.users = await _context.Members.ToListAsync();
        //        Multiple.genres = await _context.Trainers.Include(g => g.TrainerName).ToListAsync();
        //        return View(Multiple);
        //    }
        //    else
        //    {
        //        ViewBag.error = 401;
        //        return View("ClientError");
        //    }
        //}
        ////Login-Afik

    }
}
