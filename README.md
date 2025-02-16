# DriveThruRPG API Application for .NET 8
This is a .NET 8 console application for working with the DriveThruRPG API.

## Resources
- [DriveThruRPG](https://www.drivethrurpg.com/)
- [Create an Application Key in Account Settings](https://www.drivethrurpg.com/en/account/settings)
- https://github.com/jramboz/DTRPG_API
- https://github.com/glujan/drpg

## Building
- Requires [the .NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) for your operating system of choice.
- Run `dotnet build` or `dotnet publish` from the **DriveThruRpgApi** directory.

## Running
- Update `DriveThruRpgApplicationKey` in appsettings.json with the Application Key you've created on DriveThruRPG. If you do not want to enter an application key in this file, you will be prompted for one when the application starts.
- Run `dotnet run` from the **DriveThruRpgApi** directory.
    - Or `dotnet publish` and run the application from the published directory.

## Another option
From a web browser, when logged in, you can also access the following page and copy the returned JSON. This should contain all your purchased products, with some handy links that include order number information.

```
https://www.drivethrurpg.com/api/products/mylibrary/search?show_all=1&draw=9&columns[0][data]=null&columns[0][name]=&columns[0][searchable]=false&columns[0][orderable]=false&columns[0][search][value]=&columns[0][search][regex]=false&columns[1][data]=product.title&columns[1][name]=name&columns[1][searchable]=true&columns[1][orderable]=true&columns[1][search][value]=&columns[1][search][regex]=false&order[0][column]=1&order[0][dir]=asc&start=0&length=-1&search[value]=&search[regex]=false&show_archived=false&show_all=0&show_new=&show_updated=0&filter_string=&oneclick=true
```
