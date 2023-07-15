using CQRSMediatR.API.DataStore;
using CQRSMediatR.API.Models;
using CQRSMediatR.API.Queries;
using MediatR;

namespace CQRSMediatR.API.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetProductsHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request,
            CancellationToken cancellationToken) => await _fakeDataStore.GetAllProducts();
    }
}
