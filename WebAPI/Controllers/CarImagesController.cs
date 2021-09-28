using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace WebAPI.Controllers
{
    [Route("api/carimages")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private ICarImageService _carImageService;
        private IWebHostEnvironment _webHostEnvironment;

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            return ReturnResult(result);
        }

        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int id)
        {
            //TODO: Will be removed in production
            var result = _carImageService.GetByCarId(id);
            foreach (var carImage in result.Data)
            {
                carImage.ImagePath = $"{DefaultRoutes.DefaultImageFolder}{Path.GetFileName(carImage.ImagePath)}";
            }
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] CarImage carImage, IFormFile image)
        {
            carImage.ImagePath = $"{_webHostEnvironment.WebRootPath}/images";
            var result = _carImageService.Add(image, carImage);
            return ReturnResult(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] CarImage carImage, IFormFile image)
        {
            var result = _carImageService.Update(image, carImage);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] CarImage carImage)
        {
            carImage = _carImageService.GetById(carImage.Id).Data;
            var result = _carImageService.Delete(carImage);
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
