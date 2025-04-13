using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blazor_Quiz_Class
{
    public class Question
    {
        [NotMapped]
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public byte[]? Image { get; set; }
        public string QuestionA { get; set; }= "";
        public string QuestionB { get; set; }= "";
        public string QuestionC { get; set; } = "";
        public string QuestionD { get; set; } = "";
        public string CorrectAnswer { get; set; } = "";
        public int? QuizMainId { get;set; }
        [JsonIgnore]
        public Quiz_Main QuizMain { get; set; }
        [NotMapped]
        public string ImageString { get; set; } = null!;

        public void SetImage()
        {
            if (Image is null) return;
            var base64 = Convert.ToBase64String(Image);
            ImageString = $"data:image/jpeg;base64,{base64}";
        }
    }
}
