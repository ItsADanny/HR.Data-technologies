public class CaseFan : Product
{
    public double[]? RPM {get; set;}
    public double[]? AirFlow {get; set;}
    public double[]? NoiseLevel {get; set;}
    public bool PWM {get; set;}

    public CaseFan(int id, string name, double? price, eColor[]? colors, double[]? rpm, double[]? airflow, double[]? noiseLevel, bool pwm) 
    : base(id, name, price, colors)
    {
        RPM = rpm;
        AirFlow = airflow;
        NoiseLevel = noiseLevel;
        PWM = pwm;
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