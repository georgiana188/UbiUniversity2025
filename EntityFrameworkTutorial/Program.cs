using EntityFrameworkTutorial.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections.Generic;

namespace EntityFrameworkTutorial
{
    class Program
    {
        static void Main()
        {
            //ContextExample();

            // StatesExample();

            // QueryExample();

            // EagerLoadingExample();


            // LazyLoadingExample();

            // AddData();

            // Ex1();

            // Ex2();

            // Ex3();

            // Ex4();

            // Ex5();

            // Ex6();

            // Ex7();

            // Ex8();

            // Ex9();

            // Ex10();
        }

        public static string GetTitle()
        {
            return "XYZ";
        }

        public static void ContextExample()
        {
            using (var context = new LibraryContext())
            {
                //creates db if not exists
                context.Database.EnsureCreated();

                //create entity objects
                var cat = new Category() { Name = "Fiction" };
                var author = new Author() { FirstName = "George", LastName = "Orwell" };
                var book1 = new Book() { Title = "XYZ", Author = author, Category = cat };
                var book2 = new Book() { Title = "Animal Farm", Author = author, Category = cat };


                //add entitiy to the context
                context.Books.Add(book1);
                context.Books.Add(book2);

                //save data to the database tables
                context.SaveChanges();

                //retrieve all the books from the database
                foreach (var b in context.Books)
                {
                    Console.WriteLine($"Title: {b.Title}");
                }
            }
        }

        static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State.ToString()}");
            }
        }

        public static void StatesExample()
        {
            using (var context = new LibraryContext())
            {
                // retrieve entity 
                var book = context.Books.FirstOrDefault();
                DisplayStates(context.ChangeTracker.Entries());
            }

            using (var context = new LibraryContext())
            {
                context.Books.Add(new Book() { Title = "Alice's Adventures in Wonderland" });

                DisplayStates(context.ChangeTracker.Entries());
            }
        }

        public static void QueryExample()
        {
            using (var context = new LibraryContext())
            {
                var book = context.Books
                    .Where(b => b.Title == GetTitle())
                    .FirstOrDefault();

                Console.WriteLine($"Id: {book.BookId}");

            }
        }

        static void EagerLoadingExample()
        {

            using (var context = new LibraryContext())
            {
                var book = context.Books
                    .Where(b => b.Title == GetTitle())
                    .Include(b => b.Category)
                    .FirstOrDefault();

                Console.WriteLine($"Title: {book.Title}, Category:{book.Category.Name}");
            }

            using (var context = new LibraryContext())
            {
                var category = context.Categories.Find(1);
                var location1 = new Location() { Name = "", Description = "", Category = category };
                category.Locations.Add(location1);
                context.SaveChanges();

                var book = context.Books
                    .Where(b => b.Title == GetTitle())
                    .Include(b => b.Category)
                    .ThenInclude(c => c.Locations)
                    .FirstOrDefault();

                Console.Write($"Title: {book.Title}, Locations:");

                foreach (var loc in book.Category.Locations)
                    Console.Write(loc.Name + " ");

            }
        }

        static void AddData()
        {
            using (var context = new LibraryContext())
            {
                // Create authors
                var author1 = new Author { FirstName = "John", LastName = "Doe" };
                var author2 = new Author { FirstName = "Jane", LastName = "Smith" };

                // Create categories
                var category1 = new Category { Name = "Science Fiction" };
                var category2 = new Category { Name = "Fantasy" };

                // Create locations
                var location1 = new Location { Name = "Shelf A", Description = "Top Shelf", Category = category1 };
                var location2 = new Location { Name = "Shelf B", Description = "Middle Shelf", Category = category2 };

                // Create books
                var book1 = new Book { Title = "Book One", Year = 2020, NumberOfVolumes = 1, Author = author1, Category = category1 };
                var book2 = new Book { Title = "Book Two", Year = 2021, NumberOfVolumes = 2, Author = author2, Category = category2 };
                var book3 = new Book { Title = "Book Three", Year = 2022, NumberOfVolumes = 1, Author = author1, Category = category1 };
                var book4 = new Book { Title = "Book Four", Year = 2023, NumberOfVolumes = 3, Author = author2, Category = category2 };

                // Create students
                var student1 = new Student { Name = "Alice", DateOfBirth = new DateTime(2000, 1, 1), StudentKey = "S001", Class = "10A", Address = new StudentAddress { City = "City1", Street = "Street1", Number = 1, PostalCode = 12345 } };
                var student2 = new Student { Name = "Bob", DateOfBirth = new DateTime(2001, 2, 2), StudentKey = "S002", Class = "10B", Address = new StudentAddress { City = "City2", Street = "Street2", Number = 2, PostalCode = 67890 } };
                var student3 = new Student { Name = "Charlie", DateOfBirth = new DateTime(2002, 3, 3), StudentKey = "S003", Class = "10C", Address = new StudentAddress { City = "City1", Street = "Street3", Number = 3, PostalCode = 11111 } };

                // Create borrow registers
                var borrow1 = new BorrowRegister { Student = student1, Book = book1, BorrowedDate = DateTime.Now.AddDays(-10), ReturnedDate = DateTime.Now.AddDays(-5) };
                var borrow2 = new BorrowRegister { Student = student2, Book = book2, BorrowedDate = DateTime.Now.AddDays(-15), ReturnedDate = DateTime.Now.AddDays(-10) };
                var borrow3 = new BorrowRegister { Student = student1, Book = book3, BorrowedDate = DateTime.Now.AddDays(-20), ReturnedDate = DateTime.Now.AddDays(-15) };
                var borrow4 = new BorrowRegister { Student = student3, Book = book4, BorrowedDate = DateTime.Now.AddDays(-25), ReturnedDate = DateTime.Now.AddDays(-20) };

                // Add entities to context
                context.Authors.AddRange(author1, author2);
                context.Categories.AddRange(category1, category2);
                context.Locations.AddRange(location1, location2);
                context.Books.AddRange(book1, book2, book3, book4);
                context.Students.AddRange(student1, student2, student3);
                context.BorrowRegister.AddRange(borrow1, borrow2, borrow3, borrow4);

                // Save changes to the database
                context.SaveChanges();
            }
        }

        static void LazyLoadingExample()
        {
            using (var context = new LibraryContext())
            {
                //Loading books only
                IList<Book> books = context.Books.ToList();

                Book b1 = books[0];

                //Loads author for particular book only (seperate SQL query)
                Author author = b1.Author;

                Console.Write(author.FirstName + " " + author.LastName);
            }
        }

        //Exercise 1: List all books with their authors
        static void Ex1()
        {
            using (var context = new LibraryContext())
            {
                var booksWithAuthors = context.Books
                  .Select(b => new
                  {
                      BookTitle = b.Title,
                      AuthorName = b.Author.FirstName + " " + b.Author.LastName
                  }).ToList();

                foreach (var item in booksWithAuthors)
                {
                    Console.WriteLine($"Book: {item.BookTitle}, Author: {item.AuthorName}");
                }
            }
        }

        //Exercise 2: Find books borrowed by a specific student
        static void Ex2()
        {
            using (var context = new LibraryContext())
            {
                int studentId = 1; // Example student ID
                var borrowedBooks = context.BorrowRegister
                    .Where(br => br.StudentId == studentId)
                    .Select(br => br.Book)
                    .ToList();

                foreach (var book in borrowedBooks)
                {
                    Console.WriteLine($"Book: {book.Title}");
                }
            }
        }

        //Exercise 3: List all students who have borrowed books
        static void Ex3()
        {
            using (var context = new LibraryContext())
            {
                var studentsWithBorrows = context.Students
                    .Where(s => s.Borrows.Any())
                        .Select(s => new
                        {
                            StudentName = s.Name,
                            BorrowCount = s.Borrows.Count
                        }).ToList();

                foreach (var student in studentsWithBorrows)
                {
                    Console.WriteLine($"Student: {student.StudentName}, Borrowed Books: {student.BorrowCount}");
                }
            }
        }

        //Exercise 4: Find the most borrowed book
        static void Ex4()
        {
            using (var context = new LibraryContext())
            {
                var mostBorrowedBook = context.BorrowRegister
                        .GroupBy(br => br.Book)
                        .OrderByDescending(g => g.Count())
                        .Select(g => new { BookTitle = g.Key.Title, BorrowCount = g.Count() }).FirstOrDefault();

                if (mostBorrowedBook != null)
                {
                    Console.WriteLine($"Most Borrowed Book: {mostBorrowedBook.BookTitle}, Times Borrowed: {mostBorrowedBook.BorrowCount}");
                }
            }
        }

        //Exercise 5: List all books in a specific category
        static void Ex5()
        {
            using (var context = new LibraryContext())
            {
                int categoryId = 1; // Example category ID
                var booksInCategory = context.Books
                    .Where(b => b.CategoryId == categoryId)
                    .ToList();

                foreach (var book in booksInCategory)
                {
                    Console.WriteLine($"Book: {book.Title}");
                }
            }
        }

        //Exercise 6: Find overdue books
        static void Ex6()
        {
            using (var context = new LibraryContext())
            {
                DateTime today = DateTime.Today;
                var overdueBooks = context.BorrowRegister
                    .Where(br => br.ReturnedDate > today)
                    .Select(br => br.Book)
                    .ToList();

                foreach (var book in overdueBooks)
                {
                    Console.WriteLine($"Overdue Book: {book.Title}");
                }
            }
        }

        //Exercise 7: List all authors with their books
        static void Ex7()
        {
            using (var context = new LibraryContext())
            {
                var authorsWithBooks = context.Authors
                    .Select(a => new
                    {
                        AuthorName = a.FirstName + " " + a.LastName,
                        Books = a.Books.Select(b => b.Title).ToList()
                    }).ToList();

                foreach (var author in authorsWithBooks)
                {
                    Console.WriteLine($"Author: {author.AuthorName}");
                    foreach (var book in author.Books)
                    {
                        Console.WriteLine($"  Book: {book}");
                    }
                }
            }
        }

        //Exercise 8: Find students living in a specific city
        static void Ex8()
        {
            using (var context = new LibraryContext())
            {
                string city = "ExampleCity"; // Example city name
                var studentsInCity = context.Students
                    .Where(s => s.Address.City == city)
                    .ToList();

                foreach (var student in studentsInCity)
                {
                    Console.WriteLine($"Student: {student.Name}, City: {student.Address.City}");
                }
            }
        }

        //Exercise 9: List all categories with their books
        static void Ex9()
        {
            using (var context = new LibraryContext())
            {
                var categoriesWithBooks = context.Categories
                    .Select(c => new
                    {
                        CategoryName = c.Name,
                        Books = c.Books.Select(b => b.Title).ToList()
                    }).ToList();

                foreach (var category in categoriesWithBooks)
                {
                    Console.WriteLine($"Category: {category.CategoryName}");
                    foreach (var book in category.Books)
                    {
                        Console.WriteLine($"  Book: {book}");
                    }
                }
            }
        }

        //Exercise 10: Find the total number of books borrowed by each student
        static void Ex10()
        {
            using (var context = new LibraryContext())
            {
                var booksBorrowedByStudents = context.BorrowRegister
                    .GroupBy(br => br.Student)
                    .Select(g => new
                    {
                        StudentName = g.Key.Name,
                        BorrowCount = g.Count()
                    }).ToList();

                foreach (var student in booksBorrowedByStudents)
                {
                    Console.WriteLine($"Student: {student.StudentName}, Total Books Borrowed: {student.BorrowCount}");
                }
            }
        }

    }
}
