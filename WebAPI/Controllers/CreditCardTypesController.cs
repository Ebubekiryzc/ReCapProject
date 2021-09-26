using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/creditcardtypes")]
    [ApiController]
    public class CreditCardTypesController : ControllerBase
    {
        private ICreditCardTypeService _creditCardTypeService;

        public CreditCardTypesController(ICreditCardTypeService creditCardTypeService)
        {
            _creditCardTypeService = creditCardTypeService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _creditCardTypeService.GetAll();
            return ReturnResult(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _creditCardTypeService.GetById(id);
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CreditCardType creditCardType)
        {
            var result = _creditCardTypeService.Add(creditCardType);
            return ReturnResult(result);
        }

        [HttpPost("update")]
        public IActionResult Update(CreditCardType creditCardType)
        {
            var result = _creditCardTypeService.Update(creditCardType);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CreditCardType creditCardType)
        {
            var result = _creditCardTypeService.Delete(creditCardType);
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
