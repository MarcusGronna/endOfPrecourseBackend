using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private Db _db;
    public AddressesController(Db db)
    {
        _db = db;
    }

    [HttpGet]
    public List<Address> GetAllAddresses()
    {
        return _db.Addresses;
    }

    [HttpGet("{id}")]
    public Address? GetAddressById(int id)
    {
        return _db.Addresses.Find(address => address.Id == id);
    }

    // [HttpPost]
    // public IActionResult CreateNewAddress(CreateAddressRequest request)
    // {
    //     return CreatedAtAction(nameof(GetAddressById) );
    // }
}