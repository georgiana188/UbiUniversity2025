namespace EntityFrameworkTutorial.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public ICollection<BorrowRegister> Borrows { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public ICollection<Student> Students { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int NumberOfVolumes { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
