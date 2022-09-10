using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args).Build();

IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

// Application Key used to interact with the DriveThruRPG API.
string apiApplicationKey = config["DriveThruRpgApplicationKey"];

if (string.IsNullOrWhiteSpace(apiApplicationKey)) {
    Console.WriteLine("Please enter an Application Key from DriveThruRPG.");
    var suppliedApplicationKey = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(suppliedApplicationKey)) {
        Console.WriteLine("No application key entered. Exiting.");
        return;
    }
    apiApplicationKey = suppliedApplicationKey.Trim();
}

var httpClient = new HttpClient();

var apiClient = new ApiClient(httpClient, apiApplicationKey);

if (!apiClient.GetToken()) {
    Console.WriteLine("Unable to get DriveThruRPG access token.");
}

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(apiApplicationKey);
