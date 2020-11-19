using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Динамический список однородных объектов
    /// </summary>
    /// <typeparam name="T">Тип объектов</typeparam>
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        private List<T> _items = new List<T>();
        /// <summary>
        /// Объекты
        /// </summary>
        public IReadOnlyList<T> Items => _items;
        /// <summary>
        /// Добавить в коллекцию
        /// </summary>
        /// <param name="t"></param>
        public void Add(T t)
        {
            if (!_items.Contains(t)) _items.Add(t);
        }
        /// <summary>
        /// Удалить из коллекции
        /// </summary>
        /// <param name="t"></param>
        public void Remove(T t)
        {
            if (_items.Contains(t)) _items.Remove(t);
        }
        /// <summary>
        /// Очистить коллекцию
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }
    }
}