namespace Core.Utils.Sorting;

public class OrderParameters
{
    public string ColumnName { get; set; }
    public bool IsAscending { get; set; }

    public OrderParameters()
    {
    }

    public OrderParameters(string columnName)
    {
        ColumnName = columnName;
        IsAscending = true;
    }

    public OrderParameters(string columnName, bool isAscending)
    {
        ColumnName = columnName;
        IsAscending = isAscending;
    }
}