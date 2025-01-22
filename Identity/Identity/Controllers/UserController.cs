using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dominio;
using WebAPI.Dominio.Dto;
using WebAPI.Dominio.NovaPasta;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        // GET: api/<User>
        [HttpGet]
        public  IActionResult Get()
        {
            return Ok(new UserDto());
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<User>
        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = userDto.UserName,
                        Email = userDto.Email,
                        NomeCompleto = userDto.NomeCompleto
                    };

                    var result = await _userManager.CreateAsync(user, userDto.Password);

                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByNameAsync(userDto.UserName);

                        if (appUser == null)
                        {
                            return BadRequest("Usuário não encontrado após criar usuário");
                        }
                        var token = GenerateJwtToken(appUser).Result;
                        //var confirmationEmail = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);
                        //System.IO.File.WriteAllText("confirmationLink.txt", confirmationEmail);
                        return Ok($"Usuário criado com sucesso: Token {token}");
                    }

                }

                else
                {
                    return BadRequest("Usuário já existe");
                }

                return Unauthorized("Erro ao criar usuário");
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar usuário {ex.Message}");
            }

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLogin.UserName);

                var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.FindByNameAsync(userLogin.UserName);

                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    var token = GenerateJwtToken(appUser).Result;
                    return Ok(new { token, userToReturn });
                }

                return Unauthorized("Erro ao logar");
            }
            catch
            {
                return StatusCode(500, "Erro ao logar");
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
