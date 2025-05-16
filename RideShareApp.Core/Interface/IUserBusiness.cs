using RideShareApp.Core.Entity;
using RideShareApp.Core.Model;

namespace RideShareApp.Core.Interface;

public interface IUserBusiness
{
    ResponseModel<bool> Create(User user);

    ResponseModel<List<User>> GetAll();
}
