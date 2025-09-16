namespace Core.Api.QueryFilter
{
    public class QueryFilter
    {
        public List<Filter> Filters { get; set; }
        public string Logic { get; set; }
    }

    public class Filter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Logic { get; set; }
        public object Value { get; set; }
        public List<Filter> Filters { get; set; }
    }
}
