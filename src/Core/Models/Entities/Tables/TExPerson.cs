namespace Core.Models.Entities.Tables
{
    public class TExPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public TExPersonContact PersonContact { get; set; }
    }
}
