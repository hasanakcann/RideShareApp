namespace RideShareApp.Core.Model;

public class ResponseModel<T>
{
    public T Result { get; set; }
    public string Message { get; set; }
}
