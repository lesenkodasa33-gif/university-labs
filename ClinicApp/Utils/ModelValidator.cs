
using System;
using System.Globalization;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils
{
    public static class ModelValidator
    {
        public static ValidationResult Validate(object obj)
        {
            ValidationResult result = new ValidationResult();

            if (obj == null)
            {
                result.AddError("Object cannot be null.");
                return result;
            }

            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );

            foreach (PropertyInfo property in properties)
            {
                if (property.GetIndexParameters().Length > 0 ||
                    property.GetMethod == null)
                    continue;

                object? value;

                try
                {
                    value = property.GetValue(obj);
                }
                catch (Exception)
                {
                    result.AddError(
                        $"{property.Name}: Cannot read property value."
                    );
                    continue;
                }

                // Required validation
                RequiredAttribute? required =
    property.GetCustomAttribute<RequiredAttribute>();

                if (required != null)
                {
                    bool isEmpty = value == null ||
                        (value is string requiredText &&
                         string.IsNullOrWhiteSpace(requiredText));

                    if (isEmpty)
                    {
                        string message = string.IsNullOrWhiteSpace(
                            required.ErrorMessage)
                            ? $"{property.Name} is required."
                            : required.ErrorMessage;

                        result.AddError(message);
                    }
                }

                // MaxLength validation
                MaxLengthAttribute? maxLength =
                    property.GetCustomAttribute<MaxLengthAttribute>();

                if (maxLength != null && value != null)
                {
                    if (value is string stringValue)
                    {
                        if (stringValue.Length > maxLength.Length)
                        {
                            string message =
                                string.IsNullOrWhiteSpace(maxLength.ErrorMessage)
                                ? $"{property.Name} exceeds maximum length of {maxLength.Length}."
                                : maxLength.ErrorMessage;

                            result.AddError(message);
                        }
                    }
                    else
                    {
                        result.AddError(
                            $"{property.Name}: MaxLength can only be used with string properties."
                        );
                    }
                }

                // MinValue validation
                MinValueAttribute? minValue =
    property.GetCustomAttribute<MinValueAttribute>();

                if (minValue != null && value != null)
                {
                    Type propertyType =
                        Nullable.GetUnderlyingType(property.PropertyType)
                        ?? property.PropertyType;

                    if (!IsNumericType(propertyType))
                    {
                        result.AddError(
                            $"{property.Name}: MinValue can only be used with numeric properties."
                        );
                        continue;
                    }

                    try
                    {
                        double number = Convert.ToDouble(
                            value, CultureInfo.InvariantCulture
                        );

                        if (double.IsNaN(number) ||
                            double.IsInfinity(number) ||
                            number < minValue.Min)
                        {
                            string message =
                                string.IsNullOrWhiteSpace(minValue.ErrorMessage)
                                ? $"{property.Name} must be at least {minValue.Min}."
                                : minValue.ErrorMessage;

                            result.AddError(message);
                        }
                    }
                    catch (Exception)
                    {
                        result.AddError(
                            $"{property.Name}: Invalid numeric value."
                        );
                    }
                }
            }

            return result;
        }

        private static bool IsNumericType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            return type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong) ||
                   type == typeof(float) ||
                   type == typeof(double) ||
                   type == typeof(decimal);
        }

        public static void PrintInfo(Type type)
        {
            if (type == null)
            {
                Console.WriteLine("Type cannot be null.");
                return;
            }

            Console.WriteLine($"Type: {type.FullName}");
            Console.WriteLine("Properties:");

            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );

            if (properties.Length == 0)
            {
                Console.WriteLine("No public properties found.");
                return;
            }

            foreach (PropertyInfo property in properties)
            {
                Console.WriteLine(
                    $"\nProperty: {property.Name}"
                );

                Console.WriteLine(
                    $"Type: {property.PropertyType.Name}"
                );

                object[] attributes = property.GetCustomAttributes(true);

                if (attributes.Length == 0)
                {
                    Console.WriteLine("Attributes: none");
                    continue;
                }

                Console.WriteLine("Attributes:");

                foreach (object attribute in attributes)
                {
                    if (attribute is RequiredAttribute required)
                    {
                        Console.WriteLine(
                            $"  Required: {required.ErrorMessage}"
                        );
                    }
                    else if (attribute is MaxLengthAttribute max)
                    {
                        Console.WriteLine(
                            $"  MaxLength: {max.Length}"
                        );
                    }
                    else if (attribute is MinValueAttribute min)
                    {
                        Console.WriteLine(
                            $"  MinValue: {min.Min}"
                        );
                    }
                    else
                    {
                        Console.WriteLine(
                            $"  {attribute.GetType().Name}"
                        );
                    }
                }
            }
        }
    }
}
