using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Group:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }

        public int EducationId { get; set; }
        public Education Education { get; set; }
    }
}
