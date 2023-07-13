
namespace SimtekMaui.Models
{
    public class Site(string name, string address, Customer customer):IRecordClass
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Address { get; set; } = address;
        public Customer Customer { get; set; } = customer;

        public string Identity => Id.ToString();
    }
}
