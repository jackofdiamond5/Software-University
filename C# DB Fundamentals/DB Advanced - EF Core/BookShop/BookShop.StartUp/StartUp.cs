namespace BookShop
{
    using System;
    using System.Text;
    using System.Linq;
    using BookShop.Data;
    using BookShop.Initializer;
    using System.Globalization;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(context);

                //var input = int.Parse(Console.ReadLine());

                Console.WriteLine($"{RemoveBooks(context)} books were deleted");
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            context.Books.RemoveRange(books);

            context.SaveChanges();

            return books.Length;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010);

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .OrderBy(c => c.CategoryBooks.Count)
                .ThenBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    TopBooks = c.CategoryBooks.Select(b => new
                        {
                            BookReleaseDate = b.Book.ReleaseDate,
                            BookName = b.Book.Title
                        })
                        .OrderByDescending(b => b.BookReleaseDate)
                        .Take(3)
                })
                .ToArray();

            var builder = new StringBuilder();

            foreach (var category in categories)
            {
                builder.AppendLine($"--{category.Name.Trim()}");
                foreach (var book in category.TopBooks)
                {
                    builder.AppendLine($"{book.BookName.Trim()} ({book.BookReleaseDate.Value.Year})");
                }
            }

            return builder.ToString();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categories = context.Categories.Select(c => new
            {
                c.Name,
                BooksProfit = c.CategoryBooks.Select(b => new
                {
                    b.Book.Copies,
                    b.Book.Price
                }).Select(pb => pb.Copies * pb.Price)
                .Sum()
            })
            .OrderByDescending(c => c.BooksProfit)
            .ThenBy(c => c.Name)
            .ToArray();

            var builder = new StringBuilder();

            foreach (var category in categories)
            {
                builder.AppendLine($"{category.Name} ${category.BooksProfit:F2}");
            }

            return builder.ToString();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authors = context.Authors
                .Select(a => new
                {
                    BooksCopies = a.Books.Sum(b => b.Copies),
                    a.FirstName,
                    a.LastName
                })
                .OrderByDescending(a => a.BooksCopies)
                .ToArray();

            var builder = new StringBuilder();

            foreach (var author in authors)
            {
                builder.AppendLine($"{author.FirstName} {author.LastName} - {author.BooksCopies}");
            }

            return builder.ToString();
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Where(b => b.Title.Length > lengthCheck)
                .Select(b => b.Title)
                .ToArray().Length;

            return booksCount;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    AuthorFirstName = b.Author.FirstName,
                    AuthorLastName = b.Author.LastName
                })
                .Where(b => b.AuthorLastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId);

            var builder = new StringBuilder();

            foreach (var book in books)
            {
                builder.AppendLine($"{book.Title} ({book.AuthorFirstName} {book.AuthorLastName})");
            }

            return builder.ToString();
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            var builder = new StringBuilder();

            foreach (var book in books)
            {
                builder.AppendLine(book);
            }

            return builder.ToString();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors.Select(a => new
            {
                a.FirstName,
                a.LastName
            })
            .Where(a => a.FirstName.EndsWith(input))
            .OrderBy(a => a.FirstName)
            .ThenBy(a => a.LastName);

            var builder = new StringBuilder();

            foreach (var author in authors)
            {
                builder.AppendLine($"{author.FirstName} {author.LastName}");
            }

            return builder.ToString();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var books = context.Books
                .Where(
                b => DateTime.Compare(b.ReleaseDate.Value, DateTime.ParseExact(
                    date, "dd-MM-yyyy", CultureInfo.InvariantCulture)) < 0)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .OrderByDescending(b => b.ReleaseDate)
                .ToArray();

            var builder = new StringBuilder();

            foreach (var book in books)
            {
                builder.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
            }

            return builder.ToString();
        }

        public static string GetBooksByCategory(BookShopContext context, string categoriesInput)
        {
            var categories = categoriesInput
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => i.ToLower());

            var books = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToArray();

            var builder = new StringBuilder();

            foreach (var book in books)
            {
                builder.AppendLine(book);
            }

            return builder.ToString();
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books.Select(b => new
            {
                b.BookId,
                b.Title,
                b.ReleaseDate
            })
            .Where(b => DateTime.Parse(b.ReleaseDate.ToString()).Year != year)
            .OrderBy(b => b.BookId)
            .ToArray();

            var builder = new StringBuilder();

            foreach (var book in books)
            {
                builder.AppendLine(book.Title);
            }

            return builder.ToString();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books.ToArray().Select(b => new
            {
                b.Title,
                b.Price
            })
            .Where(b => b.Price > 40)
            .OrderByDescending(b => b.Price);

            var builder = new StringBuilder();

            foreach (var book in books)
            {
                builder.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return builder.ToString().Trim();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books.ToArray().Select(b => new
            {
                b.BookId,
                b.Title,
                b.EditionType,
                b.Copies
            })
            .Where(b => b.EditionType.ToString().ToLower().Equals("gold") && b.Copies < 5000)
            .OrderBy(b => b.BookId);

            var booksList = books.Select(b => b.Title);

            return string.Join(Environment.NewLine, booksList);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var books = context.Books.ToArray().Select(b => new
            {
                b.Title,
                b.AgeRestriction
            })
            .Where(b => b.AgeRestriction.ToString().ToLower().Equals(command.ToLower()))
            .OrderBy(b => b.Title);

            var booksNames = books.Select(book => book.Title).ToList();

            return string.Join(Environment.NewLine, booksNames);
        }
    }
}
