# Unit of Work Reloaded - Modern, Lightweight, Test-Friendly Implementation in C#

A clean, modern, and practical implementation of the Unit of Work pattern in C#, designed for readability, testability, and flexibility.

This repository contains the sample project from the "Unit Of Work Reloaded - C#" article on Medium. 

[![Medium -  Click here](https://img.shields.io/badge/Medium-_Click_here-2ea44f?logo=medium)](https://medium.com/@martinstm/unit-of-work-reloaded-c-241422497fc7)

It demonstrates how the classic Unit of Work pattern can be redesigned to be simpler, more explicit, and easier to maintain while still supporting advanced use cases.

Unit of Work Reloaded was created to solve real-world pain points:
- Avoid over-engineered repositories and hidden dependencies
- Make unit testing easy and reliable
- Support both generic repositories and custom repositories
- Maintain a clear, explicit transaction boundary
- Keep database operations simple and predictable

This project uses Dapper for lightweight data access, but the core concepts can be applied with any ORM or database access layer.

## Key Features

- Unit of Work Reloaded - a modern take on the classic pattern
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

Dapper documentation: https://github.com/DapperLib/Dapper

## License

MIT License - feel free to use, adapt, or learn from this code.