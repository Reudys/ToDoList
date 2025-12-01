using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ToDoList.Models;

namespace ToDoList.ViewModel
{
    public class ToDoViewData
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Titulo de la tarea es requerido")]
        [MaxLength(25, ErrorMessage = "El Titulo es demaciado largo")]
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [MaxLength(500, ErrorMessage = "La descripcion es demaciado larga")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
