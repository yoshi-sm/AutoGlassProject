using Autoglass.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.Interfaces
{
    public interface IAutoglassContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
