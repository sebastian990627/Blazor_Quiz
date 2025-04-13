using FluentValidation;
using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Quiz_Api.Api
{
    public static class ValidatorExtension
    {
        public static RouteHandlerBuilder WithValidator<T>(this RouteHandlerBuilder builder) where T : class
        {
            builder.Add(endpointbuilder =>
            {
                var orgin = endpointbuilder.RequestDelegate;

                endpointbuilder.RequestDelegate = async httpContext =>
                {
                    var validator = httpContext.RequestServices.GetRequiredService<IValidator<T>>();
                    httpContext.Request.EnableBuffering();
                    var body = await httpContext.Request.ReadFromJsonAsync<T>();
                    if (body == null)
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await httpContext.Response.WriteAsync("Bad Model request to body");
                        return;
                    }
                    var result = validator.Validate(body);
                    if (!result.IsValid)
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await httpContext.Response.WriteAsJsonAsync(result.Errors);
                        return;
                    }
                    httpContext.Request.Body.Position = 0;
                   await  orgin(httpContext);
                };
            });
            return builder;
        }
    }
}
