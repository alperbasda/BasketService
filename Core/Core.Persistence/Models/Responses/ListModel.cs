using Core.Persistence.Dynamic;
using Core.Persistence.Requests;

namespace Core.Persistence.Models.Responses;

public class ListModel<T> : BasePageableModel
{
    private IList<T> _items;

    public IList<T> Items
    {
        get => _items ??= new List<T>();
        set => _items = value;
    }

    public PageRequest PageRequest { get; set; }

    public DynamicQuery DynamicQuery { get; set; }
}
