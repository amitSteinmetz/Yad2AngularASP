using backend.Models.AssetModels;

namespace backend.IRepositories
{
    public interface IAssetsRepository
    {
        Task<Asset> CreateAssetAsync(Asset asset);
        Task<(List<Asset> Items, int AssetsTotalCount)> GetAssetsByPageAsync(AssetFilters filters);
    }
}
