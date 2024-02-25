using Autoglass.Aplicacao.Dto;
using Autoglass.Dominio.Entidades;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Produto, ProdutoDto>()
                .ForMember(x => x.Codigo, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.CodigoFornecedor, opt => opt.MapFrom(x => x.FornecedorId))
                .ForMember(x => x.FornecedorCNPJ, opt => opt.MapFrom(x => x.Fornecedor!.CNPJ))
                .ForMember(x => x.DescricaoFornecedor, opt => opt.MapFrom(x => x.Fornecedor!.Descricao));

            CreateMap<CriarProdutoDto, Produto>();
            CreateMap<EditarProdutoDto, Produto>();
        }
    }
}
