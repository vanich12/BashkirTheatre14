using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashkirTheatre14.Model;
using Microsoft.Extensions.Caching.Memory;

namespace BashkirTheatre14.Services
{
    public  class PreloadHttpRequestData
    {
        private readonly IMainApiClient _apiClient;
        private readonly IMemoryCache _cache;

        public PreloadHttpRequestData(IMainApiClient apiClient, IMemoryCache cache)
        {
            _apiClient = apiClient;
            _cache = cache;
        }

        //public async Task<IEnumerable<>> GetChroniclesDataAsync()
        //{

        //}
    }
}
