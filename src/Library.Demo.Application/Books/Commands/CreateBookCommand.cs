using Library.Demo.Domain;
using MediatR;

namespace Library.Demo.Application;

public record CreateBookCommand(
    string ISBN,
    string Title,
    string Subject,
    string Publisher,
    string Language,
    int NumberOfPages) : IRequest<BookId>{}


public static class CreateBookCommandMapper{
    public static Book MapToBook(this CreateBookCommand command){
        return Book.CreateNew(
            command.ISBN,
            command.Title,
            command.Subject,
            command.Publisher,
            command.Language,
            command.NumberOfPages);
    }
}
