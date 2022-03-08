using System.ComponentModel.DataAnnotations;  
  
namespace Identity.Api;
public class AccessTokenModel  
{  
    [Required(ErrorMessage = "Token is required")]  
    public string Token { get; set; }    
}  