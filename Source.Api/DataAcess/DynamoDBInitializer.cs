using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace Loja.DataAcess
{
    public class DynamoDBInitializer
    {
        private const string chaveDeAcesso = OMITIDO;  
        private const string chaveSecreta = OMITIDO;  
        public static AmazonDynamoDBClient client;  
        public static async Task InitializeDynamoDB() 
        {  
            string nomeDaTabela = "TabelaPeca";  
            string hashChave = "Id";  
            var credenciais = new BasicAWSCredentials(chaveDeAcesso, chaveSecreta);  

            client = new AmazonDynamoDBClient(credenciais, RegionEndpoint.USEast1);  

            var responseDaTabela = await client.ListTablesAsync();  
            if (!responseDaTabela.TableNames.Contains(nomeDaTabela)) 
            {  
                await client.CreateTableAsync(new CreateTableRequest 
                {  
                    TableName = nomeDaTabela,  
                        ProvisionedThroughput = new ProvisionedThroughput 
                        {  
                            ReadCapacityUnits = 3,  
                            WriteCapacityUnits = 1  
                        },  
                        KeySchema = new List <KeySchemaElement> 
                        {  
                            new KeySchemaElement 
                            {  
                                AttributeName = hashChave,  
                                KeyType = KeyType.HASH  
                            }  
                        },  
                        AttributeDefinitions = new List < AttributeDefinition > 
                        {  
                            new AttributeDefinition 
                            {  
                                AttributeName = hashChave, AttributeType = ScalarAttributeType.S  
                            }  
                        }  
                });  
                bool ehTabelaDisponivel = false;  
                while (!ehTabelaDisponivel) 
                {  
                    Thread.Sleep(5000);  
                    var statusDaTabela = await client.DescribeTableAsync(nomeDaTabela);  
                    ehTabelaDisponivel = statusDaTabela.Table.TableStatus == "ACTIVE";  
                }  
            }  
        }  
    }  
}  