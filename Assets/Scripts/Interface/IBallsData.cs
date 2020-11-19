using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс для менеджера шаров
    /// </summary>
    public interface IBallsData
    {
        BallsVariable GetBalls { get; }
        BoardVariable GetBoard { get; }
    }
}