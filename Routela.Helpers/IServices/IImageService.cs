using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;


namespace Routela.Services.IServices
{
    public interface IImageService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<VideoUploadResult> AddVideoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}