using Microsoft.Extensions.DependencyInjection;
using Task_Management_Infrastructure.Abstracts;
using Task_Management_Infrastructure.Implementations;
using Task_Management_Infrastructure.InfrastructureBases;

namespace Task_Management_Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IFileRepository, FileRepository>();

            /*services.AddTransient<ILessonRepository, LessonRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<ICertificateRepository, CertificateRepository>();
            services.AddTransient<IQuizEnrollmentRepository, QuizEnrollmentRepository>();
            services.AddTransient<IUserAnswerRepository, UserAnswerRepository>();*/


            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
