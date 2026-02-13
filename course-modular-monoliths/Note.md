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

```
dotnet ef database update -- --environment Testing
```
"--" means: Stop parsing arguments for dotnet-ef, and pass everything after this to the application.

"-- --environment Testing" means:
- First -- → stop EF parsing
- Second part → send --environment Testing to your app
