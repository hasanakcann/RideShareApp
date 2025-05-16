using RideShareApp.Core.Entity;
using RideShareApp.Core.Model;

namespace RideShareApp.Core.Interface;

public interface ITravelPlanBusiness
{
    ResponseModel<bool> CreateTravelPlan(TravelPlan travelPlan);

    ResponseModel<bool> PublishTravelPlan(TravelPlan travelPlan, bool isActive);

    ResponseModel<List<TravelPlan>> GetAllTravelPlans(string from, string to);

    ResponseModel<bool> RequestToTravelPlan(TravelPlan travelPlan, int userId);
}