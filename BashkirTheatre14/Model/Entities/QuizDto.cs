using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BashkirTheatre14.Model.Entities
{
    public record QuizDto(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("questions")]
        IReadOnlyList<Question> Questions,
        [property: JsonPropertyName("display")] bool Display
    );

    public record QuizModel(QuizDto QuizDto)
    {
        
        private readonly IEnumerator<Question> _questionEnumerator = QuizDto.Questions.GetEnumerator();
        public Question? ToNextQuestion() => _questionEnumerator.MoveNext() ? _questionEnumerator.Current : null;

    }

    public record Question(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("answers")] IReadOnlyList<Answer> Answers)
    {
        public bool CheckAnswer(Answer answer) => answer.Correct;
    }

    public record Answer(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("correct")]
        bool Correct
    );
}
