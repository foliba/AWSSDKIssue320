using System;

namespace Issue320
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.Model;

    public class DynamoDbWrapper
    {
        private readonly Utility m_utility;

        private readonly string m_dynamoDbUrl;


        public DynamoDbWrapper(Utility utility, string dynamoDbUrl)
        {
            m_utility = utility;
            m_dynamoDbUrl = dynamoDbUrl;
        }

        private AmazonDynamoDBClient CreateDynamoDbClient()
        {
            var config = new AmazonDynamoDBConfig { ServiceURL = m_dynamoDbUrl };
            var client = new AmazonDynamoDBClient(config);
            return client;
        }

        private DynamoDBContext CreateDynamoDbContext(AmazonDynamoDBClient client)
        {
            try
            {
                var context = new DynamoDBContext(client);
                return context;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Save<T>(T entity) where T : class, new()
        {
            try
            {
                using (var client = CreateDynamoDbClient())
                {
                    using (var context = CreateDynamoDbContext(client))
                    {
                        context.Save(entity);
                    }
                }

                return true;
            }
            catch (Exception _)
            {
                return false;
            }
        }

        public async Task<bool> SaveAsync<T>(T entity) where T : class, new()
        {
            try
            {
                using (var client = CreateDynamoDbClient())
                {
                    using (var context = CreateDynamoDbContext(client))
                    {
                        await context.SaveAsync(entity).ConfigureAwait(false);
                        return true;
                    }
                }
            }
            catch (Exception _)
            {
                return false;
            }
        }


        public void CreateDbTable()
        {
            try
            {
                using (var client = CreateDynamoDbClient())
                {
                    var response =
                        client.CreateTable(
                            new CreateTableRequest
                                {
                                    TableName = GetTableNameFor<AccountModel>(),
                                    KeySchema = new List<KeySchemaElement> { new KeySchemaElement { AttributeName = "ACC", KeyType = KeyType.HASH } },
                                    AttributeDefinitions = new List<AttributeDefinition> { new AttributeDefinition("ACC", ScalarAttributeType.S), },
                                    ProvisionedThroughput = new ProvisionedThroughput(1, 1)
                                });

                    // omit checking response here
                }
            }
            catch (Exception _)
            {
                
            }
        }

        private string GetTableNameFor<T>()
        {
            return m_utility.GetAttributeValueOfType<DynamoDBTableAttribute, string>(typeof(T), x => x.TableName);
        }
    }
}