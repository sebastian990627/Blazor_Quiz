using Blazor_Quiz_Class;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Quiz_Api.Api.Controllers
{
    public static class MinimalAPI
    {

        public static WebApplication RegisterEndPoints(this WebApplication app)
        {
            //app.MapGet("/Users", MinimalAPI.GetAll).Produces<List<Users>>().WithTags("User").RequireAuthorization();  //WithValidator -metoda napisana przez nas
            //app.MapPut("/User/{id}", MinimalAPI.Update).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound).Accepts<Users>("application/json").WithTags("User").WithValidator<Users>(); //tags-nazwa api
            //app.MapDelete("/User/{id}", MinimalAPI.Delete).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound).WithTags("User").ExcludeFromDescription(); //ExcludeFromDescription- niewidoczne w dokumentacji

            // Table Question
            app.MapGet("/Quiz/GetAllQuestions", MinimalAPI.GetAllQuestions).Produces<List<Question>>();
            app.MapPost("/Quiz/CreateQuestion", MinimalAPI.CreateQuestion).Produces<Question>(StatusCodes.Status201Created).Accepts<Question>("application/json").WithValidator<Question>(); //accepts -wartość wysyłana 
            app.MapPut("/Quiz/UpdateQuestion/{id}", MinimalAPI.UpdateQuestion).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound).Accepts<Question>("application/json").WithValidator<Question>(); //tags-nazwa api !!
            app.MapDelete("/Quiz/DeleteQuestion/{id}", MinimalAPI.DeleteQuestion).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound); //ExcludeFromDescription- niewidoczne w dokumentacji


            // Table UserQuizResult
            app.MapGet("/Quiz/GetResultById/{id}", MinimalAPI.GetResultAndAnswerById).Produces<UserQuizResult>().Produces(StatusCodes.Status404NotFound);//produces -statusy może byc sama liczba
            app.MapGet("/Quiz/GetAllResult", MinimalAPI.GetAllResult).Produces<List<UserQuizResult>>();
            app.MapPost("/Quiz/CreateResult", MinimalAPI.CreateResult).Produces<UserQuizResult>(StatusCodes.Status201Created).Accepts<UserQuizResult>("application/json"); //accepts -wartość wysyłana 
            app.MapPut("/Quiz/UpdateResult/{id}", MinimalAPI.UpdateResult).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound).Accepts<UserQuizResult>("application/json"); //tags-nazwa api !!

            // Table UserAnswer
            app.MapPost("/Quiz/CreateUserAnswer", MinimalAPI.CreateUserAnswer).Produces<UserAnswer>(StatusCodes.Status201Created).Accepts<UserAnswer>("application/json"); //accepts -wartość wysyłana 
            app.MapPut("/Quiz/UpdateUserAnswer/{id}", MinimalAPI.UpdateUserAnswer).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound).Accepts<UserAnswer>("application/json"); //tags-nazwa api !!

            // Table Quiz_Main
            app.MapGet("/Quiz/GetQuizAndQuestions/{id}", MinimalAPI.GetQuizAndQuestions).Produces<Quiz_Main>().Produces(StatusCodes.Status404NotFound);//produces -statusy może byc sama liczba
            app.MapPost("/Quiz/CreateQuiz", MinimalAPI.CreateQuiz).Produces<Quiz_Main>(StatusCodes.Status201Created).Accepts<Quiz_Main>("application/json"); //accepts -wartość wysyłana 
            app.MapGet("/Quiz/GetAllQuiz", MinimalAPI.GetAllQuiz).Produces<List<Quiz_Main>>().Produces(StatusCodes.Status404NotFound);//produces -statusy może byc sama liczba
            app.MapPut("/Quiz/UpdateQuiz/{id}", MinimalAPI.UpdateQuiz).Produces(StatusCodes.Status204NoContent).Produces(StatusCodes.Status404NotFound).Accepts<Quiz_Main>("application/json"); //tags-nazwa api !!

            return app;
        }

        #region Question
        [Authorize(Roles = "Adim")]
        public static IResult GetAllQuestions(Context context)
        {
            var users = context.Questions.ToList();
            return Results.Ok(users);
        }

        [Authorize]
        public static IResult CreateQuestion(Context context, Question question, IValidator<Question> validator)
        {
            var validationResult = validator.Validate(question);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            context.Add(question);
            context.SaveChanges();
            return Results.Created($"/User/{question.Id}", question);

        }

        [Authorize]
        public static IResult UpdateQuestion(Context context, Question question, int id, IValidator<Question> validator)
        {
            bool b = context.Questions.Where(n => n.Id == id).Any();
            if (!b)
            {
                return Results.NotFound();
            }
            var validationResult = validator.Validate(question);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            context.Update(question);
            context.SaveChanges();
            return Results.NoContent();

        }

        [Authorize]
        public static IResult DeleteQuestion(Context context, int id)
        {
            var b = context.Questions.Where(n => n.Id == id).FirstOrDefault();
            if (b == null)
            {
                return Results.NotFound();
            }
            context.Remove(b);
            context.SaveChanges();
            return Results.NoContent();
        }
        #endregion

        #region UserQuizResult
        [Authorize]
        public static IResult GetResultAndAnswerById(Context context, int id)
        {
            var users = context.UserQuizResults.Where(n => n.Id == id).Include(n => n.Answers).ThenInclude(n => n.Question).FirstOrDefault() ?? new();
            return Results.Ok(users);
        }

        [Authorize(Roles = "Adim")]
        public static IResult GetAllResult(Context context)
        {
            var users = context.UserQuizResults.ToList();
            return Results.Ok(users);
        }

        [Authorize]
        public static IResult CreateResult(Context context, UserQuizResult result)
        {
            context.Add(result);
            context.SaveChanges();
            return Results.Created($"/Quiz/GetResultById/{result.Id}", result);
        }
        #endregion

        #region UserAnswer
        [Authorize]
        public static IResult CreateUserAnswer(Context context, UserAnswer result)
        {
            context.Add(result);
            context.SaveChanges();
            return Results.Created($"/Quiz/GetUserAnswerById/{result.Id}", result);
        }
        [Authorize]
        public static IResult UpdateUserAnswer(Context context, UserAnswer question, int id)
        {
            bool b = context.UserAnswers.Where(n => n.Id == id).Any();
            if (!b)
            {
                return Results.NotFound();
            }
            context.Update(question);
            context.SaveChanges();
            return Results.NoContent();
        }

        [Authorize]
        public static IResult UpdateResult(Context context, UserQuizResult result, int id)
        {
            bool b = context.UserQuizResults.Where(n => n.Id == id).Any();
            if (!b)
            {
                return Results.NotFound();
            }
            context.Update(result);
            context.SaveChanges();
            return Results.NoContent();
        }
        #endregion

        #region Quiz_Main
        [Authorize]
        public static IResult CreateQuiz(Context context, Quiz_Main quiz)
        {
            context.Add(quiz);
            context.SaveChanges();
            return Results.Created($"/Quiz/GetResultById/{quiz.Id}", quiz);
        }
        [Authorize]
        public static IResult GetQuizAndQuestions(Context context, int id)
        {
            var users = context.Quiz_Main.Where(n => n.Id == id).Include(n => n.Questions).FirstOrDefault() ?? new();
            return Results.Ok(users);
        }

        [Authorize]
        public static IResult UpdateQuiz(Context context, Quiz_Main quiz, int id)
        {
            bool b = context.Quiz_Main.Where(n => n.Id == id).Any();
            if (!b)
            {
                return Results.NotFound();
            }
            context.Update(quiz);
            context.SaveChanges();
            return Results.NoContent();
        }
        [Authorize]
        public static IResult GetAllQuiz(Context context)
        {
            var quizes = context.Quiz_Main.ToList() ?? new();
            return Results.Ok(quizes);
        }
        #endregion
    }

}
