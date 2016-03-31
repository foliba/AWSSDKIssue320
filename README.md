# ServiceStackIssue320
A small test project to reproduce ServiceStack issue 320

How to set up this solution and reproduce the unhandled ThroughputExceededException:


- Open Issue320.sln in VisualStudio 2013+


- Notice that the AWSCredentials.config is missing, you need to create it. We have provided you with a template for that: AWSCredentials.config.template; just insert your AWSAccessKey, AWSSecretKey and specify your preferred AWS Region in which a single table will be created.


- Upon starting the project it will create a table "issue320-accounts". (you can change this in the AccountModel.cs file)


- Provisioned write capacity is set incode to 1 per second. (you can change this in DynamoDbWrapper.cs)


- Run the stressTestForIssue320.sh - It connects to the localhost server and sends 5,000 requests.


- VisualStudio will at some point report an uncaught ThroughputExceededException.