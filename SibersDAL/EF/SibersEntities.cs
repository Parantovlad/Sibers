namespace SibersDAL.EF
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SibersEntities : DbContext
    {
        public SibersEntities()
            : base("name=SibersConnection")
        {
        }

        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectEmployees> ProjectEmployees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEmployees>()
                .HasKey(p => new { p.ProjectId, p.EmployeeId });
            modelBuilder.Entity<ProjectEmployees>()
                .HasRequired(p => p.Project)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<ProjectEmployees>()
                .HasRequired(p => p.Employee)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}