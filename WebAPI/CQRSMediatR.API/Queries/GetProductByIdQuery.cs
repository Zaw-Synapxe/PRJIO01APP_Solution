using CQRSMediatR.API.Models;
using MediatR;

namespace CQRSMediatR.API.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product>;
}
