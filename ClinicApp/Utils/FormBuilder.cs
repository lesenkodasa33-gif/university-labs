
using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils
{
    public static class FormBuilder
    {
        public static T Build<T>() where T : new()
        {
            T obj = new T();

            Type type = typeof(T);

            Console.WriteLine();
            Console.WriteLine($"=== Створення об'єкта: {type.Name} ===");

            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.Public | BindingFlags.Instance
            );

            foreach (PropertyInfo property in properties)
            {
                // Пропускаємо властивості без публічного setter
                if (property.SetMethod == null ||
                    !property.SetMethod.IsPublic ||
                    property.GetIndexParameters().Length > 0)
                {
                    continue;
                }

                Console.WriteLine();
                Console.Write($"{property.Name} ({property.PropertyType.Name})");

                PrintHints(property);

                Console.WriteLine();

                Type propertyType =
                    Nullable.GetUnderlyingType(property.PropertyType)
                    ?? property.PropertyType;

                bool isNullable =
                    Nullable.GetUnderlyingType(property.PropertyType) != null;

                while (true)
                {
                    Console.Write("Введіть значення: ");

                    string? input = Console.ReadLine();

                    if (input == null)
                    {
                        Console.WriteLine("Введення завершено.");
                        return obj;
                    }

                    // Рядкові значення
                    if (propertyType == typeof(string))
                    {
                        property.SetValue(obj, input);
                        break;
                    }

                    // Дозволяємо пропустити nullable-властивості
                    if (isNullable && string.IsNullOrWhiteSpace(input))
                    {
                        property.SetValue(obj, null);
                        break;
                    }

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine(
                            "Значення не може бути порожнім.");
                        continue;
                    }

                    try
                    {
                        TypeConverter converter =
                            TypeDescriptor.GetConverter(propertyType);

                        if (!converter.CanConvertFrom(typeof(string)))
                        {
                            Console.WriteLine(
                                "Цей тип не підтримує введення з консолі.");
                            continue;
                        }

                        object? value = converter.ConvertFrom(
                            null,
                            CultureInfo.CurrentCulture,
                            input
                        );

                        if (value == null && !isNullable)
                        {
                            Console.WriteLine("Некоректне значення.");
                            continue;
                        }

                        property.SetValue(obj, value);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(
                            "Некоректне значення. Спробуйте ще раз.");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Об'єкт успішно створено.");

            return obj;
        }

        private static void PrintHints(PropertyInfo property)
        {
            RequiredAttribute? required =
                property.GetCustomAttribute<RequiredAttribute>();

            MaxLengthAttribute? maxLength =
                property.GetCustomAttribute<MaxLengthAttribute>();

            MinValueAttribute? minValue =
                property.GetCustomAttribute<MinValueAttribute>();

            if (required != null)
            {
                Console.Write(" [Обов'язкове поле]");
            }

            if (maxLength != null)
            {
                Console.Write($" [Максимум {maxLength.Length} символів]");
            }

            if (minValue != null)
            {
                Console.Write($" [Мінімальне значення {minValue.Min}]");
            }
        }
    }
}
