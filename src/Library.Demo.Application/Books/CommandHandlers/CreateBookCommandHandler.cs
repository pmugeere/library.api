using Library.Demo.Domain;
using MediatR;

namespace Library.Demo.Application;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand,Result<BookDTO>>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<Result<BookDTO>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
