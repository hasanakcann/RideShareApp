using RideShareApp.Core.Entity;
using RideShareApp.Core.Interface;
using RideShareApp.Core.Model;

namespace RideShareApp.Business;

public class UserBusiness : IUserBusiness
{
    public ResponseModel<bool> Create(User user)
    {
        if (user == null)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "User cannot be null."
            };
        }

        if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "First name and last name are required."
            };
        }

        var isDuplicate = Data.Data.Instance.Users.Any(x =>
            x.FirstName.Equals(user.FirstName, StringComparison.OrdinalIgnoreCase) &&
            x.LastName.Equals(user.LastName, StringComparison.OrdinalIgnoreCase));

        if (isDuplicate)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "A user with the same name already exists."
            };
        }

        Data.Data.Instance.Users.Add(user);
        Data.Data.Save();

        return new ResponseModel<bool> { Result = true };
    }

    public ResponseModel<List<User>> GetAll()
    {
        var users = Data.Data.Instance.Users;

        if (users == null || !users.Any())
        {
            return new ResponseModel<List<User>>
            {
                Result = new List<User>(),
                Message = "No users found."
            };
        }

        return new ResponseModel<List<User>> { Result = users };
    }
}
