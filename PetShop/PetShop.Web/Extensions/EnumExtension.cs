using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
        var attribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
        return attribute != null ? attribute.Name : enumValue.ToString();
    }
}