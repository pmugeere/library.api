namespace Library.Demo.Application;

public record BookCreationDTO(string ISBN,
    string Title,
    string Subject,
    string Publisher,
    string Language,
    int NumberOfPages);


    public record BookItemCreationDTO(string ISBN);