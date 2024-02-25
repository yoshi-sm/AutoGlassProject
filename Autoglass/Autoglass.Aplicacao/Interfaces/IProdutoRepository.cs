using Autoglass.Aplicacao.Dto;
using Autoglass.Aplicacao.RespostaController;
using Autoglass.Dominio.Entidades;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Aplicacao.Interfaces
{
    public interface IProdutoRepository
    {
        public Task<List<Produto>> GetAll();
        public Task<Produto?> GetById(int id);
        public Task<RespostaGenerica<Produto>> Create(Produto produto);
        public Task<Produto?> Update(int id, Produto produto);
        public Task<int> ExclusaoLogica(int id);
    }


}
