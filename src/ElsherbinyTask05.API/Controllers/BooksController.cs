using ElsherbinyTask05.API.Constants;
using ElsherbinyTask05.API.Contracts.Req;
using ElsherbinyTask05.API.Models;
using ElsherbinyTask05.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElsherbinyTask05.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetAll()
    {
        var books = _bookRepository.GetAll();
        return Ok(books);
    }

    [HttpPost]
    [Authorize(Roles = $"{RolesConstants.Admin},{RolesConstants.Editor}")]
    public IActionResult Add([FromBody] BookAddReqDto data)
    {

        var book = new Book
        {
            Author = data.Author,
            Title = data.Title,
            PublishedDate = data.PublishedDate.ToUniversalTime()
        };
        _bookRepository.Add(book);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }
}
