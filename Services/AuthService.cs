using ExaminationSystemDemo.Abstractions.Consts;
using ExaminationSystemDemo.Authentication;
using ExaminationSystemDemo.Contracts.Authentiaction;
using ExaminationSystemDemo.Contracts.Instructors;
using ExaminationSystemDemo.Errors;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
namespace ExaminationSystemDemo.Abstractions;

public class AuthService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
    IJwtProvider jwtProvider,
    ApplicationDbContext context) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<AuthResponse>> GetJwtAsync(string email,string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if(result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var (token, expiresIn) = _jwtProvider.GenerateJwtToken(user,roles);

            var response = new AuthResponse(user.Id, user.FirstName, user.LastName, user.Email!, token, expiresIn);

            return Result.Success(response);
        }

        return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);
    }

    public async Task<Result> StudentRegisterAsync(StudentRegisterRequest request,CancellationToken cancellationToken)
    {
        var isEmailExists = await _context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (isEmailExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();

        user.UserName = request.Email;

        var result = await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user,DefaultRoles.Student);

        if (result.Succeeded)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //todo Send Email
            var student = new Student { Name = $"{user.FirstName} {user.LastName}" ,Grade = request.Grade,UserId = user.Id};
            await _context.AddAsync(student);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description));
    }

    public async Task<Result> InstructorRegisterAsync(InstructorRequest request, CancellationToken cancellationToken)
    {
        var isEmailExists = await _context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (isEmailExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();

        user.UserName = request.Email;

        var result = await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, DefaultRoles.Instructor);

        if (result.Succeeded)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //todo Send Email
            var instructor = new Instructor { Name = $"{user.FirstName} {user.LastName}" , UserId = user.Id,Salary = request.Salary,Address = request.Address};
            await _context.AddAsync(instructor);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description));
    }
}
