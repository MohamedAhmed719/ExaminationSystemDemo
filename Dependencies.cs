using ExaminationSystemDemo.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ExaminationSystemDemo;

public static class Dependencies
{
    public static IServiceCollection AddDepdendencies(this IServiceCollection services,IConfiguration configuration)
    {


        services.AddControllers();

        services.AddHttpContextAccessor();

        services.AddSwaggerConfig();
        services.AddFluentValidationConfig();
        services.AddMapsterConfig();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IInstructorService, InstructorService>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IQuestionServce, QuestionService>();
        services.AddScoped<IChoiceService, ChoiceService>();
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddAuthConfig(configuration);
        return services;
    }


    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }


    public static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddFluentValidationAutoValidation();
        return services;    
    }

    public static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {


        var mappingConfig = TypeAdapterConfig.GlobalSettings;

        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }
    public static IServiceCollection AddAuthConfig(this IServiceCollection services,IConfiguration configuration)
    {


        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        var jwtOptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtOptions?.Issuer, 
                ValidAudience = jwtOptions?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Key))
            };
        });

        return services;
    }
}
