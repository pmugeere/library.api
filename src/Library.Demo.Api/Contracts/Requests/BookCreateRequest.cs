namespace Library.Demo.Api.Controllers;

public record BookCreateRequest(
    string ISBN,
    string Title,
    string Subject,
    string Publisher,
    string Language,
    int NumberOfPages
);