using DriveThruRpgApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

// Application Key used to interact with the DriveThruRPG API.
string apiApplicationKey = config["DriveThruRpgApplicationKey"];

if (string.IsNullOrWhiteSpace(apiApplicationKey))
{
    Console.WriteLine("Please enter an Application Key from DriveThruRPG.");
    var suppliedApplicationKey = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(suppliedApplicationKey))
    {
        Console.WriteLine("No application key entered. Exiting.");
        return;
    }
    apiApplicationKey = suppliedApplicationKey.Trim();
}

var httpClient = new HttpClient();

var apiClient = new ApiClient(httpClient, apiApplicationKey);

if (!apiClient.GetToken())
{
    Console.WriteLine("Unable to get DriveThruRPG access token.");
}

var getProducts = true;
var startPage = 1;
var productsPerPage = 25;
var products = new List<ApiProductMessageResponse>();

while (getProducts)
{
    var pulledProducts = apiClient.GetProducts(startPage, productsPerPage);
    if (pulledProducts != null && pulledProducts.Data != null && pulledProducts.Data.Any())
    {
        products.AddRange(pulledProducts.Data.Select(d => d.Products).ToList());
        startPage++;
    }
    else
    {
        getProducts = false;
    }

    if (startPage >= 5)
    {
        getProducts = false;
    }
}

Console.WriteLine(JsonSerializer.Serialize(products));

Console.WriteLine("Application complete. Press Enter to exit.");
_ = Console.ReadLine();
