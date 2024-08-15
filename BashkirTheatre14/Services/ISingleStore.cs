using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashkirTheatre14.Services
{
    public interface ISingleStore<TData>
    {
        Task<TData?> GetSingleOrDefaultAsync(CancellationToken token = default, params object[] args);
    }
}
