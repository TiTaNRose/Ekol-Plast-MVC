using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace Ekol_Plast_MVC.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Внесете го Вашето име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Внесете ја Вашата е-пошта")]
        [EmailAddress(ErrorMessage = "Внесете валидна е-пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Внесете го Вашиот телефонски број")]
        public string Number { get; set; }


        [Required(ErrorMessage = "Внесете го Вашето барање")]
        public string Body { get; set; }

        [DataType(DataType.DateTime)] 
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
