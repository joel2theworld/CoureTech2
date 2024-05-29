using Microsoft.EntityFrameworkCore;
using PhoneNumbers.Core.Entities;
using PhoneNumbers.Core.IRepository;
using PhoneNumbers.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneNumbers.Infrastructure.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Country GetCountryByCode(string countryCode)
        {
            return _context.Countries
                .Include(c => c.CountryDetails)
                .FirstOrDefault(c => c.CountryCode == countryCode);
        }
    }

}
