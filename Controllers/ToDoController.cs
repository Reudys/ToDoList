using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Reflection;
using ToDoList.Models;
using ToDoList.Services;
using ToDoList.ViewModel;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoService _service;

        public ToDoController(ToDoService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var todos = await _service.GetAll();
                return View(todos);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = "No se pudo conectar con la API: " + ex.Message;
                return View(new List<ToDo>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ToDoViewData todo)
        {
            if (!ModelState.IsValid) { return View(todo); }
            try
            {
                await _service.Create(todo);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex) {
                ModelState.AddModelError(string.Empty, "Error al Crear la tarea" + ex.Message);
                return View(todo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
