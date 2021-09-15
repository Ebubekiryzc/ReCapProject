using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/individual-customers")]
    [ApiController]
    public class IndividualCustomersController : ControllerBase
    {
        private IIndividualCustomerService _individualCustomerService;

        public IndividualCustomersController(IIndividualCustomerService individualCustomerService)
        {
            _individualCustomerService = individualCustomerService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _individualCustomerService.GetAll();
            return ReturnResult(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _individualCustomerService.GetById(id);
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add(IndividualCustomer individualCustomer)
        {
            var result = _individualCustomerService.Add(individualCustomer);
            return ReturnResult(result);
        }

        [HttpPost("update")]
        public IActionResult Update(IndividualCustomer individualCustomer)
        {
            var result = _individualCustomerService.Update(individualCustomer);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(IndividualCustomer individualCustomer)
        {
            var result = _individualCustomerService.Delete(individualCustomer);
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
