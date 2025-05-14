using CQRSMediatR.API.Models;
using MediatR;

namespace CQRSMediatR.API.Notifications
{
    public record ProductAddedNotification(Product Product) : INotification;
}
