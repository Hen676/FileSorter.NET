using Avalonia.Controls;
using Avalonia.Controls.Templates;
using FileSorter.NET.ViewModels;
using System;

namespace FileSorter.NET
{
    public class ViewLocator : IDataTemplate
    {
        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }

        Control? ITemplate<object?, Control?>.Build(object? param)
        {
            var name = param.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }
    }
}