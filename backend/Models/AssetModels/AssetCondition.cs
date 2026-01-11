namespace backend.Models.AssetModels
{
    public enum AssetCondition
    {
        NewFromDeveloper = 1, // חדש מקבלן
        New,              // נכס בן עד 10 שנים
        Renovated,        // שופץ ב - 5 שנים האחרונות
        GoodCondition,    // במצב טוב, לא שופץ
        RequiresRenovation, // דורש שיפוץ
    }
}
