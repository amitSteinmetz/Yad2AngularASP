namespace backend.Models.Asset
{
    public enum AssetCondition
    {
        NewFromDeveloper, // חדש מקבלן
        New,              // נכס בן עד 10 שנים
        Renovated,        // שופץ ב - 5 שנים האחרונות
        GoodCondition,    // במצב טוב, לא שופץ
        RequiresRenovation, // דורש שיפוץ
    }
}
