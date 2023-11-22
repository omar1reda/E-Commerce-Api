
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIS.DTOs;
using Talabat.APIS.ExtensionMethode;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.I_Services;

namespace Talabat.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager ,
            SignInManager<AppUser> signInManager , 
            ITokenServices tokenServices ,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            // Email is Existe Or no =>
            if (await EmailExists(model.Email))
                return BadRequest();

            
            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.Email.Split('@')[0]
            };

            var RegisterChek= await _userManager.CreateAsync(User, model.Password);
            if(!RegisterChek.Succeeded)
            {
                return BadRequest();
            }
            var registerReturn = new UserDTO() { Email = model.Email, DisplayName = model.DisplayName, Token = await _tokenServices.CreateToken(User) };
            return Ok(registerReturn);
       

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) { return BadRequest(); }

           var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if(!Result.Succeeded)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new UserDTO() {Email = user.Email, DisplayName=user.DisplayName , Token = await _tokenServices.CreateToken(user) }); 
            }
            

        }

       
        [HttpGet("CurrentUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
           //var user =await User.GetUserByEmailExtention( _userManager );
           var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);

           if (user == null)
                return BadRequest();

            var UserDto = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = Email,
                Token = await _tokenServices.CreateToken(user)
            };

           return Ok(UserDto);

        }


        [HttpGet("CurrentUserAddress")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AddressDTO>> GetCurrentUserAddress()
        {
            var user = await _userManager.GetUserByEmailAsync(User);
            if (user == null)
                return BadRequest();

            var addressMping = _mapper.Map<Address , AddressDTO>(user.address);

            return Ok(addressMping);
        }

        [HttpPut("UpdateUserAddress")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO addressDto)
        {
            var user =await _userManager.GetUserByEmailAsync(User);

            var AddressMaping = _mapper.Map< AddressDTO , Address>(addressDto);

            AddressMaping.Id = user.address.Id; 
            user.address = AddressMaping;
         

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest();

            return addressDto;
             


        }

        [HttpGet("EmailExists")]
        public async Task<bool> EmailExists(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            else
              return true;
        }
    }
}
