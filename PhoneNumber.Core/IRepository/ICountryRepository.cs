using PhoneNumbers.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumbers.Core.IRepository
{
    public interface ICountryRepository
    {
        Country GetCountryByCode(string countryCode);
    }

}
