using PhoneNumbers.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumbers.Application.IService
{
    public interface ICountryService
    {
        CountryDto GetCountryDetailsByPhoneNumber(string phoneNumber);
    }

}
