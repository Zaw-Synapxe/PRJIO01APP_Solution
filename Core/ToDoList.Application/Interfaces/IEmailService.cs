using ToDoList.Application.DTOs.Email;

namespace ToDoList.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);
    }
}
