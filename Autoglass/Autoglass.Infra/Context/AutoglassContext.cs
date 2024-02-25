using Autoglass.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autoglass.Aplicacao.Interfaces;

namespace Autoglass.Infra.Context
{
    public class AutoglassContext : DbContext, IAutoglassContext
    {
        public AutoglassContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
