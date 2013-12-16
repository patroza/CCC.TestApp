using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public abstract class ScreenBase : Screen
    {
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            NotifyOfPropertyChange(propertyName);
            return true;
        }
    }
}
