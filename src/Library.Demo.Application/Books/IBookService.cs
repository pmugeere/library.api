namespace Library.Demo.Application;

public interface IBookService
{
    public Task<Result<BookDTO>> CreateBook(BookCreationDTO book);
}
