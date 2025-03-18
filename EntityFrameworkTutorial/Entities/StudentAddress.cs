namespace EntityFrameworkTutorial.Entities
{
    public class StudentAddress
    {
        public int StudentAddressId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }
     
    }
}
