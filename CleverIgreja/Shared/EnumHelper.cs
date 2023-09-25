using CleverIgreja.Shared;
using System.ComponentModel.DataAnnotations;

public static class EnumHelper
{
    public static string GetDisplayValue(Enums value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());

        if (fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) is DisplayAttribute[] attributes && attributes.Length > 0)
        {
            return attributes[0].Name;
        }

        return value.ToString();
    }
}
