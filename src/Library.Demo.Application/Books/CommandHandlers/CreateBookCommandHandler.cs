using Library.Demo.Domain;
using MediatR;

namespace Library.Demo.Application;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookId>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookId> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var book = Book.CreateNew(command.ISBN,command.Title,command.Subject,command.Publisher,command.Language,command.NumberOfPages);
        return await _bookRepository.Save(book);;
    }
}
