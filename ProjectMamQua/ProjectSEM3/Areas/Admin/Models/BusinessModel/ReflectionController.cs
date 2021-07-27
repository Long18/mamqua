using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace ProjectSEM3.Areas.Admin.Models.BusinessModel
{
    public class ReflectionController
    {
        //lay danh sach cac controller
        public List<Type> GetController(string namespaces)
        {
            List<Type> listController = new List<Type>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types =
                assembly.GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces))
                    .OrderBy(x => x.Name);
            return types.ToList();
        }

        //lay danh sach cac action trong controller
        public List<string> GetActions(Type controller)
        {
            List<string> listActions = new List<string>();
            IEnumerable<MemberInfo> memberInfos =
                controller.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .OrderBy(x => x.Name);
            foreach (MemberInfo method in memberInfos )
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof (NonActionAttribute)))
                {
                    listActions.Add(method.Name.ToString());
                }
            }

            return listActions;

        }
    }
}