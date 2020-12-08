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

namespace GymFinal.Controllers
{
    public class StudioClassesController : Controller
    {
        private readonly GymFinalContext _context;

        public StudioClassesController(GymFinalContext context)
        {
            _context = context;
        }

        //GroupBy-14.11-Osher
        //public ActionResult groupBy()
        //{
        //    var classesByType =
        //       from u in _context.Lesson
        //       group u by u.StudioClass into g

        //       select new { StudioClass = g.Key, count = g.Count(), g.FirstOrDefault().StudioClassID };
        //    var group = new List<Lesson>();
        //    foreach (var t in classesByType)
        //    {
        //        group.Add(new Lesson()
        //        {
        //            StudioClass = t.StudioClass,
        //            LessonNumber = t.count
        //        });
        //    }
        //    ViewBag.typeAirboic = group[0].StudioClass.Type;
        //    ViewBag.sizeAirobic = group[0].LessonNumber;
        //    ViewBag.typeStreaching = group[1].StudioClass.Type;
        //    ViewBag.sizeStreaching = group[1].LessonNumber;
        //    //ViewBag.type..... = group[2].flightBoard.boardName;
        //    //ViewBag.size,... = group[2].flightNumber;

        //    return View(group);

        //}
        //GroupBy-14.11


        //GET: StudioClasses
        public async Task<IActionResult> Index()
        {
            //Odeya&maya-GroupBy
            //var result = from sc in _context.StudioClass
            //             group sc by sc.TypeID;        
            //Odeya&maya-GroupBy
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
        //Odeya&maya-Search
        public async Task<IActionResult> Search(string name, int calories, string type)
        {
            var results = from sc in _context.StudioClass
                          where sc.ClassName.Contains(name) || sc.BurnCalories <= calories || sc.Type.Equals(type)
                          select sc;
            //var results2 = _context.StudioClass.Where(s => s.ClassName.Contains(name));
            return View("Index", await results.ToListAsync());
        }
        //Odeya&maya-Search

        // MAYA ADDED


        //public void /*/Task<IActionResult>/*/ Sample_GroupBy_Linq()
        //{
        //    var ClassNameList1 = new List<StudioClass>{

        //              new StudioClass { ID=123, ClassName="Zumba",Type="Airobic",DuringTime=1 },
        //              new StudioClass { ID=1234, ClassName="Trx",Type="stretching",DuringTime=2 },
        //              new StudioClass { ID=1235, ClassName="Pilaties",Type="stretching",DuringTime=1 }
        //     };

        //    var result = ClassNameList1.GroupBy(x => new { ClassName = x.ClassName, Type = x.Type })
        //     .Select(b => new StudioClass() { ClassName = b.Key.ClassName, Type = b.Key.Type, ClassNameList = b.Select(c => c.ClassNameList).ToList() })
        //     .ToList();
        //}

        //return View(List_sc);

        // MAYA ADDED


        // GET: StudioClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
            //Join Query
            //ViewBag.reviews = await _context.Lesson.Where(le => le.LessonID == id).Include(le=> le.LessonDay).ToListAsync();
            //ViewBag.reviews.Reverse();
            //Join-Afik:
            //ViewBag.reviews = await _context.Lesson.Where(le => le.LessonID == id).Join(_context.Trainers, tr => tr.TrainerID, tr => tr.TrainersID, (sc, tr) => tr).ToListAsync();
            ////Query Join
            //var viewModel =
            //              from sc in _context.StudioClass
            //              join tr in _context.Trainers on sc.ID equals tr.TrainersID
            //              //where sc. == this.HttpContext.User.Identity.Name
            //              //orderby p.Name, p.ProjectNo
            //              select new Lesson { StudioClass = sc, Trainer = tr };
            //////Ouery-Join
            return View(studioClass);
        }

        // GET: StudioClasses/Create
        public IActionResult Create()
        {
            return View();
        }
        //Odeya-FacebookAPI
        public void facebook(string ClassName)  //after we add aflight we posted in facebook
        {
            dynamic messagePost = new ExpandoObject();
            messagePost.message = "Manager Update:" +
                "Hey Everyone! We just got a new lesson to our gym! It's " + ClassName;


            string acccessToken = "EAALojoyfCFsBAKNwbazMQrVby4IEZBynTHNlPgP1bUSSXltQw6ME72SLLBdfwL8FET5Vsrxf86OFyJ8THmtMKPCOYayohb0QCXTPQo00RPBQVlZAHVj6ZAzo2C2LQTY5J2ZCAGHTSjZCRQJ1kYOvcbdfRSQGxtu9U92cOfv2Wyp2pDyXeaQtZAJ7pdfDk7kwNw2UhDDc5cZAOJJuHqo5uUk";
            FacebookClient appp = new FacebookClient(acccessToken);
            try
            {
                var postId = appp.Post("118041190105913" + "/feed", messagePost);
            }
            catch (FacebookOAuthException ex)
            { //handle oauth exception } catch (FacebookApiException ex) { //handle facebook exception
            }

        }
        //Odeya-FacebookAPI

        // POST: StudioClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClassName,Type,DuringTime,BurnCalories,TypeID")] StudioClass studioClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studioClass);
                //Odeya-FacebookAPI
                facebook(studioClass.ClassName);
                //Odeya-FacebookAPI
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClassName,Type,DuringTime,BurnCalories,TypeID")] StudioClass studioClass)
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
