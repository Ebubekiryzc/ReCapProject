using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            return ReturnResult(result);
        }

        [HttpGet("getcarsbybrandid")]
        public IActionResult GetCarsByBrandId(int brandId)
        {
            var result = _carService.GetCarsByBrandId(brandId);
            return ReturnResult(result);
        }

        [HttpGet("getcarsbycolorid")]
        public IActionResult GetCarsByColorId(int colorId)
        {
            var result = _carService.GetCarsByColorId(colorId);
            return ReturnResult(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);
            return ReturnResult(result);
        }

        [HttpGet("getcarswithdetail")]
        public IActionResult GetCarsWithDetail()
        {
            var result = _carService.GetCarsWithDetails();
            return ReturnResult(result);
        }

        [HttpGet("getcarwithdetailbyid")]
        public IActionResult GetCarWithDetailsById(int id)
        {
            var result = _carService.GetCarWithDetailsById(id);
            return ReturnResult(result);
        }

        [HttpGet("getcarswithdetailbybrandid")]
        public IActionResult GetCarsWithDetailByBrandId(int brandId)
        {
            var result = _carService.GetCarsWithDetailsByBrandId(brandId);
            return ReturnResult(result);
        }

        [HttpGet("getcarswithdetailbycolorid")]
        public IActionResult GetCarsWithDetailColorId(int colorId)
        {
            var result = _carService.GetCarsWithDetailsByColorId(colorId);
            return ReturnResult(result);
        }

        [HttpGet("getcarswithdetailbybrandidandcolorid")]
        public IActionResult GetCarsWithDetailsByBrandIdAndColorId(int brandId, int colorId)
        {
            var result = _carService.GetCarsWithDetailsByBrandIdAndColorId(brandId, colorId);
            return ReturnResult(result);
        }

        [HttpGet("gettopsixdeals")]
        public IActionResult GetTopSixDeals()
        {
            var result = _carService.GetTopSixDealsWithDetails();
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            return ReturnResult(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);
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
