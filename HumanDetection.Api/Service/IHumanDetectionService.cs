using HumanDetection.Api.Model;

namespace HumanDetection.Api.Service
{
    public interface IHumanDetectionService
    {
        ResponseModel VerifyHuman(IFormFile imageFile);
    }
}