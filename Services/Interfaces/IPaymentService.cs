using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IPaymentService
    {
        Dictionary<string, IPaymentMethod> GeneratePaymentMethods();
    }

}
