using Autoglass.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Infra.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id_Produto");

            builder.Property(x => x.Descricao)
                .HasColumnName("Descricao");

            builder.Property(x => x.Ativo)
                .HasColumnName("Fl_Ativo");

            builder.Property(x => x.DataFabricacao)
                .HasColumnName("Dt_Fabricacao");

            builder.Property(x => x.DataValidade)
                .HasColumnName("Dt_Validade");

            builder.Property(x => x.FornecedorId)
                .HasColumnName("Id_Fornecedor");
        }
    }
}
