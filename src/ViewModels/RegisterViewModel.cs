using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "The name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "The e-mail is required")]
    [EmailAddress(ErrorMessage = "Invalid e-mail")]
    public string Email { get; set; }
}