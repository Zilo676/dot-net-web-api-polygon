using System.Text.Json;

namespace Core.Utils.Responses;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string TraceId { get; set; }
    public string SpanId { get; set; }
    public string MfRequestId { get; set; }
    public string Message { get; set; }
    public string ExceptionMessage { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}