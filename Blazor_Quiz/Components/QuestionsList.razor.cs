using Blazor_Quiz.Services;
using Blazor_Quiz_Class;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Blazor_Quiz.Components
{
    public partial class QuestionsList
    {
        public ElementReference dropZoneElement;
        public ElementReference inputFileContainer;
        public IJSObjectReference? _module;
        public IJSObjectReference? _dropZoneInstance;
        [Inject]
        public IJSRuntime? JSRuntime { get; set; }
        [Parameter]
        public List<Question> Questions { get; set; } = new(); //mo¿e te¿ jako param?
        [Parameter]
        public bool AllowEdit { get; set; } = true;
        [Parameter]
        public EventCallback<Question> UpdateQuestion { get; set; }
        [Parameter]
        public EventCallback<Question> DeleteQuestion { get; set; }
        [Inject]
        NotifyMinService Notify { get; set; } = default!;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await JSRuntime.InvokeVoidAsync("startIntersectionObserverForAll", DotNetObjectReference.Create(this));
                    _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./DropZone.js");
                    _dropZoneInstance = await _module.InvokeAsync<IJSObjectReference>("initializeFileDropZone", dropZoneElement, inputFileContainer);
                }
            }
            catch (System.Exception ex)
            {
                Notify.Error(ex.Message);
            }
        }
        public async Task Delete(Question question)
        {
            await DeleteQuestion.InvokeAsync(question);
        }
        public async Task Update(Question question)
        {
            await UpdateQuestion.InvokeAsync(question);
        }
        protected async Task OnInputFileChange(InputFileChangeEventArgs e, Question question)
        {
            string format = e.File.ContentType.ToString();
            IBrowserFile imageFile = e.File;
            using MemoryStream ms = new();
            await imageFile.OpenReadStream(8197152).CopyToAsync(ms);
            byte[] bytes = ms.ToArray();
            question.Image = bytes;
            question.SetImage();
        }

    }
}