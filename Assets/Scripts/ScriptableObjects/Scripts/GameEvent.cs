using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект сохранения события
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Event/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        /// <summary>
        /// Вызов события
        /// </summary>
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }
        /// <summary>
        /// Регистрация слушателя
        /// </summary>
        /// <param name="listener">Слушатель</param>
        public void RegisterListener(GameEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }
        /// <summary>
        /// Отписать слушателя
        /// </summary>
        /// <param name="listener">Слушатель</param>
        public void UnregisterListener(GameEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }
    }
}