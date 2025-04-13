using Blazor_Quiz_Class;
using FluentValidation;

namespace Blazor_Quiz_Api.Api.Controllers
{
    public class MinimalAPIValidator:AbstractValidator<Question>
    {
        public MinimalAPIValidator()
        {
            //RuleFor(n => n.Text).NotEmpty().MinimumLength(5).WithMessage("Value of a user must be atleast 5 character");
        }
    }
}
