namespace ProvaPub.Services.Exeptions
{
    public class ItemDoesNotExistException : Exception
    {
        public ItemDoesNotExistException() { }
        public ItemDoesNotExistException(string mensagem) : base(mensagem) { }
    }
}
