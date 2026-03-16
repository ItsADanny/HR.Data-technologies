public class FanController : Product
{
    public int Channels {get; set;}
    public double? ChannelWattage {get; set;}
    public bool PWM {get; set;}
    public double? FormFactor {get; set;}

    public FanController(int id, string name, double? price, eColor[]? colors, int channels, double? channelWattage, bool pwm, double? formFactor)
    : base(id, name, price, colors)
    {
        Channels = channels;
        ChannelWattage = channelWattage;
        PWM = pwm;
        FormFactor = formFactor;
    }

    public override string InsertSQL()
    {
        throw new NotImplementedException();
    }

    public override string UpdateSQL()
    {
        throw new NotImplementedException();
    }

    public override string DeleteSQL()
    {
        throw new NotImplementedException();
    }

    public override string ReadSQL()
    {
        throw new NotImplementedException();
    }

    public override string ReadSQL(int id)
    {
        throw new NotImplementedException();
    }

    public new static string ReadAllSQL()
    {
        throw new NotImplementedException();
    }
}