
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

    //ToDo: shoult we return a valueObject instead?
    public async Task<Result<Guid>> CreateBook(BookCreationDTO book)
    {
        try
        {
            var command = new CreateBookCommand(book.ISBN, book.Title, book.Subject, book.Publisher, book.Language, book.NumberOfPages);
            var bookCreationResult = await _mediator.Send(command);
            return Result<Guid>.Success(bookCreationResult.Value);
        }
        catch (Exception e)
        {
            return Result<Guid>.FromException(e);
        }
    }
}
