namespace RideShareApp.Core.Entity;

public class TravelPlan
{
    public int Id { get; set; }
    public User User { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime Date { get; set; }
    public int SeatCapacity { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}