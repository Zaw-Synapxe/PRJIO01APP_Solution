// See https://aka.ms/new-console-template for more information
using ConsumeWebAPI.ConsoleApp;
using System.Text.Json;

Console.WriteLine("Hello, World!");


//https://jsonplaceholder.typicode.com/posts/1 



using (var client = new HttpClient())
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    //HTTP GET
    var response = await client.GetAsync("posts/1");
    var result = await response.Content.ReadAsStringAsync();

    //Console.WriteLine(content);

    var post = JsonSerializer.Deserialize<Post>(result);
    Console.WriteLine(post.ToString());
}



// -------------------------------------------
var apiUrl = "https://localhost:44316/WeatherForecast"; // Example API endpoint

using var client2 = new HttpClient();

try
{
    // Making the GET request
    var response = await client2.GetAsync(apiUrl);

    // Ensure the request was successful
    response.EnsureSuccessStatusCode();

    // Read the response content as a string
    var content = await response.Content.ReadAsStringAsync();

    Console.WriteLine(content);

    Console.ReadKey();

}
catch (HttpRequestException e)
{
    Console.WriteLine($"Request error: {e.Message}");
}

// -------------------------------------------