using Dal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Leaves
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get();

        Task<Book> Get(int id);
        Task<Book> Create(Book book);
        Task Update(Book book);
        Task Delete(int id);
    }
}
