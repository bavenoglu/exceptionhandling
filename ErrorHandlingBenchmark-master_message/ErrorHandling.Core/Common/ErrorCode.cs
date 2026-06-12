namespace ErrorHandling.Core.Common
{
    public enum ErrorCode
    {
        None = 0,
        InvalidInput = 1,
        NotFound = 2,
        Error = 3
    }
    public static class ErrorCodeExtensions
    {
        public static string ToMessage(this ErrorCode code) => code switch
        {
            ErrorCode.None => "",
            ErrorCode.InvalidInput => "Invalid ID",
            ErrorCode.NotFound => "User id not found",
            ErrorCode.Error => "Cannot be divided by zero",
            _ => "An unknown error occurred." 
        };
    }
}
