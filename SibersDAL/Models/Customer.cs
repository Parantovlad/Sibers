using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersDAL.Models
{
    public partial class Customer
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = "Customer")]
        public string Name { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
