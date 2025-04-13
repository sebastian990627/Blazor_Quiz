using Blazor_Quiz.Services;
using Blazor_Quiz_Class;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Blazor_Quiz.Pages
{
    public partial class Quiz_UserResult
    {
        [Parameter]
        public int ResultId  { get; set; }       
        [Inject]
        Quiz_Service QuizService { get; set; } = default!;
        public string Name { get; set; } = "Anonymus";
        public UserQuizResult QuizResult { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var resutQuiz = await QuizService.GetResultByIdAsync(ResultId);
            if (resutQuiz.Succeeded)
            {
                QuizResult = resutQuiz.Data;
                foreach (var item in QuizResult.Answers)
                {
                    item.Question.SetImage();
                }
            }
        }

    }
}