using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPizza.Core.Entities.Identity;
using WebPizza.Core.Interfaces.ControllerInterfaces;
using WebPizza.Core.Interfaces;
using AutoMapper;
using WebPizza.Core.DTO.Account;

namespace WebPizza.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController(
      IJwtTokenService jwtTokenService,
      IMapper mapper,
      IImageService imageService,
      IAccountsControllerService service,
      UserManager<UserEntity> userManager
  ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] SignInVm model)
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
        var user = mapper.Map<UserEntity>(vm);

        user.Photo = await imageService.SaveImageAsync(vm.Image);

        try
        {
            await service.SignUpAsync(user, vm.Password);
            return Ok(new JwtTokenResponse
            {
                Token = await jwtTokenService.CreateTokenAsync(user)
            });
        }
        catch (Exception)
        {
            imageService.DeleteImageIfExists(user.Photo);
            return StatusCode(500, "Exception create user!");
        }
    }

}
