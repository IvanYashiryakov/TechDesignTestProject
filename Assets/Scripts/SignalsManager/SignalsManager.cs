using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SignalsManager : MonoSingleton<SignalsManager>
{
    private abstract class ListenersContainer { }
    private class ListenersContainer<T> : ListenersContainer where T : ISignal
    {
        private List<Action<T>> _listeners = new List<Action<T>>(128);
        private List<Action<T>> _invokeListeners = new List<Action<T>>(128);

        public void Add(Action<T> listener)
        {
            _listeners.Add(listener);
        }

        public void Remove(Action<T> listener)
        {
            _listeners.Remove(listener);
        }

        public void Invoke(T data)
        {
            _invokeListeners.Clear();
            foreach (var listener in _listeners)
                _invokeListeners.Add(listener);

            foreach (var listener in _invokeListeners)
            {
                if (_listeners.Contains(listener))
                    listener.Invoke(data);
            }
        }
    }

    private Dictionary<Type, ListenersContainer> _containers = new Dictionary<Type, ListenersContainer>();
    private List<Type> _allowedSignals = new List<Type>();

    public void AddListener<T>(Action<T> listener) where T : ISignal
    {
        var container = GetListenersContainer<T>();
        container.Add(listener);
    }

    public void RemoveListener<T>(Action<T> listener) where T : ISignal
    {
        var container = GetListenersContainer<T>();
        container.Remove(listener);
    }

    private ListenersContainer<T> GetListenersContainer<T>() where T : ISignal
    {
        var type = typeof(T);
        if (_containers.ContainsKey(type))
        {
            return _containers[type] as ListenersContainer<T>;
        }

        var container = new ListenersContainer<T>();
        _containers.Add(type, container);

        return container;
    }

    public void Send<T>(T signal) where T : ISignal
    {
        var signalType = typeof(T);
        if (_allowedSignals.Count > 0 && !_allowedSignals.Contains(signalType)) return;

        var container = GetListenersContainer<T>();
        container.Invoke(signal);
    }

    public void RegisterAllowedSignal<T>() where T : ISignal
    {
        _allowedSignals.Add(typeof(T));
    }

    public void CleanupAllowedSignals()
    {
        _allowedSignals.Clear();
    }
}
