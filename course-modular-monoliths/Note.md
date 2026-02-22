### 1. Application Setup

```
mkdir src
cd src
dotnet new sln -n RiverBooks

dotnet new webapi -n RiverBooks.Web
dotnet sln RiverBooks.sln add .\RiverBooks.Web\RiverBooks.Web.csproj

dotnet new classlib -n RiverBooks.Books -o RiverBooks.Books
dotnet sln RiverBooks.sln add .\RiverBooks.Books\RiverBooks.Books.csproj
```

- add a project reference from web to RiverBooks.Books

### 2. The Books Module - Add Persistence Abstractions

```
namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyBookRepository
{
    Task AddAsync(Book book);
    Task DeleteAsync(Book book);
    Task SaveChangesAsync();
}
```

- One question you may have is, why are we using book and not our DTO here?
- The reason is that our repository abstraction is part of our domain model. So it should only work with domain model
  types, like book.
- DTOs aren't part of the domain model typically. It's going to be the application services layers job to map between
  our domain model types and our application types, specifically these DTOs.

### 3. The Books Module - Create Basic Services

- Now that we have a repository abstraction, our book service application service can use this to get its list of books.
- Remember, the application service, this book service, supports the application, which in this case is our Web API
  application. As such, it's going to return DTOs in a form that the application can either use directly or with minimal
  additional mapping.

### 4. The Books Module - Integrating with EF Core

- Now, we are ready to add the persistence implementation using entity framework core. For this, we're going to need to
  add a db context. This will be specific to books, so we're going to call it book db context. We'll need a db set of
  books, and we'll want to set the default schema to be books as well.
- At this point, we're ready to wire up any framework migrations so that our data store is ready to use. So for that,
  I'm going to drop to the command line and we're going to do that from, in my case, PowerShell.

- under \RiverBooks.Web:

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

```
dotnet ef database update -c UsersDbContext
```