using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        private List<T> _items = new List<T>();
        public IReadOnlyList<T> Items => _items;
        public void Add(T t)
        {
            if (!_items.Contains(t)) _items.Add(t);
        }

        public void Remove(T t)
        {
            if (_items.Contains(t)) _items.Remove(t);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}