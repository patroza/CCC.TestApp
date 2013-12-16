using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace CCC.TestApp.UI.Desktop.Models
{
    public class ModelBase : PropertyChangedBase
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