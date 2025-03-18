namespace EntityFrameworkTutorial.Entities
{
    public class BorrowRegister
    {
        public int BorrowId { get; set; }
        public Student Student { get; set; }
        public Book Book { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnedDate { get; set; }
    }
}
