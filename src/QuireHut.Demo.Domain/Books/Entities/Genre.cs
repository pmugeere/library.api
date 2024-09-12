
namespace QuireHut.Demo.Domain;

public record Genre
{

    public GenreId Id { get; private set; } = GenreId.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; }
    private List<Book> Books { get;  set; } = [];
    public IReadOnlyList<Book> BooksList => Books.AsReadOnly();

    private Genre() { }

    public static Genre CreateNew(string name, string description)
    {
        return new Genre
        {
            Id = GenreId.CreateNew(),
            Name = name,
            Description = description
        };
    }

}
