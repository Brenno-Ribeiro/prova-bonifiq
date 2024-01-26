using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers
{
	
	/// <summary>
	/// Esse teste simula um pagamento de uma compra.
	/// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
	/// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
	/// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{

		private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public Parte3Controller(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpGet("orders")]
		public async Task<IActionResult> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			var costumer = await _customerService.GetById(customerId);

			if(costumer == null)
			{
				return NotFound(new { message = "O cliente informado não existe!" });
			}

			try
			{
                var result = await _orderService.PayOrder(paymentMethod, paymentValue, costumer);
                return Ok(result);
            }
			catch (Exception ex)
			{

                return BadRequest(ex.Message);
            }
			
		}
	}
}
