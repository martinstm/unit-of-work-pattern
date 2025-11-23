# Unit of Work 2.0 - Modern, Lightweight, Test-Friendly Implementation in C#

A clean, modern, and practical implementation of the Unit of Work pattern in C#, designed for readability, testability, and flexibility.

This repository contains the sample project from the "Unit of Work 2.0 - C#" article on Medium. It demonstrates how the classic Unit of Work pattern can be redesigned to be simpler, more explicit, and easier to maintain while still supporting advanced use cases.

Unit of Work 2.0 was created to solve real-world pain points:
- Avoid over-engineered repositories and hidden dependencies
- Make unit testing easy and reliable
- Support both generic repositories and custom repositories
- Maintain a clear, explicit transaction boundary
- Keep database operations simple and predictable

This project uses Dapper for lightweight data access, but the core concepts can be applied with any ORM or database access layer.

## Key Features

- Unit of Work 2.0 - a modern take on the classic pattern
- Generic repository (IRepository<T>) - automatically works for any entity
- Custom repositories - use specialized queries only when needed
- Repository factory - resolve any repository (generic or custom) easily
- Transaction support - begin, commit, and rollback explicitly
- DI-ready - integrates cleanly with .NET dependency injection
- Test-friendly - designed to be easily mocked and tested

## Example Usage

```csharp
// Using a custom repository
var user = await unitOfWork.Get<IUserRepository>()
                           .GetWithTeamsAsync("john@mail.com");

// Using the generic repository
var teams = await unitOfWork.Get<IRepository<Team>>()
                            .GetAllAsync();
```

Notice how the same Unit of Work instance seamlessly works with custom repositories and generic repositories. No magic, no extension methods, no hidden logic.


## 🛠 Tech Stack

- C# / .NET 10
- Dapper for data access
   - SQL Server (sample database)

Note: Inside of data folder there is a SQL script to create the database for this sample.


## References

Original Medium article: Unit of Work 2.0 - C#

Dapper documentation: https://github.com/DapperLib/Dapper

## License

MIT License - feel free to use, adapt, or learn from this code.