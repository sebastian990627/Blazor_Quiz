namespace Blazor_Quiz_Class
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int UserQuizResultId { get; set; }
      //  public UserQuizResult UserQuizResult { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
