namespace Core.Persistence.Models.Requests;

public class PageRequest : ICloneable
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
