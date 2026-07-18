using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.DTOs.Auth;

public class RegisterOwnerDTO : RegisterDTO
{
    public string BusinessTaxID { get; set; }
    
}