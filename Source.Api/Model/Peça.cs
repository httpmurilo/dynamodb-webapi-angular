using System;
using Amazon.DynamoDBv2.DataModel;

namespace Loja.Model
{
    [DynamoDBTable("TabelaPeca")]  
    public class Peça
    {
        public string Id { get; set; }  
        public string Nome { get; set; }  
        public string Categoria { get; set; }  
        public bool Ativa { get; set; }   
    }
}
