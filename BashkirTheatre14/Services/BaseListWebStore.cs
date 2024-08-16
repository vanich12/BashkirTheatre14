using System.Runtime.CompilerServices;

namespace BashkirTheatre14.Services
{
    public abstract class BaseListWebStore<TData>:IAsyncEnumerable<TData>
    {
        protected abstract IAsyncEnumerable<TData> GetListAsyncOverride();
        public async IAsyncEnumerator<TData> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            await foreach (var item in GetListAsyncOverride().WithCancellation(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return item;
            }
        }
    }
}
