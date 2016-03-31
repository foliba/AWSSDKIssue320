using Funq;
using ServiceStack;

namespace Issue320
{
    using System.Configuration;

    public class AppHost : AppHostBase
    {
        public AppHost()
            : base("Issue320", typeof(AppHost).Assembly)
        {

            typeof(LoginRequestDTO).AddAttributes(new RouteAttribute("/account/login", "POST"));
        }


        public override void Configure(Container container)
        {
            container.RegisterAutoWired<Utility>();
            container.Register(c => new DynamoDbWrapper(c.Resolve<Utility>(), ConfigurationManager.AppSettings["Issue320DynamoDbRegion"]));

            container.Resolve<DynamoDbWrapper>().CreateDbTable();
        }
    }
}