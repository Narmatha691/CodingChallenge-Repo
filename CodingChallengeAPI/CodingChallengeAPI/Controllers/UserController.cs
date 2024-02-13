using AutoMapper;
using CodingChallengeAPI.DTO;
using CodingChallengeAPI.Entities;
using CodingChallengeAPI.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
