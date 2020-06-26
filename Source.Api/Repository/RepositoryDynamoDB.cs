using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Loja.DataAcess;
using Loja.Interfaces;
using Loja.Model;
using Microsoft.Extensions.Logging;

namespace Loja.Repository
{
    public class RepositoryDynamoDB: IRepository
    {
        private readonly ILogger _logger;  
        public RepositoryDynamoDB(ILoggerFactory loggerFactory) 
        {  
            _logger = loggerFactory.CreateLogger("DynamoDBProvider");  
        }  
        public async Task AdicionarPeca(Peça peca) 
        {  
            var contexto = new DynamoDBContext(DynamoDBInitializer.client);  
            peca.Id = Guid.NewGuid().ToString();  
            peca.Ativa = true;  
            await contexto.SaveAsync<Peça>(peca);  
        }  
        public async Task AtualizarPeca(Peça peca) 
        {  
            var contexto = new DynamoDBContext(DynamoDBInitializer.client);  
            List <ScanCondition> condicao = new List <ScanCondition> ();  
            condicao.Add(new ScanCondition("id", ScanOperator.Equal, peca.Id));  
            var todosDocumentos = await contexto.ScanAsync<Peça>(condicao).GetRemainingAsync();  
            var estadoEditado = todosDocumentos.FirstOrDefault();  
            if (estadoEditado != null)
             {  
                estadoEditado = peca;  
                await contexto.SaveAsync <Peça> (estadoEditado);  
            }  
        }  
        public async Task DeletarPeca(string id) 
        {  
            const string nomeDaTabela = "Peca";  
            var requisicao = new DeleteItemRequest {  
                TableName = nomeDaTabela,  
                    Key = new Dictionary < string, AttributeValue > () {  
                        {  
                            "id",  
                            new AttributeValue {  
                                S = id  
                            }  
                        }  
                    }  
            };  
            var resposta = await DynamoDBInitializer.client.DeleteItemAsync(requisicao);  
        }  
        public async Task <Peça> ObterRegistroPorId(string id) 
        {  
            var contexto = new DynamoDBContext(DynamoDBInitializer.client);  
            List <ScanCondition> condicao = new List <ScanCondition> ();  
            condicao.Add(new ScanCondition("Id", ScanOperator.Equal, id));  
            var obterTodosDocumentos = await contexto.ScanAsync<Peça>(condicao).GetRemainingAsync();  
            var peca = obterTodosDocumentos.FirstOrDefault();  
            return peca;  
        }  
        public async Task <IEnumerable<Peça>> ObterTodosRegistros() 
        {  
            var contexto = new DynamoDBContext(DynamoDBInitializer.client);  
            List < ScanCondition > condicao = new List < ScanCondition > ();  
            condicao.Add(new ScanCondition("Ativa", ScanOperator.Equal, true));  
            var obterTodosDocumentos = await contexto.ScanAsync<Peça>(condicao).GetRemainingAsync();  
            return obterTodosDocumentos;  
        }  
    }  
}  