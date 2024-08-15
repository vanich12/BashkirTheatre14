using BashkirTheatre14.Model.Entities;
using Refit;

namespace BashkirTheatre14.Model;

public interface IMainApiClient
{
    [Get("/api/viktorins?terminals=Old")]
    Task<List<Quiz>> GetQuizList();

    [Get("/api/chronicles?terminals=Old")]
    Task<List<Chronicle>> GetChroniclesList();

    [Get("/api/abouts?terminals=Old")]
    Task<List<Info>> GetInfoList();

    [Get("/api/abouts?terminals=Old")]
    Task<Info> GetInfo();
}