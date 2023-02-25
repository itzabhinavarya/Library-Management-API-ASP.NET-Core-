using Book_Store_Application.Models;
using Book_Store_Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBookAsync();
            return Ok(books);
        }
        [HttpGet("FindBook/{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();    
            return Ok(book);
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel bookModel)
        {
            var id = await _bookRepository.AddBookAsync(bookModel);
            return CreatedAtAction(nameof(GetBookById),new { id = id , controller = "books"} , $"Book Added Successfully.\nYour Book Id Is : {id}");
        }
        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookModel bookModel,[FromRoute] int id)
        {
            await _bookRepository.UpdateBookAsync(id,bookModel);
            return Ok("Book Updated Successfully....");
        }
        [HttpPatch("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] int id)
        {
            await _bookRepository.UpdateBookPatchAsync(id, bookModel);
            return Ok("Book Updated Successfully....");
        }
        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok("Book Deleted Successfully....");
        }

    }
}
