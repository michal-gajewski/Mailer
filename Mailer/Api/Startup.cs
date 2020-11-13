using DataAccess.CommandsHandlers;
using DataAccess.QueriesHandlers;
using Database;
using Domain.Commands;
using Domain.Queries;
using Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MailerDbContext>(options => options.UseInMemoryDatabase(databaseName: "Mailer"));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAddEmailCommandHandler, AddEmailCommandHandler>();
            services.AddTransient<IAddRecipientCommandHandler, AddRecipientCommandHandler>();
            services.AddTransient<IGetEmailsQueryHandler, GetEmailsQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}