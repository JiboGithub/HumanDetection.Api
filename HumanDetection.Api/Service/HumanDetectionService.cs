using HumanDetection.Api.Model;
using OpenCvSharp;

namespace HumanDetection.Api.Service;

public class HumanDetectionService : IHumanDetectionService
{
    private readonly CascadeClassifier _model;

    static readonly string modelPath = string.Concat(Directory.GetCurrentDirectory(), "/haarcascade_fullbody.xml");

    public HumanDetectionService()
    {
        // Load the CascadeClassifier model
        _model = new CascadeClassifier(modelPath);
    }

    public ResponseModel VerifyHuman(IFormFile imageFile)
    {
        try
        {
            // Convert the IFormFile image to a byte array
            using var imageStream = imageFile.OpenReadStream();
            using var memoryStream = new MemoryStream();
            imageStream.CopyTo(memoryStream);
            byte[] imageData = memoryStream.ToArray();

            Mat matImage = Cv2.ImDecode(imageData, ImreadModes.Color);

            // Perform human detection using CascadeClassifier
            var detectedHumans = _model.DetectMultiScale(matImage);
            // If any human is detected, consider it verified
            return new ResponseModel { IsHuman = detectedHumans.Length > 0 };
        }
        catch (Exception ex)
        {
            // Handle the exception appropriately (log, return false, etc.)
            Console.WriteLine("Error verifying human: " + ex.Message);
            return new ResponseModel { IsHuman = false};
        }
    }
}
