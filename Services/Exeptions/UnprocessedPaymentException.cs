namespace ProvaPub.Services.Exeptions
{
    public class UnprocessedPaymentException : Exception
    {
        public UnprocessedPaymentException() { }
        public UnprocessedPaymentException(string mensagem) : base(mensagem) { }
    }
}
