using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class User:BaseEntity
    {
        [Required(ErrorMessage ="Fullname is required")]
        [MaxLength(100,ErrorMessage ="Cannot exceed 100 characters")]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}
