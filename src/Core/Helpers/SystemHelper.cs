using System.Runtime.CompilerServices;

namespace Core.Helpers
{
    public static class SystemHelper
    {
        public static string GetActualAsyncMethodName([CallerMemberName] string name = null) => name;
    }
}
