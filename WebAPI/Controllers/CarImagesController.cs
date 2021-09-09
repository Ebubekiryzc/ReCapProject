using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetById(int id)
        {
            var result = _carImageService.GetByCarId(id);
            return ReturnResult(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] CarImage carImage, IFormFile image)
        {
            var operationResult = FileHelper.AddAsync(image, ImageInfo.DefaultImageFolder);

            if (operationResult is ErrorResult)
            {
                return ReturnResult(operationResult);
            }

            carImage.ImagePath = operationResult.Message;

            var result = _carImageService.Add(carImage);
            if (!result.Success)
            {
                Delete(carImage);
            };
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] CarImage carImage, IFormFile image)
        {
            var operationResult = FileHelper.DeleteAsync(carImage.ImagePath);

            if (operationResult is ErrorResult)
            {
                ReturnResult(operationResult);
            }

            operationResult = FileHelper.AddAsync(image, ImageInfo.DefaultImageFolder);

            if (operationResult is ErrorResult)
            {
                return ReturnResult(operationResult);
            }

            carImage.ImagePath = operationResult.Message;

            var result = _carImageService.Update(carImage);
            return ReturnResult(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] CarImage carImage)
        {
            var operationResult = BusinessRules.Check(FileHelper.DeleteAsync(carImage.ImagePath));

            if (operationResult is ErrorResult)
            {
                return ReturnResult(operationResult);
            }

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
