
using Library.Demo.Domain;
using MediatR;

namespace Library.Demo.Application;

public class BookService : IBookService
{
    private readonly IMediator _mediator;

    public BookService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<BookDTO>> CreateBook(BookCreationDTO book)
    {
        var command = new CreateBookCommand(book.ISBN, book.Title, book.Subject, book.Publisher, book.Language, book.NumberOfPages);
        return await _mediator.Send(command);
    }
}
