using System.Text.Json.Serialization;
using MapControlLib.Models;

namespace BashkirTheatre14.Model.Entities.Map
{
    public record Terminal(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("floor")] Floor Floor,
        [property: JsonPropertyName("area")] int? Area,
        [property: JsonPropertyName("node")] int? Node) : ITerminal;
}
