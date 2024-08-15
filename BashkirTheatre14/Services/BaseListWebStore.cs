using System.Runtime.CompilerServices;

namespace BashkirTheatre14.Services
{
    public abstract class BaseListWebStore<TData>:IListStore<TData>
    {

        public async IAsyncEnumerable<TData> GetListAsync([EnumeratorCancellation] CancellationToken token = default, params object[] args)
        {

            token.ThrowIfCancellationRequested();
            await foreach (var item in GetListAsyncOverride(args).WithCancellation(token))
            {
                token.ThrowIfCancellationRequested();
                yield return item;
            }
        }
        protected abstract IAsyncEnumerable<TData> GetListAsyncOverride(params object[] args);
    }
}
