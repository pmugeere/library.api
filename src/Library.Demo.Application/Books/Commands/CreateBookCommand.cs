using MediatR;

namespace Library.Demo.Application;

public record CreateBookCommand(
    string ISBN,
    string Title,
    string Subject,
    string Publisher,
    string Language,
    int NumberOfPages) : IRequest
{

}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    public Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
