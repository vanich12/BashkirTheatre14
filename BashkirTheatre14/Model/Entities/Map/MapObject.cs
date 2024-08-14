using System.Text.Json.Serialization;
using MapControlLib.Models;

namespace BashkirTheatre14.Model.Entities.Map
{
    public record Image(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("image")] string Url);

    public record MapObject(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("images")] IReadOnlyList<Image> Images,
        [property: JsonPropertyName("floor")] Floor Floor,
        [property: JsonPropertyName("node")] int? Node,
        [property: JsonPropertyName("area")] int? Area,
        [property: JsonPropertyName("infrastructure")] bool IsInfrastructure
    ) : INavigationObject,ISearchable
    {
        public string SearchProperty => Title;
    }
}
