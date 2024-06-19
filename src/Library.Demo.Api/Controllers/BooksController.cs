using Library.Demo.Application;
using Microsoft.AspNetCore.Mvc;

namespace Library.Demo.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetBookAsync()
    {
        return Ok("Books..");
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateBookAsync(BookCreateRequest book)
    {
        return Created();
    }

}
