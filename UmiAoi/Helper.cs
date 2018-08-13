using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UmiAoi
{
    public static class Helper
    {
        public static void BindingHelper(BindingModel bindingModel)
        {
            if (bindingModel.BindingElement == null) throw new NullReferenceException(nameof(bindingModel.BindingElement));
            var binding = new Binding();
            if (bindingModel.Source != null) binding.Source = bindingModel.Source;
            if (bindingModel.Path != null) binding.Path = new PropertyPath(bindingModel.Path);
            if (bindingModel.ElementName != null) binding.ElementName = bindingModel.ElementName;            
            binding.Mode = bindingModel.BindingMode;
            bindingModel.BindingElement.SetBinding(bindingModel.Property, binding);
        }

        public static TValue GetValue<TValue>(Object t, params string[] name) 
        {
            TValue value = default(TValue);
            var obj = t;
            var nameMsg = "";
            try
            {
                for (var i = 0; i < name.Length; i++)
                {
                    nameMsg += "[" + name[i] + "]";
                    PropertyInfo[] propertyInfo = obj.GetType().GetProperties();
                    var p = propertyInfo.FirstOrDefault(x => x.Name == name[i]);
                    obj = p.GetValue(obj, null);
                }
                if (obj != null)
                    value = (TValue)Convert.ChangeType(obj, typeof(TValue));
            }
            catch (Exception ex)
            {
                throw new Exception(nameMsg + ex.Message);
            }
            return value;
        }
    }

    public class BindingModel
    {
        public FrameworkElement BindingElement { get; set; }
        public DependencyProperty Property { get; set; }
        public Object Source { get; set; }
        public string Path { get; set; }
        public BindingMode BindingMode { get; set; }
        public String ElementName { get; set; }
    }
}
