using RideShareApp.Core.Entity;
using RideShareApp.Core.Interface;
using RideShareApp.Core.Model;

namespace RideShareApp.Business;

public class TravelPlanBusiness : ITravelPlanBusiness
{
    public ResponseModel<bool> CreateTravelPlan(TravelPlan travelPlan)
    {
        if (travelPlan == null)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "Travel plan cannot be null."
            };
        }

        Data.Data.Instance.TravelPlans.Add(travelPlan);
        Data.Data.Save();

        return new ResponseModel<bool> { Result = true };
    }

    public ResponseModel<List<TravelPlan>> GetAllTravelPlans(string from, string to)
    {
        if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
        {
            return new ResponseModel<List<TravelPlan>>
            {
                Result = new List<TravelPlan>(),
                Message = "Origin (from) and destination (to) fields cannot be empty."
            };
        }

        var list = Data.Data.Instance.TravelPlans
            .Where(x => x.From.Equals(from, StringComparison.OrdinalIgnoreCase)
                     && x.To.Equals(to, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (list.Count == 0)
        {
            return new ResponseModel<List<TravelPlan>>
            {
                Result = list,
                Message = $"No travel plans found from '{from}' to '{to}'."
            };
        }

        return new ResponseModel<List<TravelPlan>>
        {
            Result = list,
            Message = $"{list.Count} travel plan(s) found from '{from}' to '{to}'."
        };
    }


    public ResponseModel<bool> PublishTravelPlan(TravelPlan travelPlan, bool isActive)
    {
        if (travelPlan == null)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "Travel plan cannot be null."
            };
        }

        var existingPlan = Data.Data.Instance.TravelPlans.Find(x => x.Id == travelPlan.Id);
        if (existingPlan == null)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "Travel plan not found."
            };
        }

        existingPlan.IsActive = isActive;
        Data.Data.Save();

        return new ResponseModel<bool> { Result = true };
    }

    public ResponseModel<bool> RequestToTravelPlan(TravelPlan travelPlan, int userId)
    {
        if (travelPlan == null)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "Travel plan cannot be null."
            };
        }

        var existingPlan = Data.Data.Instance.TravelPlans.Find(x => x.Id == travelPlan.Id);
        if (existingPlan == null)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "Travel plan not found."
            };
        }

        var user = Data.Data.Instance.Users.Find(x => x.Id == userId);
        if (user == null)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "User not found."
            };
        }

        int passengerCount = Data.Data.Instance.Contracts.Count(x => x.TravelPlan.Id == travelPlan.Id);
        if (passengerCount >= travelPlan.SeatCapacity)
        {
            return new ResponseModel<bool>
            {
                Result = false,
                Message = "Seat capacity is full."
            };
        }

        Data.Data.Instance.Contracts.Add(new Contract
        {
            User = user,
            TravelPlan = existingPlan,
            Description = existingPlan.Description,
            IsActive = true
        });

        Data.Data.Save();

        return new ResponseModel<bool> { Result = true };
    }
}
