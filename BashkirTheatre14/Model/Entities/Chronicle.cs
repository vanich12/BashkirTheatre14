using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BashkirTheatre14.Model.Entities
{
    public record Chronicle(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("description")]
        string Description,
        [property: JsonPropertyName("startAt")] string StartAt,
        [property: JsonPropertyName("endAt")] string EndAt,
        [property: JsonPropertyName("image")] string ImagePath,
        [property: JsonPropertyName("descriptionsWithoutTags")]
        string DescriptionsWithoutTags
    )
    {
        [JsonIgnore]
        public string LocalImagePath;
    }
}
