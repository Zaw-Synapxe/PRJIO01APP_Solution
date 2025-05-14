using CQRSMediatR.API.Models;
using MediatR;

namespace CQRSMediatR.API.Commands
{
    public record AddProductCommand(Product Product) : IRequest<Product>;
}
