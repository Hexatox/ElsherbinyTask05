using ElsherbinyTask05.API.Data;
using ElsherbinyTask05.API.Models;

namespace ElsherbinyTask05.API.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Book book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book));

        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            throw new InvalidOperationException($"Book with ID {id} not found.");

        _context.Books.Remove(book);
        _context.SaveChanges();
    }

    public IEnumerable<Book> GetAll()
    {
        return _context.Books.ToList();
    }

    public Book GetById(Guid id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            throw new InvalidOperationException($"Book with ID {id} not found.");

        return book;
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Update(Guid id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            throw new InvalidOperationException($"Book with ID {id} not found.");

        _context.SaveChanges();
    }
}
