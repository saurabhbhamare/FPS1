using System;

public class EventController
{
    private event Action baseEvent;
    public void Invoke() => baseEvent?.Invoke();
    public void AddListener(Action listener) => baseEvent += listener;
    public void RemoveListener(Action listener) => baseEvent -= listener;
}

public class EventController<T>
{
    private event Action<T> baseEvent;
    public void Invoke(T type) => baseEvent?.Invoke(type);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}
