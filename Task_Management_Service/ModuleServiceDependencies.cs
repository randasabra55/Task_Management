using Microsoft.Extensions.DependencyInjection;
using Task_Management_Service.Abstracts;
using Task_Management_Service.Implementations;

namespace Task_Management_Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IFileService, FileService>();
            services.AddScoped<IGoogleTasksService, GoogleTasksService>();
            //services.AddHttpContextAccessor();
            services.AddScoped<ISyncGoogleTasksService, GoogleTasksSyncService>();
            //services.AddHostedService<SyncGoogleTasksBackgroundService>();
            services.AddHostedService<GoogleTasksBackgroundService>();






            return services;
        }
    }
}
