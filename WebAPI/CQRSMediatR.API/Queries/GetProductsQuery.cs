using CQRSMediatR.API.Models;
using MediatR;

namespace CQRSMediatR.API.Queries
{
    public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
}
