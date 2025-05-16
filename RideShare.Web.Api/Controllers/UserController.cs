using Microsoft.AspNetCore.Mvc;
using RideShareApp.Core.Entity;
using RideShareApp.Core.Interface;
using RideShareApp.Core.Model;

namespace AdessoRideShare.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserBusiness _userBusiness;

    public UserController(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [HttpGet]
    public ResponseModel<List<User>> GetAllUsers()
    {
        return _userBusiness.GetAll();
    }

    [HttpPost("create")]
    public ResponseModel<bool> CreateUser([FromBody] User user)
    {
        return _userBusiness.Create(user);
    }
}
