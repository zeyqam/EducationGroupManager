using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class Education:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Color { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Group>Groups { get; set; }
    }
}
