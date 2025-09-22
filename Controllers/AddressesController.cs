using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public IActionResult CreateNewAddress(CreateAddressRequest request)
    {
        var nextId = _db.Addresses.Count + 1;
        var newAddress = new Address()
        {
            Id = nextId,
            City = request.City,
            Street = request.Street,
            StreetNumber = request.StreetNumber
        };
        _db.Addresses.Add(newAddress);

        return CreatedAtAction(nameof(GetAddressById), new { id = nextId }, newAddress);
    }


    [HttpPatch("{id}")] // https://learn.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-9.0
    public async Task<IActionResult> PatchAddress(
        int id,
        [FromBody] JsonPatchDocument<Address> patchDoc)
    {
        if (patchDoc != null)
        {
            var address = _db.Addresses.Find(addr => addr.Id == id);

            patchDoc.ApplyTo(address, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return new ObjectResult(address);
        }
        else
        {
            return BadRequest(ModelState);
        }

    }



    // [HttpPatch("{id}")]
    // public IActionResult UpdateAddressById(int id, UpdateAddressRequest request)
    // {
    //     var addressToUpdate = _db.Addresses.Find(address => address.Id == id);
    //     addressToUpdate

    //     return CreatedAtAction(nameof(GetAddressById), id, addressToUpdate);
    // }


    [HttpDelete("{id}")]
    public IActionResult DeleteAddressById(int id)
    {
        try
        {
            var AddressToDelete = _db.Addresses.Find(address => address.Id == id);

            _db.Addresses.Remove(AddressToDelete);
            return CreatedAtAction(nameof(GetAddressById), new { id = AddressToDelete.Id }, AddressToDelete);
        }
        catch (NullReferenceException ex)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound();
        }
    }
}