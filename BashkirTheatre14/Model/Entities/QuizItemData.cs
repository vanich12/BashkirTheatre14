using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BashkirTheatre14.Model.Entities
{
    public class QuizItemData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("imagePath")]
        public string ImagePath { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("addDescription")]
        public string AddDescription { get; set; }
    }
}
