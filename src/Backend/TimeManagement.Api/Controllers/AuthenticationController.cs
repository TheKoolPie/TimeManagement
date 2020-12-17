using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApi.Context.Authentication;
using TimeManagementApi.Models.Authentication;
using TimeManagementApi.Response;
using TimeManagementApi.Settings;

namespace TimeManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginModel model)
        {
            var response = new AuthResponse();
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                JwtSettings jwtSettings = new JwtSettings();
                _configuration.GetSection("JWT").Bind(jwtSettings);

                var token = CreateToken(jwtSettings, authClaims);

                response.IsSuccess = true;
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(response);
            }
            response.IsSuccess = false;
            response.Message = $"No user with user name '{model.Username}'";
            return Unauthorized(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterModel model)
        {
            var result = new AuthResponse();
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                result.IsSuccess = false;
                result.Message = "User name is not available";
                return Conflict(result);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
            {
                result.IsSuccess = false;
                result.Message = "Could not create user";
                return Conflict(result);
            }

            result.IsSuccess = true;
            result.Message = "Successfully created user!";
            return Ok(result);
        }

        private JwtSecurityToken CreateToken(JwtSettings settings, List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecurityKey));
            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                expires: DateTime.Now.AddHours(settings.ExpiryInDays),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return token;
        }
    }
}
