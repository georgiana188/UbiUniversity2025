namespace EntityFrameworkTutorial.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public ICollection<BorrowRegister> Borrows { get; set; }
        public Student()
        {
            this.Books = new List<Book>();
        }

        public DateTime DateOfBirth { get; set; }
        public string StudentKey { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public ICollection<Book> Books { get; set; }
        public StudentAddress Address { get; set; }

    }
}
