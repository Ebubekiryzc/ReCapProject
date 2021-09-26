using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/usercreditcards")]
    [ApiController]
    public class UserCreditCardsController : ControllerBase
    {
        private IUserCreditCardService _userCreditCardService;

        public UserCreditCardsController(IUserCreditCardService userCreditCardService)
        {
            _userCreditCardService = userCreditCardService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userCreditCardService.GetAll();
            return ReturnResult(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userCreditCardService.GetById(id);
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add(UserCreditCard creditCard)
        {
            var result = _userCreditCardService.Add(creditCard);
            return ReturnResult(result);
        }

        [HttpPost("update")]
        public IActionResult Update(UserCreditCard creditCard)
        {
            var result = _userCreditCardService.Update(creditCard);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(UserCreditCard creditCard)
        {
            var result = _userCreditCardService.Delete(creditCard);
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
