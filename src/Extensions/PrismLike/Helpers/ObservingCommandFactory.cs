using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TheXDS.MCART.Types.Extensions;
using TheXDS.MCART.ViewModel;
using TheXDS.MCART.Math;
using static TheXDS.MCART.Helpers.ReflectionHelpers;

namespace TheXDS.MCART.Helpers
{
    public partial class ConfiguredObservingCommand<T> where T : INotifyPropertyChanged
    {
        private readonly ObservingCommand command;
        private readonly T observedObject;
        private readonly List<Func<object?, bool>> canExecuteTree = new();

        public bool IsBuilt { get; private set; }

        internal ConfiguredObservingCommand(T observedObject, Action action)
        {
            command = new (this.observedObject = observedObject, action);
        }

        internal ConfiguredObservingCommand(T observedObject, Action<object?> action)
        {
            command = new(this.observedObject = observedObject, action);
        }

        internal ConfiguredObservingCommand(T observedObject, Func<Task> action)
        {
            command = new(this.observedObject = observedObject, action);
        }

        internal ConfiguredObservingCommand(T observedObject, Func<object?, Task> action)
        {
            command = new(this.observedObject = observedObject, action);
        }

        public ConfiguredObservingCommand<T> ListensTo(params Expression<Func<T, object?>>[] properties)
        {
            ListensTo<object?>(properties);
            return this;
        }

        public ConfiguredObservingCommand<T> ListensTo<TValue>(params Expression<Func<T, TValue>>[] properties)
        {
            ListensToProperty_Contract(properties);
            if (IsBuilt) throw new InvalidOperationException();
            command.RegisterObservedProperty(properties.Select(GetProperty).Select(p => p.Name).ToArray());
            return this;
        }

        public ConfiguredObservingCommand<T> ListensToCanExecute(Expression<Func<T, bool>> selector)
        {
            var pi = GetProperty(selector);
            ListensToCanExecute_Contract(pi, typeof(T));
            if (IsBuilt) throw new InvalidOperationException();
            command.RegisterObservedProperty(pi.Name);
            canExecuteTree.Add(_ => selector.Compile().Invoke(observedObject));
            return this;
        }

        public ConfiguredObservingCommand<T> CanExecute(Func<object?, bool> canExecute)
        {
            IsBuilt_Contract();
            canExecuteTree.Add(canExecute);
            return this;
        }

        public ConfiguredObservingCommand<T> CanExecute(Func<bool> canExecute)
        {
            IsBuilt_Contract();
            canExecuteTree.Add(_ => canExecute());
            return this;
        }

        public ConfiguredObservingCommand<T> CanExecuteIfNotNull(params Expression<Func<T, object?>>[] properties)
        {
            return CanExecuteIf(p => p is not null, properties);
        }

        public ConfiguredObservingCommand<T> CanExecuteIfNotDefault(params Expression<Func<T, object?>>[] properties)
        {
            return CanExecuteIf(p => p is not null && p != p.GetType().Default(), properties);
        }

        public ConfiguredObservingCommand<T> CanExecuteIfFilled(params Expression<Func<T, string?>>[] properties)
        {
            return CanExecuteIf(p => !p.IsEmpty(), properties);
        }

        public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, float>>[] properties)
        {
            return CanExecuteIf(p => p.IsValid(), properties);
        }

        public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, float?>>[] properties)
        {
            return CanExecuteIf(p => p.HasValue && p.Value.IsValid(), properties);
        }

        public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, double>>[] properties)
        {
            return CanExecuteIf(p => p.IsValid(), properties);
        }

        public ConfiguredObservingCommand<T> CanExecuteIfValid(params Expression<Func<T, double?>>[] properties)
        {
            return CanExecuteIf(p => p.HasValue && p.Value.IsValid(), properties);
        }

        public ConfiguredObservingCommand<T> CanExecuteIfNotZero<TValue>(params Expression<Func<T, TValue>>[] properties) where TValue : notnull, IComparable<TValue>
        {
            return CanExecuteIf(p => p.CompareTo((TValue)p.GetType().Default()!) != 0, properties);
        }

        private ConfiguredObservingCommand<T> CanExecuteIf<TValue>(Func<TValue, bool> predicate, params Expression<Func<T, TValue>>[] properties)
        {
            if (IsBuilt) throw new InvalidOperationException();
            ListensTo(properties);
            CanExecute(_ => properties.Select(p => p.Compile().Invoke(observedObject)).All(predicate));
            return this;
        }

        public ConfiguredObservingCommand<T> CanExecuteIfObservedIsFilled()
        {
            static bool IsFilled(object? p)
            {
                return p switch
                {
                    null => false,
                    string s => !s.IsEmpty(),
                    ValueType v => v != p.GetType().Default(),
                    _ => true
                };
            }
            if (IsBuilt) throw new InvalidOperationException();
            var props = observedObject.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite);
            command.RegisterObservedProperty(props.Select(p => p.Name).ToArray());
            canExecuteTree.Add(_ => props.Select(p => p.GetValue(observedObject)).All(IsFilled));
            return this;
        }

        public ObservingCommand Build()
        {
            if (IsBuilt) return command;
            IsBuilt = true;
            return command.SetCanExecute(o => canExecuteTree.All(p => p(o)));            
        }

        public static implicit operator ObservingCommand (ConfiguredObservingCommand<T> command) => command.Build();
    }

    public static class ObservingCommandFactory
    {
        public static ConfiguredObservingCommand<T> CreateObservingCommand<T>(this T observedObject, Action action) where T : INotifyPropertyChanged
        {
            return new ConfiguredObservingCommand<T>(observedObject, action);
        }
        public static ConfiguredObservingCommand<T> CreateObservingCommand<T>(this T observedObject, Action<object?> action) where T : INotifyPropertyChanged
        {
            return new ConfiguredObservingCommand<T>(observedObject, action);
        }
        public static ConfiguredObservingCommand<T> CreateObservingCommand<T>(this T observedObject, Func<Task> action) where T : INotifyPropertyChanged
        {
            return new ConfiguredObservingCommand<T>(observedObject, action);
        }
        public static ConfiguredObservingCommand<T> CreateObservingCommand<T>(this T observedObject, Func<object?, Task> action) where T : INotifyPropertyChanged
        {
            return new ConfiguredObservingCommand<T>(observedObject, action);
        }
    }
}
