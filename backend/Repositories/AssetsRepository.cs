using backend.DB;
using backend.IRepositories;
using backend.Models.AssetModels;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class AssetsRepository : IAssetsRepository
    {
        private readonly AppDbContext _context;

        public AssetsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task<(List<Asset> Items, int AssetsTotalCount)> GetAssetsAsync(AssetFilters filters)
        {
            // 1. הגדרת הבסיס עם AsNoTracking
            var query = _context.Assets
                .Include(a => a.Address)
                .Include(a => a.ContactDetails)
                .AsNoTracking()
                .AsQueryable(); // what exactly this does?

            // 2. שרשור פילטרים דינמי 
            query = ApplyFilters(query, filters);

            // 3. ספירת סך הכל (חשוב בשביל ה-UI באנגולר כדי לדעת כמה דפים יש)
            int assetsTotalCount = await query.CountAsync();

            // 4. יישום Pagination (חלון התוצאות)
            // גודל דף קבוע בשרת כפי שביקשת
            const int pageSize = 10;

            var items = await query
                .OrderByDescending(a => a.PublishDate) // תמיד הכי חדש למעלה
                .Skip((filters.PageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, assetsTotalCount);
        }

        private IQueryable<Asset> ApplyFilters(IQueryable<Asset> query, AssetFilters filters)
        {
            // Need to complete all the filters from assetFilters model
            if (filters.Type.HasValue) query = query.Where(a => a.Type == filters.Type);
            if (filters.Condition.HasValue) query = query.Where(a => a.Condition == filters.Condition);
            if (!string.IsNullOrEmpty(filters.City)) query = query.Where(a => a.Address.City == filters.City);
            if (!string.IsNullOrEmpty(filters.Street)) query = query.Where(a => a.Address.Street.Contains(filters.Street));

            if (filters.MinPrice.HasValue) query = query.Where(a => a.Price >= filters.MinPrice);
            if (filters.MaxPrice.HasValue) query = query.Where(a => a.Price <= filters.MaxPrice);
            if (filters.MinRooms.HasValue) query = query.Where(a => a.NumberOfRooms >= filters.MinRooms);
            if (filters.MaxRooms.HasValue) query = query.Where(a => a.NumberOfRooms <= filters.MaxRooms);

            if (filters.HasParking) query = query.Where(a => a.HasParking);
            if (filters.HasElevator) query = query.Where(a => a.HasElevator);
            if (filters.HasSafeRoom) query = query.Where(a => a.HasSafeRoom);
            if (filters.IsRenovated) query = query.Where(a => a.IsRenovated);
            if (filters.WithImage) query = query.Where(a => a.GalleryImageUrls.Any() || a.MainImageUrl != null); // דוגמה לבדיקת תמונות

            return query;
        }
    }
}
