using FluentAssertions;

namespace Library.Demo.Domain.Tests;

public class BookTests
{
    [Fact]
    public void Given_Book_details_Should_Create_New_Book(){
        string isbn = "";
        string title = "";
        string language = "";
        int numberOfPages = 360;
        var items = new List<BookItem> ();
        
        var book = Book.CreateNew(isbn,title,language,numberOfPages,items);

        book.ISBN.Should().Be(isbn);
        book.Title.Should().Be(title);
        book.Language.Should().Be(language);
        book.NumberOfPages.Should().Be(numberOfPages);  
        book.BookItems.Count().Should().Be(0);
    }
    
    [Fact]
    public void Given_DateOfPurchase_AddBookItem_Should_Add_New_BookItem()
    {
        var dateOfPurchase = DateTime.UtcNow;
        var book = Book.CreateNew("test-isbn","some-title","test-language",360,[]);
        book.AddBookItem(dateOfPurchase);

        book.BookItems.Count.Should().Be(1);
        book.BookItems.ToList()[0].DateOfPurchase.Should().Be(dateOfPurchase);
    }
}
