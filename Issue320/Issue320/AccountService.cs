namespace Issue320
{
    using System.Threading.Tasks;

    using ServiceStack;


    public class AccountService : Service
    {
        private readonly DynamoDbWrapper m_dynamoDbWrapper;

        public AccountService(DynamoDbWrapper dynamoDbWrapper)
        {
            m_dynamoDbWrapper = dynamoDbWrapper;
        }

        public LoginResponseDTO Post(LoginRequestDTO requestDto)
        {
            // let's not do an actual login here but just perform a write operation on the DB

            // sync
            //m_dynamoDbWrapper.Save(new AccountModel { AccountId = requestDto.SomeId });

            // async
            Task.Run(() => m_dynamoDbWrapper.SaveAsync(new AccountModel { AccountId = requestDto.SomeId }));


            return new LoginResponseDTO // does not really matter
                       {
                           UserToken = "Token_" + requestDto.SomeId,
                           ValidDuration = 60 * 60
                       };
        }
    }
}