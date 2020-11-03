using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс предустановки контроллеров
    /// </summary>
    public interface IAwake
    {
        /// <summary>
        /// Предустановка контроллера
        /// </summary>
        void OnAwake();
    }
}