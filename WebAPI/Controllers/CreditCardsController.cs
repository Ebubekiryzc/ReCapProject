using Business.Abstract;
using Core.Utilities.Results;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/creditcards")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _creditCardService.GetAll();
            return ReturnResult(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _creditCardService.GetById(id);
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CreditCardForUserOperationsDto creditCard)
        {
            var result = _creditCardService.Add(creditCard);
            return ReturnResult(result);
        }

        [HttpPost("update")]
        public IActionResult Update(CreditCardForUserOperationsDto creditCard)
        {
            var result = _creditCardService.Update(creditCard);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CreditCardForUserOperationsDto creditCard)
        {
            var result = _creditCardService.Delete(creditCard);
            return ReturnResult(result);
        }

        private IActionResult ReturnResult(IResult result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
