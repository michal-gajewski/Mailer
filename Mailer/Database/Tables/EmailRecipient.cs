namespace Database.Tables
{
    public class EmailRecipient
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Email Email { get; set; }
    }
}