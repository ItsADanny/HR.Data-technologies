public static class GeneralMethods
{
    public static bool IntToBool(int input) => input == 1;
    public static int BoolToInt(bool input) => input ? 1 : 0;
    public static DateTime ParseDBDateTime(string dbDateTimeString) => DateTime.Parse(dbDateTimeString, System.Globalization.CultureInfo.InvariantCulture);
    public static string SQLInjectionSanitizer(string input) => input.Replace("'", "''");
}