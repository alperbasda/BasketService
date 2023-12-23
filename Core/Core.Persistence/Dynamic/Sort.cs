namespace Core.Persistence.Dynamic;

public class Sort : ICloneable
{
    public string Field { get; set; }
    public OrderOperator OrderOperator { get; set; }

    public Sort()
    {
        Field = string.Empty;
        OrderOperator = OrderOperator.Unknown;
    }

    public Sort(string field, OrderOperator op)
    {
        Field = field;
        OrderOperator = op;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
