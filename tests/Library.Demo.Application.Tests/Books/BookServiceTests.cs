using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using MediatR;

namespace Library.Demo.Application.Tests;

public class BookServiceTests
{
    private readonly IBookService _bookService;
    private readonly Fixture _fixture;
    private readonly IMediator _mediator;

    public BookServiceTests()
    {
        _fixture = new Fixture();
        _mediator = A.Fake<IMediator>();
        _bookService = new BookService(_mediator);
    }

    [Fact]
    public async Task Given_Book_details_CreateBook_Should_Create_New_Book()
    {
        var bookToBeCreated = _fixture.Create<BookCreationDTO>();

        await _bookService.CreateBook(bookToBeCreated);

        A.CallTo(() => _mediator.Send(A<CreateBookCommand>.That.Matches(x =>
            x.ISBN.Equals(bookToBeCreated.ISBN) &&
            x.Title.Equals(bookToBeCreated.Title) 
        ), CancellationToken.None)).MustHaveHappened();

    }
}
