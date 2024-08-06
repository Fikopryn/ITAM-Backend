using Newtonsoft.Json;

namespace Core.Helpers
{
    public class LogData
    {
        public string Type { get; set; }
        public string UserId { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        public static LogData BuildInformationLog(string type, string userId, string method)
        {
            return new LogData
            {
                Type = type,
                UserId = userId,
                Method = method
            };
        }

        public static LogData BuildInformationLog(string type, string userId, string method, string message)
        {
            return new LogData
            {
                Type = type,
                UserId = userId,
                Method = method,
                Message = message
            };
        }
        public static LogData BuildInformationLog(string type, string userId, string method, string message, object data)
        {
            return new LogData
            {
                Type = type,
                UserId = userId,
                Method = method,
                Message = message,
                Data = JsonConvert.SerializeObject(data)
            };
        }

        public static LogData BuildErrorLog(string userId, string method, string exMessage)
        {
            return new LogData
            {
                Type = LogType.Error,
                UserId = userId,
                Method = method,
                Message = exMessage
            };
        }

        public static LogData BuildErrorLog(string userId, string method, string exMessage, string url)
        {
            var errData = new { url };

            return new LogData
            {
                Type = LogType.Error,
                UserId = userId,
                Method = method,
                Message = exMessage,
                Data = JsonConvert.SerializeObject(errData)
            };
        }

        public static LogData BuildErrorLog(string userId, string method, string exMessage, string url, object data)
        {
            var errData = new { url, data };

            return new LogData
            {
                Type = LogType.Error,
                UserId = userId,
                Method = method,
                Message = exMessage,
                Data = JsonConvert.SerializeObject(errData)
            };
        }
    }
}
