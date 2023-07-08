namespace PocketIS.Application.Common.Types
{
    public class PocketISException : Exception
    {
        public string Code { get; }

        public PocketISException()
        {
        }

        public PocketISException(string code)
        {
            Code = code;
        }

        public PocketISException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public PocketISException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public PocketISException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public PocketISException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
