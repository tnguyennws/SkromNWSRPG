using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkromNWSRPG;

namespace SkromRPGTests
{
    public class Utils
    {
        private static Dictionary<string, string> Names = new Dictionary<string, string>
        {
            {"String", "string"},
            {"Int32", "int"}
        };

        private static string NameResolve(string str)
        {
            return (Names.ContainsKey(str) ? Names[str] : str);
        }

        public static void CheckFields<T>((string, string)[] fieldsToCheck)
        {
            IEnumerable<FieldInfo> fields = typeof(T).GetFields();

            foreach ((string, string) pair in fieldsToCheck)
            {
                FieldInfo field = fields.FirstOrDefault(i => i.Name == pair.Item1);

                if (field == null || NameResolve(field.FieldType.Name) != pair.Item2)
                    Assert.Fail($"La classe {typeof(T).Name} n'a pas de {pair.Item1} de type {NameResolve(pair.Item2)}");
            }
        }

        public static void CheckValues(object obj, (string, object)[] values)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields();

            foreach ((string, object) value in values)
            {
                FieldInfo field = fields.FirstOrDefault(i => i.Name == value.Item1);

                if (field == null)
                    Assert.Fail();
                object v = field.GetValue(obj);
                    
                if (v == null || v.ToString() != value.Item2.ToString())
                    Assert.Fail($"La Variable {value.Item1} ne vaut pas {value.Item2}");
            }
        }

        public static void ValidateInstantiation<T>(object[] args)
        {
            try
            {
                Activator.CreateInstance(typeof(T), args);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        public static T ValidateInstantiation<T>((string, object)[] args)
        {
            List<object> objs = new List<object>();
            T instance = default;

            foreach ((string, object) tuple in args)
            {
                objs.Add(tuple.Item2);
            }

            try
            {
                instance = (T)Activator.CreateInstance(typeof(T), objs.ToArray());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            CheckValues(instance, args);
            return (instance);
        }

        public static void RefuseInstantiation<T>(string message, params object[] args)
        {
            foreach (object[] o in args)
            {
                Assert.ThrowsException<TargetInvocationException>(() => Activator.CreateInstance(typeof(T), o), message);
            }
        }

        public static void CheckTrue(Func<bool> fct, string message)
        {
            try
            {
                if (!fct())
                    Assert.Fail(message);
            }
            catch (Exception e)
            {
                Assert.Fail(message);
            }
        }

        public static void Invoke<T>(object obj, string name, object[] args)
        {
            MethodInfo method = obj.GetType().GetMethod(name);

            if (method == null)
                Assert.Fail($"La Méthode {name} n'existe pas");

            try
            {
                method.Invoke(obj, args);
            }
            catch (Exception e)
            {
                Assert.Fail($"La Méthode {name} n'a pas les bons paramètres");
            }
        }

        public static U Invoke<T, U>(object obj, string name, object[] args)
        {
            MethodInfo method = obj.GetType().GetMethod(name);

            if (method == null)
                Assert.Fail($"La Méthode {name} n'existe pas");

            try
            {
                return ((U)method.Invoke(obj, args));
            }
            catch (Exception)
            {
                Assert.Fail($"La Méthode {name} n'a pas les bons paramètres");
            }

            return (default);
        }
    }
}
