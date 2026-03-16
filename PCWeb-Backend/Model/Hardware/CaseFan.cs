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
}