namespace AuthService.API.Models
{
    public class LoginResponse
    {
        public string userName {  get; set; }
        public string Token {  get; set; }  
        public bool IsSuccess {  get; set; } = false;
    }
}
