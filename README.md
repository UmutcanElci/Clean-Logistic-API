This repository is a sandbox project where I practice **Clean Architecture** principles. It serves as a playground for exploring new technologies and structural patterns.

**Note:** This is a **Work in Progress**. I prioritize core logic and architecture over a polished, production-ready state.

### Tech Stack
*   **Framework:** .NET 10 (Preview)
*   **Language:** C#
*   **Database:** PostgreSQL
*   **Security:** Configuration via `dotnet user-secrets` (No hardcoded credentials).

### Current Status & Known Issues
I am actively refactoring parts of the system. Please be aware of the following:
*   **GeocodingService:** This service is currently being implemented.
*   **Tests:** The `OrderService` tests will currently fail because the `GeocodingService` dependency hasn't been fully mocked/integrated yet. I am working on fixing this integration.
*   **Cleanup:** Default template files (like `WeatherForecast`) may still be present as I focus on the business logic first.

### Project Structure
The solution follows a strict Clean Architecture approach:
*   **Domain:** Core entities and business rules (Dependency-free).
*   **Application:** Use cases, interfaces, and logic processing.
*   **Infrastructure:** PostgreSQL context and external service implementations.
*   **API:** RESTful endpoints.
