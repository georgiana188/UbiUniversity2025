using EntityFrameworkTutorial.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;

namespace EntityFrameworkTutorial
{
    public class LibraryContext: DbContext
    {
        public LibraryContext():base()
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<BorrowRegister> BorrowRegister { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Library;Trusted_Connection=True;");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            // Get the Section to Read from the Configuration File
            var configSection = configBuilder.GetSection("ConnectionStrings");
            // Get the Configuration Values based on the Config key.
            var connectionString = configSection["DBLocalConnection"] ?? null;

            //Configuring the Connection String
            optionsBuilder.UseSqlServer(connectionString);


            //to see the query behind the linq in the console

            //optionsBuilder.LogTo(Console.WriteLine);


            //Enable lazy loading
            //optionsBuilder.UseLazyLoadingProxies();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Entity configs
            ////Map entity to table
            ////override the convention and give a different table name than the DbSet properties
            //modelBuilder.Entity<Student>().ToTable("StudentInfo");

            ////Configure primary key
            //modelBuilder.Entity<Student>().HasKey(s => s.StudentKey);

            ////Configure composite primary key
            //modelBuilder.Entity<Student>().HasKey(s => new { s.StudentKey, s.Name });

            ////Property mapping
            ////Configure Column
            //modelBuilder.Entity<Student>()
            //            .Property(p => p.DateOfBirth)
            //            .HasColumnName("DoB")
            //            .HasColumnOrder(3)
            //            .HasColumnType("datetime2")
            //            .HasDefaultValue(DateTime.Now)
            //            .IsRequired();

            //modelBuilder.Entity<Book>()
            //    .HasOne(b => b.Author)
            //    .WithMany(a => a.Books)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Book>()
            //   .HasOne(b => b.Category)
            //   .WithMany(a => a.Books)
            //   .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Student>()
            //    .HasOne(s => s.Address)
            //    .WithOne(ad => ad.Student)
            //    .HasForeignKey<StudentAddress>(ad => ad.StudentId);


            //// configures one-to-many relationship
            //modelBuilder.Entity<Book>()
            //    .HasOne<Author>(b => b.Author)
            //    .WithMany(g => g.Books)
            //    .IsRequired()
            //    .HasForeignKey(s => s.AuthorId);

            modelBuilder.Entity<BorrowRegister>()
                .HasKey(sc => new { sc.StudentId, sc.BookId });

            modelBuilder.Entity<BorrowRegister>()
                .HasOne<Student>(b => b.Student)
                .WithMany(s => s.Borrows)
                .HasForeignKey(b => b.StudentId);


            modelBuilder.Entity<BorrowRegister>()
                .HasOne<Book>(b => b.Book)
                .WithMany(bk => bk.Borrows)
                .HasForeignKey(b => b.BookId);


        }

    }
}
