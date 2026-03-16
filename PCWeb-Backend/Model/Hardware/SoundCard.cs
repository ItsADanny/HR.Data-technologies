public class SoundCard : Product
{
    public double? Channels {get; set;}
    public int? DigitalAudio {get; set;}
    public int? SNR {get; set;}
    public int? SampleRate {get; set;}
    public string? Chipset {get; set;}
    public string? ConnectionInterface {get; set;}

    public SoundCard(int id, string name, double? price, double? channels, int? digitalAudio, int? snr,
                     int? sampleRate, string? chipset, string? connectionInterface)
    : base(id, name, price, null)
    {
        Channels = channels;
        DigitalAudio = digitalAudio;
        SNR = snr;
        SampleRate = sampleRate;
        Chipset = chipset;
        ConnectionInterface = connectionInterface;
    }
}