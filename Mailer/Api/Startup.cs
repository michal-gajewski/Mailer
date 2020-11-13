using Application;
using BackgroundWorker;
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
            services.AddSwaggerGen();

            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAddEmailCommandHandler, AddEmailCommandHandler>();
            services.AddTransient<IAddRecipientCommandHandler, AddRecipientCommandHandler>();
            services.AddTransient<IMarkEmailAsSentCommandHandler, MarkEmailAsSentCommandHandler>();
            services.AddTransient<IGetEmailsQueryHandler, GetEmailsQueryHandler>();
            services.AddTransient<IGetEmailStatusQueryHandler, GetEmailStatusQueryHandler>();
            services.AddTransient<IGetEmailQueryHandler, GetEmailQueryHandler>();
            services.AddTransient<IGetPendingEmailsQueryHandler, GetPendingEmailsQueryHandler>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmtpClient, SmtpClient>();
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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