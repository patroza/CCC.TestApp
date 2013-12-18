using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Caliburn.Micro;

namespace CCC.TestApp.UI.Desktop.ViewModels
{
    public abstract class ScreenBase : Screen
    {
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null) {
            if (CompareValues(storage, value))
                return false;
            storage = value;
            NotifyOfPropertyChange(propertyName);
            return true;
        }

        protected static bool CompareValues<T>(T value, T newValue) {
            return EqualityComparer<T>.Default.Equals(value, newValue);
        }

        protected IConductor GetParentScreen() {
            return (IConductor) Parent;
        }
    }
}