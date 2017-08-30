using System;
using System.IO;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Json = Newtonsoft.Json;

namespace CSharpLambdaFunction
{

    public class LambdaHandler
    {

        [LambdaSerializer(typeof(JsonSerializer))]
        public string MyHandler(InputData data, ILambdaContext context)
        {
            var logger = context.Logger;
            logger.Log("received : " + data.Value);
            return data.Value;
        }

        [LambdaSerializer(typeof(JsonSerializer))]
        public async Task<List<Post>> PostsHandler(Object input, ILambdaContext context)
        {
            var url = "https://jsonplaceholder.typicode.com/posts";

            var client = new HttpClient();

            var response = await client.GetStringAsync(url);
            return Json.JsonConvert.DeserializeObject<List<Post>>(response);             

        }
    }
}