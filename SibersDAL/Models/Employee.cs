using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersDAL.Models
{
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(50)]
        public string Patronymic { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual ICollection<ProjectEmployees> ProjectEmployees { get; set; } = new HashSet<ProjectEmployees>();
    }
}
