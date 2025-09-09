namespace Core.Utils.Sorting;

public interface IQueryParametersSorting
{
    /// <summary>
    /// Точное имя поля
    /// </summary>
    public string Sort { get; set; }
    /// <summary>
    /// Признак обратного порядка
    /// </summary>
    public bool DESC { get; set; }

    public OrderParameters Order { get; }
}