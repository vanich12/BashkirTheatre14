using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BashkirTheatre14.Model.Entities
{
    public record Info(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("description")]
        string Description,
        [property: JsonPropertyName("workTime")]
        string WorkTime,
        [property: JsonPropertyName("address")]
        string Address,
        [property: JsonPropertyName("phone")] string Phone,
        [property: JsonPropertyName("email")] string Email,
        [property: JsonPropertyName("phoneMinistry")]
        string PhoneMinistry,
        [property: JsonPropertyName("emailFirstMinistry")]
        string EmailFirstMinistry,
        [property: JsonPropertyName("emailSecondMinistry")]
        string EmailSecondMinistry,
        [property: JsonPropertyName("image")] string ImagePath
    )
    {
        [JsonIgnore] public string LocalImagePath;
    }
}
