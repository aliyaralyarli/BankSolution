using System;
using System.ComponentModel.DataAnnotations;

namespace TestEmptyApp.WebUI.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AnswerDate { get; set; }
        public int AnsweredByUserUserId { get; set; }
    }
}
