using MediatR;

namespace Library.Demo.Application;

public record CreateBookCommand(
    string ISBN,
    string Title,
    string Subject,
    string Publisher,
    string Language,
    int NumberOfPages) : IRequest<Result<BookDTO>>
{

}
