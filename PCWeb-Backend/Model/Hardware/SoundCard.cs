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