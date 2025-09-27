# UrlShortener

A simple URL shortener service built with **.NET 9 / C# 11**, following **Clean Architecture** principles.

## 🚀 Features
- Shorten long URLs into short codes
- Expand short codes back to original URLs
- HTTP redirect when visiting a short code
- In-memory persistence (EF Core InMemory)
- In-memory caching for faster lookups
- Fully testable (xUnit + Moq)

## 📂 Project Structure
- **Domain** – core entities and interfaces  
- **Application** – business logic (UrlShortenerService)  
- **Infrastructure** – EF Core persistence & dependency injection  
- **Presentation** – ASP.NET Web API controllers  
- **Tests** – unit tests for service and controllers  