namespace BashkirTheatre14.Services;

public interface IListStore<TData>
{
    public IAsyncEnumerable<TData?> GetListAsync(CancellationToken token = default, params object[] args);
}