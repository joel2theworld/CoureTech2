using Microsoft.AspNetCore.Mvc;
using PhoneNumbers.Application.IService;

[ApiController]
[Route("api/[controller]")]
public class PhoneNumberController : ControllerBase
{
    private readonly ICountryService _countryService;

    public PhoneNumberController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("phoneNumber")]
    public IActionResult GetCountryDetails(string phoneNumber)
    {
        var result = _countryService.GetCountryDetailsByPhoneNumber(phoneNumber);
        if (result == null)
        {
            return NotFound("Country not found.");
        }

        return Ok(result);
    }
}
