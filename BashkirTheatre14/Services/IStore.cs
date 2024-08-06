namespace BashkirTheatre14.Services;

public interface IStore<TData>
{
    public Task<TData?> GetAsync(CancellationToken token = default,int ? id=null);
}