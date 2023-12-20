using FluentEmailImp.WebApp.Models;

namespace FluentEmailImp.WebApp.Service
{
    public interface IEmailService
    {
        Task Send(EmailMetadata emailMetadata);
        Task SendUsingTemplateFromFile(EmailMetadata emailMetadata, User model, string templateFile);

    }
}
