using Bal.Leaves;
using Dal;
using Dal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiExplorerSettings(GroupName = "v3")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookInfoController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly HelperlandContextData _DbContext;
        public HttpContext httpContext => new HttpContextAccessor().HttpContext;
        public BookInfoController(HelperlandContextData DbContext, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _DbContext = DbContext;
        }

        
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            return await _bookRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var newbook = await _bookRepository.Create(book);
            //return CreatedAtAction(nameof(GetBooks), new { id = newbook.Id }, newbook);
            return Ok(newbook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            await _bookRepository.Update(book);
            //return NoContent();
            return Ok(await _bookRepository.Get());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookDelete = await _bookRepository.Get(id);
            if (bookDelete == null)
            {
                return NotFound();
            }
            await _bookRepository.Delete(bookDelete.Id);
            //return NoContent();
            var result = await _bookRepository.Get();
            return Ok(result);
        }
    }
}
