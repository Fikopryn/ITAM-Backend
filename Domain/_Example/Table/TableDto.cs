namespace Domain.Example.Table
{
    public class ExPersonReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public ExPersonContactReadDto PersonContact { get; set; }
    }

    public class ExPersonContactReadDto
    {
        public int Id { get; set; }
        public string HomeNumber { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<ExPersonIdReadDto> PersonIds { get; set; }
    }

    public class ExPersonIdReadDto
    {
        public int Id { get; set; }
        public string IDType { get; set; }
        public string IDValue { get; set; }
    }

    public class ExPersonInsertDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public ExPersonContactInsertDto PersonContact { get; set; }
    }

    public class ExPersonContactInsertDto
    {
        public string HomeNumber { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<ExPersonIdInsertDto> PersonIds { get; set; }
    }

    public class ExPersonIdInsertDto
    {
        public string IDType { get; set; }
        public string IDValue { get; set; }
    }
}
