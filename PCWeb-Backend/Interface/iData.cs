public interface iData
{
    public int ID {get; set;}

    public abstract string InsertSQL();
    public abstract string UpdateSQL();
    public abstract string DeleteSQL();
    public abstract string ReadSQL();
    public abstract string ReadSQL(int id);
    public abstract static string ReadAllSQL();
}