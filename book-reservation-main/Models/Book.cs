using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Reservation.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Genre { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public bool Featured { get; set; }
        public string ImageUrl { get; set; }
        public string Edition { get; set; }
        public int Year { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        [Required]
        public int UserId { get; set; }
    }

public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int UserId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
    }
    public static class DummyBooks
    {
        public static List<Book> All => new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Genre = "Classic",
                Pages = 218,
                Price = 10.99M,
                Featured = true,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1925,
                Publisher = "Scribner",
                ISBN = "9780743273565"
            },
            new Book
            {
                Id = 2,
                Title = "1984",
                Author = "George Orwell",
                Genre = "Dystopian",
                Pages = 328,
                Price = 9.99M,
                Featured = false,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1949,
                Publisher = "Secker & Warburg",
                ISBN = "9780451524935"
            },
            new Book
            {
                Id = 3,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Genre = "Fiction",
                Pages = 281,
                Price = 12.99M,
                Featured = true,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1960,
                Publisher = "J.B. Lippincott & Co.",
                ISBN = "9780061120084"
            },
            new Book
            {
                Id = 4,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Genre = "Romance",
                Pages = 279,
                Price = 8.99M,
                Featured = false,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "2nd",
                Year = 1813,
                Publisher = "T. Egerton",
                ISBN = "9780141199078"
            },
            new Book
            {
                Id = 5,
                Title = "Moby Dick",
                Author = "Herman Melville",
                Genre = "Adventure",
                Pages = 635,
                Price = 11.99M,
                Featured = false,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1851,
                Publisher = "Harper & Brothers",
                ISBN = "9781503280786"
            },
            new Book
            {
                Id = 6,
                Title = "The Catcher in the Rye",
                Author = "J.D. Salinger",
                Genre = "Fiction",
                Pages = 277,
                Price = 10.50M,
                Featured = false,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1951,
                Publisher = "Little, Brown and Company",
                ISBN = "9780316769488"
            },
            new Book
            {
                Id = 7,
                Title = "Brave New World",
                Author = "Aldous Huxley",
                Genre = "Science Fiction",
                Pages = 311,
                Price = 9.99M,
                Featured = false,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1932,
                Publisher = "Chatto & Windus",
                ISBN = "9780060850524"
            },
            new Book
            {
                Id = 8,
                Title = "Jane Eyre",
                Author = "Charlotte Brontë",
                Genre = "Gothic",
                Pages = 500,
                Price = 10.99M,
                Featured = false,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1847,
                Publisher = "Smith, Elder & Co.",
                ISBN = "9780142437209"
            },
            new Book
            {
                Id = 9,
                Title = "Animal Farm",
                Author = "George Orwell",
                Genre = "Satire",
                Pages = 112,
                Price = 7.99M,
                Featured = true,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1945,
                Publisher = "Secker & Warburg",
                ISBN = "9780451526342"
            },
            new Book
            {
                Id = 10,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                Pages = 310,
                Price = 13.99M,
                Featured = true,
                ImageUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L.jpg",
                Edition = "1st",
                Year = 1937,
                Publisher = "George Allen & Unwin",
                ISBN = "9780547928227"
            }
        };
    }
}