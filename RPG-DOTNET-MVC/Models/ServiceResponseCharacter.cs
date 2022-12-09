namespace RPG_DOTNET_MVC.Models
{
    public class ServiceResponseCharacter
    {
        public List<Character> Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
    }
}
