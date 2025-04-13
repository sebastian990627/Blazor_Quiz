using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Quiz_Api.Api
{
    public class ContextServices
    {
        [Inject]
        IServiceProvider ISP { get; set; }

        public ContextServices(IServiceProvider ISP)
        {
            this.ISP = ISP;
        }


        public T Create<T>() where T : class
        {
            try
            {

                var scope = ISP.CreateScope();
                var db3 = scope.ServiceProvider
                     .GetRequiredService<T>();
                return db3;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
