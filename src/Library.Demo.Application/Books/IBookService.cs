namespace Library.Demo.Application;

public interface IBookService
{
    public Task<bool> CreateBook(BookCreationDTO book);
}
