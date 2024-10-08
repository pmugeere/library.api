﻿using FluentAssertions;
using QuireHut.Demo.Domain;
using QuireHut.Demo.Domain.Books.Exceptions;

namespace QuireHut.Demo.Tests;
public class BookTests
{

    [Fact]
    public void CreateNewBook_WithRequiredParameters_ShouldReturnABook()
    {
        // Given
        var title = new Title("some-title");
        var subject = new Subject("test-subject");
        var author = Author.CreateNew("test-author", "test-bibliography");
        var edition = Edition.CreateNew(EditionId.Empty, new ISBN("test-isbn"), Format.HardPaper, "English", new Dimensions(23, 14, 45), 1000, 200, EditionStatus.Planned);

        // When
        var book = Book.CreateNew(title, subject, [edition], [author.Id]);

        // Then
        book.Should().NotBeNull();
        book.Title.Should().Be(title);
        book.Subject.Should().Be(subject);
        book.AuthorIds.Should().Contain(author.Id);
        book.Editions.Should().NotBeEmpty();
        book.Editions.Should().Contain(x=>x.Id==edition.Id);
    }

    [Fact]
    public void CreateNewBook_WithNoEdition_ShouldThrowAnInvalidBookException()
    {
        // Given
        var isbn = new ISBN("test-isbn");
        var title = new Title("some-title");
        var subject = new Subject("test-subject");
        var author = Author.CreateNew("test-author", "test-bibliography");

        // When
        var act = () => Book.CreateNew(title, subject, [], [author.Id]);

        // Then
        act.Should().Throw<InvalidBookException>().Where(x => x.Message.Equals("A book must have at least 1 edition."));

    }

    [Fact]
    public void CreateNewBook_WithNoAuthor_ShouldThrowAnInvalidBookException()
    {
        // Given
        var isbn = new ISBN("test-isbn");
        var title = new Title("some-title");
        var subject = new Subject("test-subject");
        var edition = Edition.CreateNew(EditionId.Empty, isbn, Format.HardPaper, "English", new Dimensions(23, 14, 45), 1000, 200, EditionStatus.Planned);

        // When
        var act = () => Book.CreateNew(title, subject, [edition], []);

        // Then
        act.Should().Throw<InvalidBookException>().Where(x => x.Message.Equals("A book must have at least 1 author."));

    }

    [Fact]
    public void AddEdition_ToABook_ShouldIncludeTheNewEditionInTheBookEditions()
    {
        var editionToAdd = Edition.CreateNew(EditionId.Empty, new("test-isbn2"), Format.HardPaper, "test-language", new Dimensions(2, 5, 6), 1000, 200, EditionStatus.Planned);
        var bookEdition = Edition.CreateNew(EditionId.Empty, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 1000, 200, EditionStatus.Planned);
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [bookEdition], [AuthorId.CreateNew()]);
        book.AddEdition(editionToAdd);

        book.Editions.Should().Contain(x=>x.Id==editionToAdd.Id);
    }


    [Fact]
    public void AddEdition_ThatHasADuplicateISBN_ShouldThrowAnInvalidBookException()
    {
        var editionToAdd = Edition.CreateNew(EditionId.Empty, new("test-isbn"), Format.HardPaper, "test-language", new Dimensions(2, 5, 6), 1000, 200, EditionStatus.Planned);
        var existingBookEdition = Edition.CreateNew(EditionId.Empty, new("test-isbn"), Format.AudioBook, "test-language3", new Dimensions(), 1000, 200, EditionStatus.Planned);

        var author = Author.CreateNew("test-author", "test-bibliography");
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [existingBookEdition], [author.Id]);
        var action = () => book.AddEdition(editionToAdd);

        action.Should().Throw<InvalidBookException>().Where(x => x.Message.Equals("A book cannot editions with duplicate ISBN."));
    }

    [Fact]
    public void SetEditionStatus_ofAnExistingEdition_ShouldChangeTheStatusOfTheEdition()
    {
        var editionId = EditionId.CreateNew();
        var statusToUpdateTo = EditionStatus.Published;
        var bookEdition = Edition.CreateNew(editionId, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 1000, 200, EditionStatus.Planned);
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [bookEdition], [AuthorId.CreateNew()]);

        book.SetEditionStatus(editionId, statusToUpdateTo);

        book.Editions?.FirstOrDefault(e => e.Id == editionId)?.Status.Should().Be(statusToUpdateTo);
    }

    [Fact]
    public void UpdatePrice_ofAnExistingEdition_ShouldUpdateThePriceOfTheEdition()
    {
        var priceToUpdateTo = 1000;
        var editionId = EditionId.CreateNew();
        var bookEdition = Edition.CreateNew(editionId, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 2000, 200, EditionStatus.Planned);
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [bookEdition], [AuthorId.CreateNew()]);

        book.UpdatePrice(editionId, priceToUpdateTo);

        book.Editions?.FirstOrDefault(e => e.Id == editionId)?.Price.Should().Be(priceToUpdateTo);
    }

    [Fact]
    public void UpdateStock_ofAnExistingEdition_ShouldUpdateThePriceOfTheEdition()
    {
        var stockToUpdateTo = 1000;
        var editionId = EditionId.CreateNew();
        var bookEdition = Edition.CreateNew(editionId, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 2000, 200, EditionStatus.Planned);
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [bookEdition], [AuthorId.CreateNew()]);

        book.UpdateStock(editionId, stockToUpdateTo);

        book.Editions?.FirstOrDefault(e => e.Id == editionId)?.Stock.Should().Be(stockToUpdateTo);
    }

    [Fact]
    public void AddGenre_ToBook_ShouldIncludeTheGenreInTheBookGenres()
    {
        var genreToAdd = GenreId.CreateNew();
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [Edition.CreateNew(EditionId.Empty, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 2000, 200, EditionStatus.Planned)], [AuthorId.CreateNew()]);

        book.AddGenre(genreToAdd);

        book.GenreIds.Should().Contain(genreToAdd);
    }

    [Fact]
    public void AddGenre_ThatAlreadyExists_ShouldThrowAnInvalidBookException()
    {
        var genreToAdd = GenreId.CreateNew();
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [Edition.CreateNew(EditionId.Empty, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 2000, 200, EditionStatus.Planned)], [AuthorId.CreateNew()]);
        book.AddGenre(genreToAdd);

        var action = () => book.AddGenre(genreToAdd);

        action.Should().Throw<InvalidBookException>().Where(x => x.Message.Equals("A book cannot have duplicate genres."));
    }

    [Fact]
    public void RemoveGenre_FromBook_ShouldRemoveTheGenreFromTheBookGenres()
    {
        var genreToRemove = GenreId.CreateNew();
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [Edition.CreateNew(EditionId.Empty, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 2000, 200, EditionStatus.Planned)], [AuthorId.CreateNew()]);
        book.AddGenre(genreToRemove);

        book.RemoveGenre(genreToRemove);

        book.GenreIds.Should().NotContain(genreToRemove);
    }

    [Fact]
    public void RemoveEdition_FromBook_ShouldRemoveTheEditionFromTheBookEditions()
    {
        var editionId = EditionId.CreateNew();
        var bookEdition = Edition.CreateNew(editionId, new("test-isbn1"), Format.AudioBook, "test-language3", new Dimensions(), 2000, 200, EditionStatus.Planned);
        var book = Book.CreateNew(new Title("some-title"), new Subject("test-subject"), [bookEdition], [AuthorId.CreateNew()]);

        book.RemoveEdition(editionId);

        book.Editions.Should().NotContain(x=>x.Id==bookEdition.Id);
    }
}
