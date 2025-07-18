using HeartsDesireLuxury.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.Services
{
    public class CountryServices : ICounntryService
    {
        private readonly HttpClient _httpClient;

        public CountryServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<string>> GetAllCountries()
        {
            throw new NotImplementedException();


        }

        public Task<List<string>> GetStateByCountries()
        {
            throw new NotImplementedException();
        }
    }
}
