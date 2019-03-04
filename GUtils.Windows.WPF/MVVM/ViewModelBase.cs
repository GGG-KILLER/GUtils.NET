﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GUtils.Windows.WPF.MVVM
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Does the logic for calling <see cref="PropertyChanged" /> if the value has changed (and also
        /// sets the value of the field)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetField<T> ( ref T field, T newValue, [CallerMemberName] String propertyName = null )
        {
            if ( field.Equals ( newValue ) )
                return;
            field = newValue;
            this.OnPropertyChanged ( propertyName );
        }

        /// <summary>
        /// Invokes <see cref="PropertyChanged" />
        /// </summary>
        /// <param name="propertyName"></param>
        /// <exception cref="ArgumentNullException">
        /// Invoked if a <paramref name="propertyName" /> is not provided (and the value isn't auto-filled
        /// by the compiler)
        /// </exception>
        protected virtual void OnPropertyChanged ( [CallerMemberName] String propertyName = null ) =>
            this.PropertyChanged?.Invoke (
                this,
                new PropertyChangedEventArgs ( propertyName ?? throw new ArgumentNullException ( nameof ( propertyName ) ) ) );
    }
}
