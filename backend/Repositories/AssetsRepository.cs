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

        public async Task<(List<Asset> Items, int AssetsTotalCount)> GetAssetsByPageAsync(AssetFilters filters)
        {
            // 1. הגדרת הבסיס עם AsNoTracking
            var query = _context.Assets
                .Include(a => a.Address)
                .Include(a => a.ContactDetails)
                .AsNoTracking()
                .AsQueryable(); // only for readability - declare that filters will be applied

            // 2. שרשור פילטרים דינמי 
            query = ApplyFilters(query, filters);

            // 3. ספירת סך הכל (חשוב בשביל ה-UI באנגולר כדי לדעת כמה דפים יש)
            int assetsTotalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(a => a.PublishDate) // תמיד הכי חדש למעלה
                .Skip((filters.PageNumber - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync(); 

            return (items, assetsTotalCount);
        }

        private static IQueryable<Asset> ApplyFilters(IQueryable<Asset> query, AssetFilters filters)
        {
            // --- פילטרים בסיסיים ו-Enums ---
            if (filters.Type.HasValue) query = query.Where(a => a.Type == filters.Type);
            if (filters.Condition.HasValue) query = query.Where(a => a.Condition == filters.Condition);

            // --- כתובת (שימוש ב-Address המקונן) ---
            if (!string.IsNullOrEmpty(filters.City)) query = query.Where(a => a.Address.City == filters.City);
            if (!string.IsNullOrEmpty(filters.Neighborhood)) query = query.Where(a => a.Address.Neighborhood == filters.Neighborhood);
            if (!string.IsNullOrEmpty(filters.Street)) query = query.Where(a => a.Address.Street.Contains(filters.Street));

            // --- קומה ---
            if (filters.Floor.HasValue) query = query.Where(a => a.Floor == filters.Floor);

            // --- טווחי מחירים וחדרים ---
            if (filters.MinPrice.HasValue) query = query.Where(a => a.Price >= filters.MinPrice);
            if (filters.MaxPrice.HasValue) query = query.Where(a => a.Price <= filters.MaxPrice);
            if (filters.MinRooms.HasValue) query = query.Where(a => a.NumberOfRooms >= filters.MinRooms);
            if (filters.MaxRooms.HasValue) query = query.Where(a => a.NumberOfRooms <= filters.MaxRooms);

            // --- טווחי שטח (כללי ובנוי) ---
            if (filters.MinAreaInSquareMeters.HasValue) query = query.Where(a => a.AreaInSquareMeters >= filters.MinAreaInSquareMeters);
            if (filters.MaxAreaInSquareMeters.HasValue) query = query.Where(a => a.AreaInSquareMeters <= filters.MaxAreaInSquareMeters);
            if (filters.MinBuiltAreaInSquareMeters.HasValue) query = query.Where(a => a.BuiltAreaInSquareMeters >= filters.MinBuiltAreaInSquareMeters);
            if (filters.MaxBuiltAreaInSquareMeters.HasValue) query = query.Where(a => a.BuiltAreaInSquareMeters <= filters.MaxBuiltAreaInSquareMeters);

            // --- תאריך כניסה ---
            // מחזיר נכסים שתאריך הכניסה שלהם הוא עד התאריך המבוקש (או פנוי מיידית)
            if (filters.EntryDate.HasValue) query = query.Where(a => a.EntryDate <= filters.EntryDate);

            // --- פילטרים בוליאניים (מאפייני נכס) ---
            if (filters.WithImage) query = query.Where(a => a.GalleryImageUrls.Any() || a.MainImageUrl != null);
            if (filters.HasParking) query = query.Where(a => a.HasParking);
            if (filters.HasElevator) query = query.Where(a => a.HasElevator);
            if (filters.HasSafeRoom) query = query.Where(a => a.HasSafeRoom);
            if (filters.HasBalcony) query = query.Where(a => a.HasBalcony);
            if (filters.HasAirConditioning) query = query.Where(a => a.HasAirConditioning);
            if (filters.HasStorage) query = query.Where(a => a.HasStorage);
            if (filters.IsRenovated) query = query.Where(a => a.IsRenovated);
            if (filters.IsAccessible) query = query.Where(a => a.IsAccessible);
            if (filters.HasBars) query = query.Where(a => a.HasBars);
            if (filters.IsFurnished) query = query.Where(a => a.IsFurnished);
            if (filters.IsExclusive) query = query.Where(a => a.IsExclusive);

            // --- תאריך כניסה (תיקון שקט) ---
            if (filters.EntryDate.HasValue)
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                var effectiveEntryDate = filters.EntryDate < today ? today : filters.EntryDate.Value;
                query = query.Where(a => a.EntryDate >= effectiveEntryDate);
            }

            return query;
        }
    }
}
