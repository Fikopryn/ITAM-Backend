using Core.Models.Entities.Tables;

namespace Domain.Example.FileProcessing
{
    public class ExampleExcelDto
    {
        public string POSID { get; set; }
        public string POSNAME { get; set; }
        public string USERID { get; set; }
        public string USERNAME { get; set; }
        public string PARENTPOSID { get; set; }
        public string PARENTPOSNAME { get; set; }
        public string AREA { get; set; }
        public string SUBAREA { get; set; }
        public string EMPCAT { get; set; }
        public string LEVELS { get; set; }
        public string POSCAT { get; set; }
        public string L1NAME { get; set; }
        public string L2NAME { get; set; }
        public string L3NAME { get; set; }
        public string L4NAME { get; set; }
        public string L5NAME { get; set; }
        public string L6NAME { get; set; }
        public string POSABBR { get; set; }
    }

    public class ExFileDetailsReadDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
    }

    public class ExFileReferenceDto
    {
        public Guid FileNumber { get; set; }
        public Guid? ModulId { get; set; }
        public string? ModulName { get; set; }
        public string? FileCategory { get; set; }
        public string? FileNameExtention { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    public class InterfaceToDbDto
    {
        public string ModuleUid { get; set; }
        public string ModuleCode { get; set; }
    }

    public class UploadPayload
    {
        public string AppCodeName { get; set; }
        public string TableName { get; set; }
        public string UploadBy { get; set; }
        public string UploadDateTime { get; set; }
        //public byte[] flDoc { get; set; }
    }

    public class UploadResult
    {
        public string status { get; set; }
        public List<ResultMessage> data { get; set; }
    }

    public class ExcelProcessingResult
    {
        public string status { get; set; }
        public List<ExampleExcelDto> data { get; set; }
    }

    public class ResultMessage
    {
        public int line { get; set; }
        public string message { get; set; }
    }
}
