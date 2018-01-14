namespace SibersDAL.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SibersDAL.EF.SibersEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "SibersDAL.EF.SibersEntities";
        }

        protected override void Seed(SibersDAL.EF.SibersEntities context)
        {
            var contractors = new List<Contractor>
            {
                new Contractor { Id=1, Name="Parantovlad" },
                new Contractor { Id=2, Name="Other contractor" },
            };
            contractors.ForEach(x => context.Contractors.AddOrUpdate(c => new { c.Id, c.Name }, x));

            var customers = new List<Customer>
            {
                new Customer { Id=1, Name="Sibers" },
                new Customer { Id=2, Name="Other customer" },
            };
            customers.ForEach(x => context.Customers.AddOrUpdate(c => new { c.Id, c.Name }, x));

            var employees = new List<Employee>
            {
                new Employee { Id=1, Name="Anton", Surname="Parakhnevich", Patronymic="Vladimirovich", Email="parantovlad@mail.ru" },
                new Employee { Id=2, Name="Ekaterina", Surname="Meshkova", Patronymic="Yurievna", Email="meshkova@mail.ru" },
            };
            employees.ForEach(x => context.Employees.AddOrUpdate(e => new { e.Id, e.Name, e.Surname, e.Patronymic, e.Email }, x));

            var projects = new List<Project>
            {
                new Project
                {
                    Id = 1,
                    Name = "DataBase",
                    CustomerId = customers[0].Id,
                    ContractorId = contractors[0].Id,
                    ManagerId = employees[0].Id,
                    StartDate = DateTime.Now - TimeSpan.FromDays(4),
                    EndDate = DateTime.Now + TimeSpan.FromDays(4),
                    Priority = 100,
                    Comment = "Active"
                },
                new Project
                {
                    Id = 2,
                    Name = "Chat",
                    CustomerId = customers[1].Id,
                    ContractorId = contractors[1].Id,
                    ManagerId = employees[1].Id,
                    StartDate = DateTime.Now + TimeSpan.FromDays(4),
                    EndDate = DateTime.Now + TimeSpan.FromDays(14),
                    Priority = 0,
                    Comment = "Prepare"
                },
            };
            
            projects.ForEach(x => context.Projects.AddOrUpdate(p => new
            {
                p.Id,
                p.Name,
                p.CustomerId,
                p.ContractorId,
                p.ManagerId,
                p.StartDate,
                p.EndDate,
                p.Priority,
                p.Comment
            }, x));

            var projectEmployees = new List<ProjectEmployees>
            {
                new ProjectEmployees { ProjectId = projects[0].Id, EmployeeId = employees[0].Id },
                new ProjectEmployees { ProjectId = projects[0].Id, EmployeeId = employees[1].Id },
                new ProjectEmployees { ProjectId = projects[1].Id, EmployeeId = employees[0].Id },
            };

            projectEmployees.ForEach(x => context.ProjectEmployees.AddOrUpdate(p => new { p.ProjectId, p.EmployeeId }, x));
        }
    }
}
