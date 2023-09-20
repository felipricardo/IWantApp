using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IWantApp.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(
        LoginRequest loginRequest, 
        IConfiguration configuration, 
        UserManager<IdentityUser> userManager,
        ILogger<TokenPost> log,
        IWebHostEnvironment environment)
    {
        log.LogInformation("Getting token");
        log.LogWarning("Warning");
        log.LogError("Error");

        // Encontra o usuário pelo e-mail
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

        // Verifica se o usuário existe e se a senha está correta
        if (user == null || !userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            return Results.BadRequest();

        // Cria e configura o token JWT
        var claims = userManager.GetClaimsAsync(user).Result;
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        });
        subject.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuration["JwtBearerTokenSettings:Audience"],
            Issuer = configuration["JwtBearerTokenSettings:Issuer"],
            Expires = environment.IsDevelopment() || environment.IsStaging() ?
                DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(2)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Retorna o token
        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}
