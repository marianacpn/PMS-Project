using Domain.Interfaces;
using Domain.Loggable.Attributes;
using Shared.Support.ClassExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Application.Common.Loggable
{
    public static class LoggableMethods
    {
        public static T DeepClone<T>(T a)
        {
            using MemoryStream stream = new MemoryStream();

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }

        public static string LogToCreate<T>(Logger<T> obj) where T : class, ILoggableEntity
        {
            Type type = obj.Entity.GetType();

            string result = AutoLog(obj.Entity, type);
            result += AutoLogRelationshipCreate(obj.Entity, type);

            return result;
        }

        private static string AutoLogRelationshipCreate<T>(T obj, Type type)
        {
            if (obj == null)
                throw new ArgumentNullException("Parametro 'obj' não pode ser nulo");

            if (type == null)
                throw new ArgumentNullException("Parametro 'type' não pode ser nulo");


            List<PropertyInfo> propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                                           prop => Attribute.IsDefined(prop, typeof(LoggableRelationshipAttribute))).ToList();

            string result = string.Empty;

            foreach (PropertyInfo pi in propertyInfos)
            {
                LoggableRelationshipAttribute display = pi.GetCustomAttribute(typeof(LoggableRelationshipAttribute)) as LoggableRelationshipAttribute;

                object value = type.GetProperty(pi.Name).GetValue(obj, null);

                if (value == null)
                    continue;

                Type relationshipType = value.GetType();

                object relationshipValue = relationshipType.GetProperty(display.PropertyName).GetValue(value, null);
                object relationshipTypeName = display.PropertyType ?? relationshipType.GetProperty("Type").GetValue(value, null);

                result += $" {display.Preposition} {relationshipTypeName}: '{relationshipValue}', ";
            }

            if (!string.IsNullOrEmpty(result))
                result = result.Remove(result.Length - 2).Insert(result.Length - 2, ".");

            return result;
        }

        public static string LogToDelete<T>(T obj)
        {
            Type type = obj.GetType();

            string result = AutoLog(obj, type);

            return result;
        }



        public static (bool, string) LogToUpdate<T>(Logger<T> obj) where T : class, ILoggableEntity
        {
            Type type = obj.Entity.GetType();
            
            string result = AutoLogUpdate(obj.Entity, type, obj.OldEntity);
            result += AutoLogRelationshipUpdate(obj.Entity, type, obj.OldEntity);

            return (!string.IsNullOrEmpty(result), result);
        }

        private static string AutoLogRelationshipUpdate<T>(T obj, Type type, object oldObj)
        {
            if (obj == null)
                throw new ArgumentNullException("Parametro 'obj' não pode ser nulo");

            if (type == null)
                throw new ArgumentNullException("Parametro 'type' não pode ser nulo");

            if (oldObj == null)
                throw new ArgumentNullException("Parametro 'OldValue' não pode ser nulo");


            List<PropertyInfo> propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                                           prop => Attribute.IsDefined(prop, typeof(LoggableRelationshipAttribute))).ToList();

            string result = string.Empty;

            foreach (PropertyInfo pi in propertyInfos)
            {
                LoggableRelationshipAttribute display = pi.GetCustomAttribute(typeof(LoggableRelationshipAttribute)) as LoggableRelationshipAttribute;

                object value = type.GetProperty(pi.Name).GetValue(obj, null);
                object valueId = type.GetProperty(pi.Name + "Id").GetValue(obj, null);

                object oldValue = type.GetProperty(pi.Name).GetValue(oldObj, null);
                object oldValueId = type.GetProperty(pi.Name + "Id").GetValue(oldObj, null);

                if (value == null)
                    continue;

                if (valueId.Equals(oldValueId))
                    continue;

                Type relationshipType = value.GetType();
                PropertyInfo relationshipPropInfo = relationshipType.GetProperty(display.PropertyName);

                if (relationshipPropInfo == null)
                    throw new NullReferenceException($"Não existe propriedade '{display.PropertyName}' na classe {relationshipType.Name}");

                object relationshipTypeName = display.PropertyType ?? relationshipType.GetProperty("Type").GetValue(value, null);

                object relationshipValue = value == null ? "Nenhum" : relationshipPropInfo.GetValue(value, null);
                object relationshipOldValue = oldValue == null ? "Nenhum" : relationshipPropInfo.GetValue(oldValue, null);

                result += $" {display.PrepositionOnUpdate ?? display.Preposition} {relationshipTypeName} de '{GetTextFromType(relationshipPropInfo, relationshipOldValue)}' para '{GetTextFromType(relationshipPropInfo, relationshipValue)}', ";
            }

            if (!string.IsNullOrEmpty(result))
                result = result.Remove(result.Length - 2).Insert(result.Length - 2, ".");

            return result;
        }

        private static string AutoLogUpdate<T>(T obj, Type type, object oldObj)
        {
            if (oldObj == null)
                return "";

            string oldText = string.Empty;
            string newText = string.Empty;

            List<PropertyInfo> propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                                            prop => Attribute.IsDefined(prop, typeof(LoggableAttribute))).ToList();

            foreach (PropertyInfo pi in propertyInfo)
            {
                object newValue = type.GetProperty(pi.Name).GetValue(obj, null);
                object oldValue = type.GetProperty(pi.Name).GetValue(oldObj, null);

                if (newValue != oldValue && (newValue == null || !newValue.Equals(oldValue)))
                {
                    LoggableAttribute display = pi.GetCustomAttribute(typeof(LoggableAttribute)) as LoggableAttribute;

                    string prefix = string.Empty, sufix = string.Empty;
                    if (!string.IsNullOrEmpty(display.PrefixText))
                        prefix += " ";

                    if (!string.IsNullOrEmpty(display.SufixText))
                        sufix = " " + sufix;

                    string textForOld = GetTextFromType(pi, oldValue);

                    string textForNew = GetTextFromType(pi, newValue);

                    if (pi.GetCustomAttribute(typeof(LoggableSensitiveDataAttribute)) is LoggableSensitiveDataAttribute sensitiveData)
                    {
                        if (!string.IsNullOrEmpty(sensitiveData.MessageOnUpdate))
                        {
                            string text = sensitiveData.MessageOnUpdate.Contains("{0}") ? string.Format(sensitiveData.MessageOnUpdate, display.Name) : sensitiveData.MessageOnUpdate;
                            newText += $"{text}, ";
                        }

                        continue;
                    }

                    oldText += $"{display.Name}: {prefix}{textForOld}{sufix}, ";
                    newText += $"{display.Name}: {prefix}{textForNew}{sufix}, ";
                }
            }

            if (oldText.Length > 0)
            {
                oldText = oldText.Remove(oldText.Length - 2);
                oldText = $"de ({oldText}) ";
            }


            if (newText.Length > 0)
            {
                newText = newText.Remove(newText.Length - 2);
                newText = $"para ({newText})";
            }


            return $"{oldText}{newText}";
        }



        private static string AutoLog<T>(T obj, Type type)
        {
            if (obj == null)
                throw new ArgumentNullException("Parametro 'obj' não pode ser nulo");

            if (type == null)
                throw new ArgumentNullException("Parametro 'type' não pode ser nulo");


            List<PropertyInfo> propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                                           prop => Attribute.IsDefined(prop, typeof(LoggableAttribute))).ToList();

            string result = string.Empty;

            foreach (PropertyInfo pi in propertyInfos)
            {
                LoggableAttribute display = pi.GetCustomAttribute(typeof(LoggableAttribute)) as LoggableAttribute;

                if (pi.GetCustomAttribute(typeof(LoggableSensitiveDataAttribute)) is LoggableSensitiveDataAttribute sensitiveData)
                    continue;

                object value = type.GetProperty(pi.Name).GetValue(obj, null);
                string prefix = string.Empty, sufix = string.Empty;

                if (!string.IsNullOrEmpty(display.PrefixText))
                    prefix += " ";

                if (!string.IsNullOrEmpty(display.SufixText))
                    sufix = " " + sufix;

                string text = GetTextFromType(pi, value);

                result += $"{display.Name}: {prefix}{text}{sufix}, ";
            }

            if (!string.IsNullOrEmpty(result))
                result = result.Remove(result.Length - 2).Insert(0, "(").Insert(result.Length - 1, ")");

            return result;
        }

        private static string GetTextFromType(PropertyInfo pi, object value)
        {
            return PropertyInfoExtensions.GetTextFromType(pi, value);
        }
    }
}
