namespace Core
{
    public static class ConsSecurity
    {
        public static string AesKey => "$b5YdJYsC#3Vnw4V!g#F";
        public static string AesVector => "1NCF53K5qgYzis/GiJTOeg==";
    }

    public static class LogType
    {
        public static string Authorization => "AUTHORIZATION";
        public static string Error => "ERROR";
    }

    public static class HttpClientName
    {
        public static string R3Client => "R3Client";
    }

    public static class R3WorkflowType
    {
        public static string ACTION => "action";
        public static string INPUT => "input";
        public static string ASSIGN => "assign";
        public static string END => "end";
    }

    public static class R3WorkflowAction
    {
        public static string APPROVE => "approve";
        public static string REVISE => "revise";
    }

}
