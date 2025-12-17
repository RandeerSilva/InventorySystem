# Inventory Unit Tests

This project contains unit tests for the Inventory application, targeting .NET 10 and using C# 14.0. The tests cover application features and infrastructure components, ensuring correctness and reliability.

## Structure

- **Application Tests:** Located in `test/Inventory.UnitTests/Application/Features/Products/`, these tests validate product-related business logic.
- **Infrastructure Tests:** Located in `test/Inventory.UnitTests/Infrastructure/`, these tests verify repository and data access behaviors.

## Testing Frameworks

- [xUnit](https://xunit.net/) for test execution
- [NSubstitute](https://nsubstitute.github.io/) for mocking dependencies
- [FluentAssertions](https://fluentassertions.com/) for expressive assertions
- [Microsoft.EntityFrameworkCore.InMemory](https://learn.microsoft.com/en-us/ef/core/testing/in-memory) for in-memory database testing

## Running Tests

To run all tests, use the following command in the solution directory:

## Contributing

- Follow the coding standards defined in `.editorconfig` and `CONTRIBUTING.md`.
- Ensure all new tests pass before submitting a pull request.

## Notes

- Mocking of `DbSet<T>` is handled using a custom helper to ensure compatibility with NSubstitute.
- For integration tests, the in-memory provider is used to simulate database operations.

---