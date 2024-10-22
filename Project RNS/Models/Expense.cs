namespace Project_RNS.Models
{
    public class Expense
    {

        public int Id { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }
        public byte[]? BillDoc { get; set; }

    }

    public class ExpenseDto
    {

        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Base64data { get; set; }

    }

    public class ImageUploadDto
    {
        public int Id { get; set; }
        public string Base64Form { get; set; }
    }

    public class SignUpDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        //public int?  Status {  get; set; }


    }
}
