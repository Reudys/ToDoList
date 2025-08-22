using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly DataContext _context;

        public ToDoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var todos = _context.ToDos.ToList();
            return View(todos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDo todo)
        {
            if (!ModelState.IsValid) return View(todo);
            _context.ToDos.Add(todo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var todo = _context.ToDos.Find(id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ToDo todo)
        {
            if (!ModelState.IsValid) return View(todo);
            _context.ToDos.Update(todo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var todo = _context.ToDos.Find(id);
            if (todo == null) return NotFound();
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var todo = _context.ToDos.Find(id);
            _context.ToDos.Remove(todo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Completed(int id)
        {
            var todos = _context.ToDos.Find(id);
            if (todos == null) return NotFound();
            if (!todos.IsCompleted) todos.IsCompleted = true;
            else todos.IsCompleted = false;
                _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
