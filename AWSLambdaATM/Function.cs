using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaATM
{
    public class Function
    {
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var headers = new Dictionary<string, string> { { "content-type", "application/json" } };

            try
            {
                var atmRequest = JsonConvert.DeserializeObject<AtmRequest>(request.Body);
                if (atmRequest == null)
                {
                    return Createresponse(400, new { error = "Invalid or empty request body" }, headers);
                }
                var noteinatm = atmRequest.notesinatm ?? new Dictionary<int, int>
                {
                    { 500, 20 },
                    { 200, 20 },
                    { 100, 50 }
                };
                int totalamountinatm = TotalAmountInATM(noteinatm);
                switch (atmRequest.operation)
                {
                    case 'a':
                        return Createresponse(200, new { TotalBalance = totalamountinatm }, headers);
                    case 'w':
                        break;
                    default:
                        return Createresponse(400, new { error = "Enter valid char" }, headers);
                }

                int num = atmRequest.Amount;



                var despensenotes = new Dictionary<int, int> { { 500, 0 }, { 200, 0 }, { 100, 0 } };

                if (num < 0 || num % 100 != 0)
                {
                    return Createresponse(400, new { error = "Enter positive amount and multiple of 100" }, headers);
                }



                if (num > totalamountinatm)
                {
                    return Createresponse(400, new { error = "Insufficient cash!!" }, headers);
                }

                int remainingamount = num;
                List<int> denominations = new List<int> { 500, 200, 100 };

                foreach (int denom in denominations)
                {
                    if (!noteinatm.ContainsKey(denom)) continue;

                    if (remainingamount >= denom)
                    {
                        int notesneeded = remainingamount / denom;
                        int notesavailable = noteinatm[denom];
                        int notesToDispense = Math.Min(notesneeded, notesavailable);

                        remainingamount -= (notesToDispense * denom);
                        noteinatm[denom] -= notesToDispense;
                        despensenotes[denom] = notesToDispense;
                    }
                }

                if (remainingamount == 0)
                {
                    var successResult = new
                    {
                        message = "Success!!",
                        dispensedNotes = despensenotes,
                        remainingInventory = noteinatm
                    };
                    return Createresponse(200, successResult, headers);
                }
                else
                {
                    return Createresponse(400, new { error = "Cannot dispense change with available note denominations" }, headers);
                }
            }
            catch (JsonException e)
            {
                return Createresponse(400, new { error = $"An error occurred: {e.Message}" }, headers);
            }
        }

        private APIGatewayProxyResponse Createresponse(int statusCode, object body, Dictionary<string, string> headers)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = JsonConvert.SerializeObject(body),
                Headers = headers
            };
        }
        private int TotalAmountInATM(Dictionary<int, int> n)
        {
            int totalamountinatm = 0;
            foreach (KeyValuePair<int, int> k in n)
            {
                totalamountinatm += (k.Value * k.Key);
            }
            return totalamountinatm;
        }
    }
    public class AtmRequest()
    {
        public char operation { get; set; }
        public int Amount { get; set; }
        public Dictionary<int, int> notesinatm { get; set; }
    }
}