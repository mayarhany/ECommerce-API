using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymenyService _paymenyService;
        private readonly ILogger<PaymentController> _logger;
        const string endpointSecret = "whsec_d8bd11dbfcf23b5d8ae684e58eddd409fb303c3b4ccae73fbdc8c6364a7f36c2";

        public PaymentController(
            IPaymenyService paymenyService,
            ILogger<PaymentController> logger)
        {
            _paymenyService = paymenyService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreatOrUpdatePaymentIntent(CustomerBasketDto input)
            => Ok(await _paymenyService.CreatOrUpdatePaymentIntent(input));

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                PaymentIntent paymentIntent;

                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Failed : ", paymentIntent.Id);

                    var order = await _paymenyService.UpdateOrderPaymentFailed(paymentIntent.Id);

                    _logger.LogInformation("Order Updated To Payment Failed : ", order.Id);
                }
                else if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    _logger.LogInformation("Payment Succeeded : ", paymentIntent.Id);

                    var order = await _paymenyService.UpdateOrderPaymentSucceeded(paymentIntent.Id);

                    _logger.LogInformation("Order Updated To Payment Succeeded : ", order.Id);
                }
                else if (stripeEvent.Type == "payment_intent.created")
                {
                    _logger.LogInformation("Payment Created");
                }
                else if (stripeEvent.Type == "payment_intent.canceled")
                {

                }
                else
                {
                    Console.WriteLine("Unhandeled event type : {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
    }
}
