namespace Core.Models.Entities.Tables
{
    public class TExPersonIdentification
    {
        public int Id { get; set; }
        public int PersonContactId { get; set; }
        public string IDType { get; set; }
        public string IDValue { get; set; }

        public TExPersonContact PersonContact { get; set; }
    }
}
