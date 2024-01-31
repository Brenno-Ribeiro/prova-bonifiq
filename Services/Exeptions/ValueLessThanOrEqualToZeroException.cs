namespace ProvaPub.Services.Exeptions
{
    public class ValueLessThanOrEqualToZeroException : Exception
    {
        public ValueLessThanOrEqualToZeroException() { }
        public ValueLessThanOrEqualToZeroException(string mensagem) : base(mensagem) { }
    }
}
