using HumanDetection.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace HumanDetection.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HumanDetectionController : ControllerBase
    {
        private readonly IHumanDetectionService _humanDetection;

        public HumanDetectionController(IHumanDetectionService humanDetection)
        {
            _humanDetection = humanDetection;
        }

        [HttpPost(Name = "DetectHuman")]
        public IActionResult Predict(IFormFile imageFile)
        {
            return Ok(_humanDetection.VerifyHuman(imageFile));
        }
    }
}