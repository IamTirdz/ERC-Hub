using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ERC.Hub.Common.Extensions
{
    public static class EnumExtension
    {
        private static string GetAttributeValue<TEnum, TAttribute>(this TEnum enumValue, Func<TAttribute, string> valueSelector) 
            where TEnum : Enum
            where TAttribute : Attribute
        {
            var field = enumValue.GetType().GetField(enumValue.ToString())!;
            var attribute = field.GetCustomAttribute<TAttribute>();

            return attribute == null ? enumValue.ToString() : valueSelector(attribute);
        }

        public static string GetDescription(this Enum enumValue) =>
            enumValue.GetAttributeValue<Enum, DescriptionAttribute>(attr => attr.Description);

        public static string GetDisplayName(this Enum enumValue) =>
            enumValue.GetAttributeValue<Enum, DisplayAttribute>(attr => attr.Name!);
    }
}
