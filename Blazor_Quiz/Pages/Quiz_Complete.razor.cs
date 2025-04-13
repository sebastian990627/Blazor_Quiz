using Blazor_Quiz.Services;
using Blazor_Quiz_Class;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Blazor_Quiz.Pages
{
    public partial class Quiz_Complete
    {
        [Parameter]
        public int QuizId { get; set; } = 1;
        [Inject]
        Quiz_Service QuizService { get; set; } = default!;
        public string Name { get; set; } = "Anonymus";
        public UserQuizResult QuizResult { get; set; } = null!;
        [Inject]
        NotifyMinService Notify { get; set; } = default!;
        [Inject]
        DialogService DialogService { get; set; } = default!;
        public Quiz_Main QuizMain { get; set; } = default!;

        protected override async  Task OnInitializedAsync()
        {
            var results = await QuizService.GetQuizAndQuestions(QuizId);  //wyœwietliæ jeszcze nazwe quizu 
            if (!results.Succeeded)
            {
                return;
            }
            QuizMain = results.Data;
        }

        public async Task CreateQuiz()
        {
            var result = await QuizService.CreateResultAsync(new() { UserId = Name, TakenOn = DateTime.Now, QuizName= QuizMain.Name });
            if (!result.Succeeded) return;
            var answers = QuizMain.Questions.Select(n => new UserAnswer { QuestionId = n.Id, UserQuizResultId = result.Data.Id });
            foreach (var answer in answers)
            {
                await QuizService.CreateUserAnswerAsync(answer);
            }
            var resutQuiz = await QuizService.GetResultByIdAsync(result.Data.Id);
            if (resutQuiz.Succeeded)
            {
                QuizResult = resutQuiz.Data;
                foreach (var item in QuizResult.Answers)
                {
                    item.Question.SetImage();
                }
            }
            Notify.Success();
        }
        public async Task EndQuiz()
        {
            await UserAnswersResult();
            QuizResult.TotalQuestions = QuizResult.Answers.Count();
            QuizResult.CorrectAnswers = QuizResult.Answers.Count(n => n.IsCorrect);
            await QuizService.UpdateResultsync(QuizResult.Id, QuizResult);
            await ShowDialog();
        }
        public async Task UserAnswersResult()
        {
            foreach (var item in QuizResult.Answers)
            {
                if (item.SelectedAnswer == item.Question.CorrectAnswer)
                {
                    item.IsCorrect = true;
                }
                await QuizService.UpdateUserAnswerAsync(item.Id, item);
            }
        }
        private async Task ShowDialog()
        {
            var a = await DialogService.OpenAsync<Quiz_ShowResult>($"Wynik",
            new Dictionary<string, object>() { { "Result",QuizResult} },
            new DialogOptions() { Width = "calc(60%)", Height = "calc(60%)", Top = "calc(10%)", Style = "animation: crescendo 0.2s alternate infinite ease-in;animation-iteration-count: 1; @keyframes crescendo {0% {transform: scale(.01);}100% {transform: scale(1.0);}}" });
        }
    }
}