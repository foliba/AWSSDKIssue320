namespace Issue320
{
    using Amazon.DynamoDBv2.DataModel;

    [DynamoDBTable("issue320-accounts")]
    public class AccountModel
    {
        [DynamoDBHashKey("ACC")]
        public string AccountId { get; set; }

        // some more
    }
}