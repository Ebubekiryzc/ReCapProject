using Business.Abstract;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("getpayment")]
        public IActionResult GetPayment(PaymentQueryDto paymentQueryDto)
        {
            var result = _paymentService.GetPayment(paymentQueryDto.CreditCardForUserOperationsDto, paymentQueryDto.Rental);
            return ReturnResult(result);
        }

        private ActionResult ReturnResult(IResult result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
