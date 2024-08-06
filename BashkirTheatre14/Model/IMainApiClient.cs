using BashkirTheatre14.Model.Entities;
using Refit;

namespace BashkirTheatre14.Model;

public interface IMainApiClient
{
    [Get("/api/viktorins?terminals=Old")]
    Task<List<Quiz>> GetQuizList();
}