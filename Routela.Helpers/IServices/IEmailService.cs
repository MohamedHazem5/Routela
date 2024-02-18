using Routela.Models;


namespace Routela.Services.IServices
{
    public interface IEmailService
    {
        public Task EmailSender(string email, string subject, string htmlMessage);
    }
}