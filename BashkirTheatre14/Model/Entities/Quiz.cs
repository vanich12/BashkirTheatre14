using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BashkirTheatre14.Model.Entities
{
    public record Quiz(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("questions")] IReadOnlyList<Question> Questions,
        [property: JsonPropertyName("display")] bool Display,
        [property: JsonPropertyName("terminals")] string Terminals
    );

    public record Question(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("answers")] IReadOnlyList<Answer> Answers,
        [property: JsonPropertyName("correctAnswers")] int CorrectAnswers,
        [property: JsonPropertyName("number")] int Number,
        [property: JsonPropertyName("ratios")] IReadOnlyList<string?> Ratios,
        [property: JsonPropertyName("image")] string? Image
    );

    public record Answer(
        [property: JsonPropertyName("id")] int Id,
        bool IsSelect,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("correct")] bool Correct,
        [property: JsonPropertyName("image")] string? Image
    );
}
