namespace Core.Models.Entities.Tables
{
    public class TExPersonContact
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string HomeNumber { get; set; }
        public string PhoneNumber { get; set; }

        public TExPerson Person { get; set; }
        public IEnumerable<TExPersonIdentification> PersonIds { get; set; }
    }
}
