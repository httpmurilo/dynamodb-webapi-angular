using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loja.Model;

namespace Loja.Interfaces
{
    public interface IRepository
    {
         Task AdicionarPeca(Peça peca);  
        Task AtualizarPeca(Peça peca);  
        Task DeletarPeca(string id);  
        Task <Peça> ObterRegistroPorId(string id);  
        Task <IEnumerable<Peça>> ObterTodosRegistros();  
    }  
}  