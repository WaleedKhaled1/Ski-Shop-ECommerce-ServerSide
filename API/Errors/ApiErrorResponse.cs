namespace API.Errors
{
    public class ApiErrorResponse(int statuscode,string messege,string ?details)
    {
        public int StatusCode { get; set; } = statuscode;
        public string Message { get; set; } = messege;
        public string ?Details { get; set; }=details;

    }
}
