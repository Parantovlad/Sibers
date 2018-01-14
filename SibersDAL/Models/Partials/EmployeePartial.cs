using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersDAL.Models
{
    public partial class Employee
    {
        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{Surname} {Name} {Patronymic}";
    }
}
