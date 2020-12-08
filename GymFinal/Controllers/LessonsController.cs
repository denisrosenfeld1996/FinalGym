using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymFinal.Data;
using GymFinal.Models;
using Facebook;
using System.Dynamic;


namespace GymFinal.Controllers
{
    public class LessonsController : Controller
    {
        private readonly GymFinalContext _context;

        public LessonsController(GymFinalContext context)
        {
            _context = context;
        }

        //GroupBy-Noy

        //GroupBy-Noy
        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var gymContext = _context.Lesson.Include(l => l.StudioClass).Include(l => l.Trainer).Include(l => l.Members);
            //Query Join-Lesson+SC+Trainers
            var viewModel =
                          from sc in _context.StudioClass
                          join tr in _context.Trainers on sc.ID equals tr.TrainersID
                          //where sc. == this.HttpContext.User.Identity.Name
                          //orderby p.Name, p.ProjectNo
                          select new Lesson { StudioClass = sc, Trainer = tr };
            //Ouery-Join-Lesson+SC+Trainers
            ////Query Join-Odeya
            var viewModel1 =
                          from sc in _context.StudioClass
                          join me in _context.Members on sc.ID equals me.MembersID
                          //where sc. == this.HttpContext.User.Identity.Name
                          //orderby p.Name, p.ProjectNo
                          select new Lesson { StudioClass = sc, Members = me };
            ////Ouery-Join
            return View(await gymContext.ToListAsync());
        }

        //Odeya&maya-Search
        public async Task<IActionResult> Search(string day)
        {
            var results = from l in _context.Lesson
                          where l.LessonDay.Contains(day)
                          select l;
            //var results2 = _context.Lesson.Where(le =>le.LessonDay.Contains(day)) ;
            return View("Index", await results.ToListAsync());
        }
        //Odeya&maya-Search

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .Include(l => l.StudioClass)
                .Include(l => l.Trainer)
                .Include(l => l.Members) //Join
                .FirstOrDefaultAsync(m => m.LessonID == id);

            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);

        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["StudioClassID"] = new SelectList(_context.StudioClass, "ID", "ID");
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainersID", "TrainersID");
            ViewData["MemberID"] = new SelectList(_context.Members, "MembersID", "MembersID"); //Join
            return View();
        }

        //Odeya-FacebookAPI
        public void facebook(string LessonDay)  //after we add aflight we posted in facebook
        {
            dynamic messagePost = new ExpandoObject();
            messagePost.message = "Manager Update:" +
                "Hey Everyone!We made a change in the date of a lesson! It's " + LessonDay;


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


        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LessonID,TrainerID,StudioClassID,MemberID,Capacity,LessonDay,LessonTime")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lesson);
                //Odeya-FacebookAPI
                facebook(lesson.LessonDay);
                //Odeya-FacebookAPI
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudioClassID"] = new SelectList(_context.StudioClass, "ID", "ID", lesson.StudioClassID);
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainersID", "TrainersID", lesson.TrainerID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MembersID", "MembersID"); //Join
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["StudioClassID"] = new SelectList(_context.StudioClass, "ID", "ID", lesson.StudioClassID);
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainersID", "TrainersID", lesson.TrainerID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MembersID", "MembersID"); //Join
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LessonID,TrainerID,StudioClassID,MemberID,Capacity,LessonDay,LessonTime")] Lesson lesson)
        {
            if (id != lesson.LessonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.LessonID))
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
            ViewData["StudioClassID"] = new SelectList(_context.StudioClass, "ID", "ID", lesson.StudioClassID);
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainersID", "TrainersID", lesson.TrainerID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MembersID", "MembersID"); //Join
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lesson
                .Include(l => l.StudioClass)
                .Include(l => l.Trainer)
                .Include(l => l.Members) ///Join
                .FirstOrDefaultAsync(m => m.LessonID == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lesson.FindAsync(id);
            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lesson.Any(e => e.LessonID == id);
        }
    }
}
