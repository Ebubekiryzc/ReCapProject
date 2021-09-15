using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _rentalService.GetAll();
            return ReturnResult(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _rentalService.GetById(id);
            return ReturnResult(result);
        }

        [HttpGet("getallwithindividualcustomerdetails")]
        public IActionResult GetAllWithIndividualCustomerDetails()
        {
            var result = _rentalService.GetAllRentalsWithIndividualCustomerDetails();
            return ReturnResult(result);
        }

        [HttpGet("getwithindividualcustomerdetailsbyrentalid")]
        public IActionResult GetWithIndividualCustomerDetailsByRentalId(int id)
        {
            var result = _rentalService.GetRentalDetailsForIndividualCustomersByRentalId(id);
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);
            return ReturnResult(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Rental rental)
        {
            var result = _rentalService.Update(rental);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Rental rental)
        {
            var result = _rentalService.Delete(rental);
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
