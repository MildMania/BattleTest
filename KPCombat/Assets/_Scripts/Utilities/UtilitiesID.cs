using System.Collections;
static partial class Utilities
{
    /// <summary>
    /// Returns Unique ID for object
    /// </summary>
    /// <param name="themeType">Check ConstantsID</param>
    /// <param name="objType">Check ConstantsID</param>
    /// <param name="objID">Check ConstantsID</param>
    /// <returns></returns>
    public static int GetUID(int themeType, int objType, int objID)
    {
        string value = themeType.ToString() + objType.ToString() + objID.ToString("D2");

        int uniqueID;

        if (!int.TryParse(value, out uniqueID))
            return -1;
        else
            return uniqueID;
    }

    public static int GetUIDPrefix(int themeType, int objType)
    {
        string value = themeType.ToString() + objType.ToString();

        int prefixID;

        if (!int.TryParse(value, out prefixID))
            return -1;
        else
            return prefixID;
    }
}
