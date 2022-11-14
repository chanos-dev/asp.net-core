namespace api.Model;

public class SystemDateTime : IDateTime
{
    public DateTime Now
    {
        get => DateTime.Now;
    }
}