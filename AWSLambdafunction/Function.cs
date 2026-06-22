using Amazon.Lambda.Core;
using System;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdafunction
{
    public class Function
    {
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request,ILambdaContext context)
        {
            try
            {
                Customer customer = JsonConvert.DeserializeObject<Customer>( request.Body);
                customer.customerid = Guid.NewGuid();
                customer.RegestrationDate = DateTime.Now;
                return new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body=JsonConvert.SerializeObject(customer),
                    Headers=new Dictionary<string,string>
                    {
                        {"content-type","application/json" }
                    }
                };
            }
            catch (JsonException e)
            {
                return new APIGatewayProxyResponse
                {
                    StatusCode=400,
                    Body=JsonConvert.SerializeObject(new {error=$"an error occured {e.Message}"}),
                    Headers = new Dictionary<string, string>
                    {
                        {"content-type","application/json" }
                    }
                };
            }
        }
    }
    public class Customer
    {
        public Guid? customerid { get; set; }
        public string customername { get; set; }
        public string email { get; set; }
        public string contactnumber { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public DateTime RegestrationDate { get; set; }
    }
}
