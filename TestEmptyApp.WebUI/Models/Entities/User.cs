using System;
using System.ComponentModel.DataAnnotations;

namespace TestEmptyApp.WebUI.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
