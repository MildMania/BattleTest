using System;

static partial class Utilities
{

    public static T IdentifyObjectEnum<T>(string objectname)
    {
        T type = (T)Enum.Parse(typeof(T), objectname);
        return type;
    }

    public static int HighestValueInEnum(Type enumType)
    {
        Array values = Enum.GetValues(enumType);
        int highestValue = (int)values.GetValue(0);
        for (int index = 0; index < values.Length; ++index)
        {
            if ((int)values.GetValue(index) > highestValue)
            {
                highestValue = (int)values.GetValue(index);
            }
        }

        return highestValue;
    }
}
