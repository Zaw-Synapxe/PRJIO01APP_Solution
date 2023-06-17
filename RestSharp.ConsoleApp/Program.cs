using RestSharp;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;

public class Program
{
    public static async Task Main()
    {
        // https://jasonwatmore.com/c-restsharp-http-get-request-examples-in-net#get-request-async-await

        //
        // send GET request with RestSharp
        var client = new RestClient("https://testapi.jasonwatmore.com");
        var request = new RestRequest("products/1");
        var response = client.ExecuteGet(request);

        // deserialize json string response to JsonNode object
        var data = JsonSerializer.Deserialize<JsonNode>(response.Content!)!;

        // output result
        Console.WriteLine($"---------------- json properties ---------------- id: {data["id"]} name: {data["name"]} ---------------- raw json data ---------------- {data} ");


        //
        // GET request using RestSharp with strongly typed response
        // send GET request with RestSharp
        // deserialize json string response to Product object
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var product = JsonSerializer.Deserialize<Product>(response.Content!, options)!;

        // output result
        Console.WriteLine($"---------------- json properties ---------------- id: {product.Id} ");


        //
        // GET request using RestSharp with error handling
        // send GET request with RestSharp
        // handle error
        if (!response.IsSuccessful)
        {
            Console.WriteLine($"ERROR: {response.ErrorException?.Message}");
            return;
        }

        // deserialize json string response to JsonNode object
        var data2 = JsonSerializer.Deserialize<JsonNode>(response.Content!)!;

        // output result
        Console.WriteLine($"---------------- json properties ---------------- id: {data2["id"]} name: {data2["name"]} ---------------- raw json data ---------------- {data2} ");


        //
        // GET request using RestSharp with async/await
        var response3 = await client.ExecuteGetAsync(request);
        // deserialize json string response to JsonNode object
        var data3 = JsonSerializer.Deserialize<JsonNode>(response3.Content!)!;



        // POST
        //var request = new RestRequest("products", Method.POST);
        //request.AddJsonBody(newProduct);
        //var response = client.Execute(request);

        // GET
        //var request = new RestRequest("products/{id}", Method.GET);
        //request.AddUrlSegment("id", productId.ToString());
        //var response = client.Execute<Product>(request);
        //var product = response.Data;

        //// PUT
        //var request = new RestRequest("products/{id}", Method.PUT);
        //request.AddUrlSegment("id", productId.ToString());
        //request.AddJsonBody(updatedProduct);
        //var response = client.Execute(request);

        // DELETE
        //var request = new RestRequest("products/{id}", Method.DELETE);
        //request.AddUrlSegment("id", productId.ToString());
        //var response = client.Execute(request);



        //
    }

    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}