using System.Reflection;

namespace Onova.ReleaseNotes
{
    internal static class ReflectionUtils
    {
        public static T InvokeMethod<T>(this object obj, string methodName, params object[] args)
            where T : class
        {
            var type = obj.GetType();
            var method = type.GetTypeInfo().GetMethod(methodName, BindingFlags.Public
                                                                | BindingFlags.NonPublic
                                                                | BindingFlags.Static
                                                                | BindingFlags.Instance
                                                                | BindingFlags.InvokeMethod);
            return method?.Invoke(obj, args) as T;
        }

        public static T GetFieldValue<T>(this object obj, string name)
        {
            if (obj == null || string.IsNullOrEmpty(name))
                return default(T);

            var prop = obj.GetType().GetField(name, BindingFlags.Public
                                                  | BindingFlags.NonPublic
                                                  | BindingFlags.Instance
                                                  | BindingFlags.GetField);

            T result = default(T);
            if (prop != null)
                result = (T)prop.GetValue(obj);

            return result;
        }
    }
}
