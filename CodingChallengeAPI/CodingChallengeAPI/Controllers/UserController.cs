using AutoMapper;
using CodingChallengeAPI.DTO;
using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Model;
using CodingChallengeAPI.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodingChallengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILog _logger;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration, ILog logger)
        {
            this.userService = userService;
            this._mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
        }
        [HttpPost, Route("Register")]
        [AllowAnonymous]
        //
        public IActionResult AddUser(UserDTO userdto)
        {
            try
            {
                User user = _mapper.Map<User>(userdto);
                var result = userService.AddUser(user);
                if (result.Success)
                {
                    _logger.Info("User added successfully");
                    return StatusCode(200, user);
                }
                else
                {
                    _logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetAllUsers")]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        //
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = userService.GetAllUsers();
                List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetUserById/{userId}")]
        [AllowAnonymous]
        //
        public IActionResult GetUserById(int userId)
        {
            try
            {
                User user = userService.GetUserById(userId);
                if (user != null)
                {
                    return StatusCode(200, user);
                }
                else
                {
                    _logger.Error($"User not found");
                    return StatusCode(200);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut, Route("EditUser")]
        [AllowAnonymous]
        //
        public IActionResult EditUser(User userdto)
        {
            try
            {

                var result = userService.EditUser(userdto);
                if (result.Success)
                {
                    _logger.Info(result.Message);
                    return StatusCode(200, result.Message);
                }
                else
                {
                    _logger.Error(result.Message);
                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteUser/{UserId}")]
        [AllowAnonymous]
        //
        public IActionResult Deleteuser(int userId)
        {
            try
            {
                var result = userService.DeleteUser(userId);
                if (result.Success)
                {
                    return StatusCode(200, result.Message);
                }
                else
                {

                    return StatusCode(400, result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(400, ex.Message);
            }
        }


        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        //
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.Email, login.Password);
                AuthResponse authReponse = new AuthResponse();
                if (user != null)
                {
                    authReponse.UserId = user.UserId;
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Email,user.UserEmail),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

    }
}
