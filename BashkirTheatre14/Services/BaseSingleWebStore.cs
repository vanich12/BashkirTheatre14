using System.Threading;
using System.Threading.Tasks;

namespace BashkirTheatre14.Services
{
    public abstract class BaseSingleWebStore<TData> : ISingleStore<TData>
    {
        public async Task<TData?> GetSingleOrDefaultAsync(CancellationToken token = default, params object[] args)
        {
            token.ThrowIfCancellationRequested();
            var result = await GetSingleOrDefaultAsyncOverride(args).ConfigureAwait(false);
            token.ThrowIfCancellationRequested();
            return result;
        }

        protected abstract Task<TData?> GetSingleOrDefaultAsyncOverride(params object[] args);
    }
}
