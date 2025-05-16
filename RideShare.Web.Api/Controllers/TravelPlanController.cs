using Microsoft.AspNetCore.Mvc;
using RideShareApp.Core.Entity;
using RideShareApp.Core.Interface;
using RideShareApp.Core.Model;

namespace RideShare.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TravelPlanController : ControllerBase
{
    private readonly ITravelPlanBusiness _travelPlanBusiness;

    public TravelPlanController(ITravelPlanBusiness travelPlanBusiness)
    {
        _travelPlanBusiness = travelPlanBusiness;
    }

    [HttpGet]
    public ResponseModel<List<TravelPlan>> SearchTravelPlans([FromQuery] string from, [FromQuery] string to)
    {
        return _travelPlanBusiness.GetAllTravelPlans(from, to);
    }

    [HttpPost("create")]
    public ResponseModel<bool> Create([FromBody] TravelPlan travelPlan)
    {
        return _travelPlanBusiness.CreateTravelPlan(travelPlan);
    }

    [HttpPost("publish")]
    public ResponseModel<bool> UpdateStatus([FromBody] TravelPlan travelPlan, [FromQuery] bool isActive)
    {
        return _travelPlanBusiness.PublishTravelPlan(travelPlan, isActive);
    }

    [HttpPost("request-participation")]
    public ResponseModel<bool> RequestParticipation([FromBody] TravelPlan travelPlan, [FromQuery] int userId)
    {
        return _travelPlanBusiness.RequestToTravelPlan(travelPlan, userId);
    }
}
