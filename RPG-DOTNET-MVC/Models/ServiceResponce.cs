namespace RPG_DOTNET_MVC.Models
{
    public class ServiceResponce
    {
        public string Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}
