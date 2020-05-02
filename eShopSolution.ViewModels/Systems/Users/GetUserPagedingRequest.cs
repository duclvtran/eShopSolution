using eShopSolution.ViewModels.Common;

namespace eShopSolution.ViewModels.Systems.Users
{
    public class GetUserPagedingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}