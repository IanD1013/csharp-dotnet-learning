under \RiverBooks.Web:
```
dotnet tool install --global dotnet-ef
```
```
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.8
```
```
dotnet ef migrations add Initial 
-c BookDbContext 
-p ..\RiverBooks.Books\RiverBooks.Books.csproj 
-s .\RiverBooks.Web.csproj
-o Data/Migrations
```
```
dotnet ef database update
```
