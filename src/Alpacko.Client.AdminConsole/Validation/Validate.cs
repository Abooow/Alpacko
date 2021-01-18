using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Alpacko.Client.AdminConsole.Validation
{
    public class Validate
    {
        private const string EmailRegex = @"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+";

        public static bool ValidateProperty<TInstance, TValue>(TInstance obj, TValue value, string propName, out List<ValidationResult> results)
        {
            ValidationContext ctx = new ValidationContext(obj)
            {
                DisplayName = propName,
                MemberName = propName
            };

            results = new List<ValidationResult>();

            bool defaultValidatorSuccess = Validator.TryValidateProperty(value, ctx, results);
            bool customValidatorSuccess = true;

            // Email validation.
            MemberInfo[] s = ctx.ObjectType.FindMembers(MemberTypes.Property, BindingFlags.Instance | BindingFlags.Public, (x, _) => x.Name == propName, null);
            if (s.Length > 0)
            {
                if (s[0].GetCustomAttributes(false).FirstOrDefault(x => x.GetType() == typeof(EmailAttribute)) is EmailAttribute email &&
                    !Regex.Match((value as string).Trim(), EmailRegex).Success)
                {
                    results.Add(new ValidationResult(email.ErrorMessage));
                    customValidatorSuccess = false;
                }
            }

            return defaultValidatorSuccess && customValidatorSuccess;
        }

        public static T GetValidInstance<T>()
        {
            Type type = typeof(T);
            T instance = (T)type.GetConstructors()[0].Invoke(new object[] { });

            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CustomAttributes.Any(x => x.AttributeType == typeof(SkipAttribute)))
                    continue;

                do
                {
                    Console.Write($"{property.Name}: ");
                    string rawInput = GetInput(property);
                    object input = Converter.ConvertFromString(property.PropertyType, rawInput, out string errorMessage);
                    if (errorMessage != null)
                    {
                        Utils.WriteLineInColor(errorMessage, Utils.ErrorColor);
                        continue;
                    }
                    property.SetValue(instance, input);

                    if (!ValidateProperty(instance, property.GetValue(instance), property.Name, out List<ValidationResult> result))
                    {
                        foreach (ValidationResult error in result)
                        {
                            Utils.WriteLineInColor(error.ErrorMessage, Utils.ErrorColor);
                        }
                        Console.WriteLine();
                        continue;
                    }

                    break;
                } while (true);
            }

            return instance;
        }

        private static string GetInput(PropertyInfo propertyInfo)
        {
            string input = "";

            if (propertyInfo.CustomAttributes.Any(x => x.AttributeType == typeof(PasswordAttribute)))
                input = Input.GetPasswordAsStar();
            else
                input = Console.ReadLine();

            foreach (object property in propertyInfo.GetCustomAttributes(false))
            {
                switch (property)
                {
                    case TrimAttribute _:
                        input = input.Trim();
                        break;

                    case ToLowerAttribute _:
                        input = input.ToLower();
                        break;

                    case ToTitleCaseAttribute _:
                        input = input.ToTitleCase();
                        break;

                    case UpperCaseFirstAttribute _:
                        input = input.UppercaseFirst();
                        break;

                    case ReplaceAttribute replace:
                        input = input.Replace(replace.OldValue, replace.NewValue);
                        break;
                }
            }

            return input;
        }
    }
}