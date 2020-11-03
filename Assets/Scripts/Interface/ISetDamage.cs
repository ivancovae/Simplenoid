using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс для столкновений с возможностью получения урона
    /// </summary>
    public interface ISetDamage : ICollidable
    {
        /// <summary>
        /// Нанесение урона
        /// </summary>
        /// <param name="damage">Значение урона</param>
        void SetDamage(int damage);
    }
}