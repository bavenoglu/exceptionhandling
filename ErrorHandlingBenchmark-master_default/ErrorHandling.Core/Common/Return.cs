namespace ErrorHandling.Core.Common
{
    public readonly struct Return<T>
    {
        public ErrorCode Code { get; }
        public T? Value { get; }

        public bool IsSuccess => Code == ErrorCode.None;

        private Return(ErrorCode code, T? value)
        {
            Code = code;
            Value = value;
        }
        
        public static Return<T> Success(T value) => new(ErrorCode.None, value);
        public static Return<T> Fail(ErrorCode code)  => new(code, default);
    }
}
