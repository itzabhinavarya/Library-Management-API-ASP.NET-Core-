using AutoMapper;
using Book_Store_Application.Data;
using Book_Store_Application.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_Application.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> GetAllBookAsync()
        {
            //var records =await _context.Books.Select(x => new BookModel() {
            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Description
            //}).ToListAsync();

            //return records;
            var records = await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(records);
        }
        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            //var records = await _context.Books.Where(x => x.Id == bookId ).Select(x=>new BookModel()
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Description
            //}).FirstOrDefaultAsync();                                                                                                                         
             
            //return records;

            var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(book);  


        }
        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Name = bookModel.Name,
                Description = bookModel.Description
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }
        public async Task UpdateBookAsync(int bookId , BookModel bookModel)
        {
            //var book = await _context.Books.FindAsync(bookId);
            //if (book != null)
            //{
            //    book.Name = bookModel.Name;
            //    book.Description = bookModel.Description;

            //    await _context.SaveChangesAsync();
            //}
            var book = new Books()
            {
                Id = bookId,
                Name = bookModel.Name,
                Description = bookModel.Description
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            //return book.Id;
        }
        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if(book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteBookAsync(int bookId)
        {
            //var book = await _context.Books.FindAsync(bookId);
            var book = new Books() { Id = bookId };
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
