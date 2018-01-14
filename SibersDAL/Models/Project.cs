using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersDAL.Models
{
    public partial class Project
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Display(Name = "Project")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Contractor")]
        public int ContractorId { get; set; }
        [Required]
        [Display(Name = "Manager")]
        public int ManagerId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yyyy}")]
        public DateTime EndDate { get; set; }
        [Range(0,100)]
        public int? Priority { get; set; }
        [StringLength(250)]
        public string Comment { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractor { get; set; }
        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }
        public virtual ICollection<ProjectEmployees> ProjectEmployees { get; set; } = new HashSet<ProjectEmployees>();
    }
}
