using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс для объекта с которым можно столкнуться 
    /// </summary>
    public interface ICollidable
    {
        /// <summary>
        /// Размер объекта
        /// </summary>
        Vector2 Size { get; }
        /// <summary>
        /// Позиция объекта
        /// </summary>
        Vector3 Position { get; }
    }
}