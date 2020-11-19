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

        /// <summary>
        /// Точка слева сверху
        /// </summary>
        Vector3 PointLT { get; }
        /// <summary>
        /// Точка справа сверху
        /// </summary>
        Vector3 PointRT { get; }
        /// <summary>
        /// Точка справа снизу
        /// </summary>
        Vector3 PointRB { get; }
        /// <summary>
        /// Точка слева снизу
        /// </summary>
        Vector3 PointLB { get; }
    }
}