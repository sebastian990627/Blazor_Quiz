using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_Quiz_Class
{
    public class UserQuizResult
    {
        public int Id { get; set; }
        public string QuizName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime TakenOn { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        [NotMapped]
        public double Score => TotalQuestions > 0 ? (double)CorrectAnswers / TotalQuestions * 100 : 0;
        public List<UserAnswer> Answers { get; set; }
    }
}
