using ElsherbinyTask05.API.Models;

namespace ElsherbinyTask05.API.Repositories;

public interface IBookRepository
{

    public IEnumerable<Book> GetAll();
    public Book GetById(Guid id);
    public void Add(Book book);
    public void Delete(Guid id);
    public void Update(Guid id);
    public void Save(); 
}
