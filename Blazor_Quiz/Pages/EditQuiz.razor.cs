using Blazor_Quiz.Services;
using Blazor_Quiz_Class;
using Microsoft.AspNetCore.Components;

namespace Blazor_Quiz.Pages
{
    public partial class EditQuiz
    {
        [Parameter]
        public Quiz_Main Quiz { get; set; } = new(); //mo¿e te¿ jako param?
        [Inject]
        Quiz_Service QuizService { get; set; } = default!;
        [Inject]
        NotifyMinService Notify { get; set; } = default!;
        [Parameter]
        public int QuizId { get; set; } = 1;
        
        protected override async Task OnInitializedAsync()
        {
            var results = await QuizService.GetQuizAndQuestions(QuizId);  //wyœwietliæ jeszcze nazwe quizu 
            if (results.Succeeded)
            {
                Quiz = results.Data;
                Quiz.Questions.ForEach(n =>
                {
                    n.SetImage();
                });
            }
        }
        private async Task AddQuestion()
        {
            var question = await QuizService.CreateQuestionAsync(new() {QuizMainId=QuizId });
            if (!question.Succeeded)
            {
                Notify.Error(question.ErrorMessage);
                return;
            }
            Quiz.Questions.Add(question.Data);
            Notify.Success();
        }
        private async Task SaveQuestion(Question question)
        {
            var result = await QuizService.UpdateQuestionAsync(question.Id, question);
            if (!result.Succeeded)
            {
                Notify.Error(result.ErrorMessage);
                return;
            }
            Notify.Success();
        }
        private async Task DeleteQuestion(Question question)
        {
            var result = await QuizService.DeleteQuestionAsync(question.Id);
            if (!result.Succeeded)
            {
                Notify.Error(result.ErrorMessage);
                return;
            }
            Notify.Success();
            Quiz.Questions.Remove(question);
        }
    }
}