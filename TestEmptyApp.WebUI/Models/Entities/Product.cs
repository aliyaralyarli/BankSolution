using System;
using System.ComponentModel.DataAnnotations;

namespace TestEmptyApp.WebUI.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public int CardNumber { get; set; }

        [Required]
        public string ExpireDate { get; set; }
    }
}
