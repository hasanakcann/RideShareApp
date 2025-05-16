namespace RideShareApp.Core.Entity;

public class Contract
{
    public TravelPlan TravelPlan { get; set; }
    public User User { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
}