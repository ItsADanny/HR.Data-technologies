public class OpticalDrive : Product
{
    public int? BlueRayDrive {get; set;}
    public int? DVDDrive {get; set;}
    public int? CDDrive {get; set;}
    public string? BlueRayDriveWrite {get; set;}
    public string? DVDDriveWrite {get; set;}
    public string? CDDriveWrite {get; set;}

    public OpticalDrive(int id, string name, double? price, eColor[]? colors, int? bluerayDrive, int? dvdDrive, int? cdDrive, 
                        string? bluerayDriveWrite, string? dvdDriveWrite, string? cdDriveWrite)
    : base(id, name, price, colors)
    {
        BlueRayDrive = bluerayDrive;
        DVDDrive = dvdDrive;
        CDDrive = cdDrive;
        BlueRayDriveWrite = bluerayDriveWrite;
        DVDDriveWrite = dvdDriveWrite;
        CDDriveWrite = cdDriveWrite;
    }
}