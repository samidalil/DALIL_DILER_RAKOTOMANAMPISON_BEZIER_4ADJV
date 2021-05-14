using System.Collections.Generic;
using UnityEngine;

public delegate void OnEvent();

public abstract class Emitter : MonoBehaviour
{
    private Dictionary<string, List<OnEvent>> _listeners = new Dictionary<string, List<OnEvent>>();
    
    public void On(string ev, OnEvent fn)
    {
        if (!this._listeners.ContainsKey(ev))
            this._listeners[ev] = new List<OnEvent>();

        this._listeners[ev].Add(fn);
    }

    public void Off(string ev, OnEvent fn)
    {
        if (this._listeners.ContainsKey(ev))
        {
            this._listeners[ev].Remove(fn);

            if (this._listeners[ev].Count == 0)
                this._listeners.Remove(ev);
        }
    }

    public void Emit(string ev)
    {
        if (this._listeners.ContainsKey(ev) && this._listeners[ev].Count > 0)
            foreach (OnEvent listener in this._listeners[ev])
                listener();
    }
}
