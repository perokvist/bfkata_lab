using BF.Api;
using Grpc.Core;
using Protos;

namespace BF.Api.Services
{
    public class BFService : InventoryService.InventoryServiceBase
    {
        private readonly ILogger<BFService> _logger;
        public BFService(ILogger<BFService> logger) => _logger = logger;

        public override Task<AddLocationsResp> AddLocations(AddLocationsReq request, ServerCallContext context)
        {
            var r = new AddLocationsResp();
            r.Locs.AddRange(request.Locs.Select(x => Map(x)));
            return Task.FromResult(r);
        }
        public override Task<AddProductsResp> AddProducts(AddProductsReq request, ServerCallContext context)
        {
            return base.AddProducts(request, context);
        }

        public override Task<FulfillResp> Fulfill(FulfillReq request, ServerCallContext context)
        {
            return base.Fulfill(request, context);
        }

        public override Task<GetLocInventoryResp> GetLocInventory(GetLocInventoryReq request, ServerCallContext context)
        {
            return base.GetLocInventory(request, context);
        }

        public override Task<MoveLocationResp> MoveLocation(MoveLocationReq request, ServerCallContext context)
        {
            return base.MoveLocation(request, context);
        }
        public override Task<ListLocationsResp> ListLocations(ListLocationsReq request, ServerCallContext context)
        {
            return base.ListLocations(request, context);
        }

        public override Task<ReserveResp> Reserve(ReserveReq request, ServerCallContext context)
        {
            return base.Reserve(request, context);
        }

        public override Task<UpdateInventoryResp> UpdateInventory(UpdateInventoryReq request, ServerCallContext context)
        {
            return base.UpdateInventory(request, context);
        }

        public static AddLocationsResp.Types.Loc Map(AddLocationsReq.Types.Loc loc, string parent = "")
        {
            var x = loc
                .Locs
                .Select(x => Map(x, x.Name));

            var r = new AddLocationsResp.Types.Loc
            {
                Name = loc.Name,
                Uid = "id",
                Parent = parent ?? string.Empty
            };
            r.Locs.Add(x);
            return r;
        }
    }
}