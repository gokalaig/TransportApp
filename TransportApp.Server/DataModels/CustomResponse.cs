namespace TransportApp.Server.DataModels
{
    public class CustomResponse<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public string errorDetails { get; set; }
        public T? data { get; set; }
    }
}
