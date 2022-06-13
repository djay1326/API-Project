using Dal;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Leaves
{
    public class BookRepository : IBookRepository
    {
        private readonly HelperlandContextData _DbContext;

        public BookRepository(HelperlandContextData DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<Book> Create(Book book)
        {
            _DbContext.Books.Add(book);
            await _DbContext.SaveChangesAsync();
            return book;
        }

        public async Task Delete(int id)
        {
            var bookDelete = await _DbContext.Books.FindAsync(id);
            _DbContext.Books.Remove(bookDelete);
            await _DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _DbContext.Books.ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await _DbContext.Books.FindAsync(id);
        }

        public async Task Update(Book book)
        {
            _DbContext.Entry(book).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
        }
    }
}
