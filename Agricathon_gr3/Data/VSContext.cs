using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Agricathon_gr3.Models
{
    public class VSContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Person> PersonDB { get; set; }
        public DbSet<Project> ProjectDB { get; set; }
        public DbSet<PersProject> PersProjectDB { get; set; }
        public DbSet<TypePerson> TypePersonDB { get; set; }
        public DbSet<TypeResult> TypeResultDB { get; set; }
        public DbSet<Phase> PhaseDB { get; set; }
        public DbSet<Questionnaire> QuestionnaireDB { get; set; }
        public DbSet<Anwser> AnwserDB { get; set; }
        public DbSet<Question> QuestionDB { get; set; }
        public DbSet<Option> OptionDB { get; set; }

        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        //{
        //    builder.AddConsole();
        //});

        public static string ConnectionString { get; set; } = @"Data Source=153.109.124.35;Initial Catalog=Dura-farm;Integrated Security=False;User Id=6231db;Password=Pwd46231.;MultipleActiveResultSets=True";

        public VSContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
            //options.UseLoggerFactory(MyLoggerFactory).EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PersProject>().HasKey(x => new { x.PersonId, x.ProjetId });
            builder.Entity<Anwser>().HasKey(x => new { x.QuestionId, x.QuestionnaireId });

        }

    }
}
