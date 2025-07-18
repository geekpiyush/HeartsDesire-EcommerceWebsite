using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Core.ServiceContracts
{
    public interface ICounntryService
    {
        Task<List<string>> GetAllCountries();
        Task<List<string>> GetStateByCountries();
    }
}
