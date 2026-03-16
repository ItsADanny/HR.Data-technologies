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
}