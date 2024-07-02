using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPizza.Constants;
using WebPizza.Data.Entities.Identity;
using WebPizza.Services.ControllerServices.Interfaces;
using WebPizza.Services.Interfaces;
using WebPizza.ViewModels.Account;
using WebPizza.Services;

namespace WebPizza.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController(
        IJwtTokenService jwtTokenService,
        IAccountsControllerService service,
        UserManager<UserEntity> userManager
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] SignInVm model)
        {
            UserEntity? user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Wrong authentication data");

            return Ok(new JwtTokenResponse
            {
                Token = await jwtTokenService.CreateTokenAsync(user)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromForm] RegisterVm vm)
        {
            try
            {
                var user = await service.SignUpAsync(vm);

                return Ok(new JwtTokenResponse
                {
                    Token = await jwtTokenService.CreateTokenAsync(user)
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "Exception create user!");
            }
        }

    }
}
