namespace ProvaPub.Services.Exeptions
{
    public class UnprocessedPayment : Exception
    {
        public UnprocessedPayment() { }
        public UnprocessedPayment(string mensagem) : base(mensagem) { }
    }
}
