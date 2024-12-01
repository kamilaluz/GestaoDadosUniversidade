using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DadosUniversitarios.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace DadosUniversitarios.Services
{
    public class ApplicationDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().Property(p => p.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Curso>().Property(p => p.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Disciplina>().Property(p => p.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Endereco>().Property(p => p.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Pessoa>().Property(p => p.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<TipoPessoa>().HasData(
                    new TipoPessoa { Id = 1, NomeTipo = "Aluno" },
                    new TipoPessoa { Id = 2, NomeTipo = "Professor" });
            modelBuilder.Entity<Empresa>().Property(p => p.Id).UseIdentityAlwaysColumn();
            modelBuilder.Entity<Contrato>().Property(p => p.Id).UseIdentityAlwaysColumn();
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }        
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<TipoPessoa> TiposPessoa { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Contrato> Fornecedores { get; set; }

    }
}
