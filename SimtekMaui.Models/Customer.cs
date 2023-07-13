namespace SimtekMaui.Models
{
    public class Customer(string name, string surname, string address) : IRecordClass
    {
        public Customer(string name, string surname, string address, Guid id, string vat, string email, string phoneNumber):this(name,surname,address)
        {
            Email = email;
            Id = id;
            Vat = vat;
            Email = email;
            Phone = phoneNumber;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public string Address { get; set; } = address;

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CAP { get; set; }

        public string? Vat { get; set; }

        public string FullName => name + " " + surname;

        public string Identity => Id.ToString();

        Site[] Sites { get; set; } = Array.Empty<Site>();
    }
}
