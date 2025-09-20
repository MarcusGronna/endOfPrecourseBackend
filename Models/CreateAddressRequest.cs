using System.ComponentModel.DataAnnotations;

public class CreateAddressRequest
{
    [Required(ErrorMessage = "Must have city")]
    public string City { get; set; }
    [Required(ErrorMessage = "Must have street")]
    public string Street { get; set; }
    [Required(ErrorMessage = "Must have street number")]
    public string StreetNumber { get; set; }
}