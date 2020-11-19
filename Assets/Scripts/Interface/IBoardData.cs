using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс для контроллера управления доской
    /// </summary>
    public interface IBoardData
    {
        BoardVariable GetBoard { get; }
        BoolVariable GetIsLongBoard { get; }
        BordersVariable GetBorders { get; }
        BonusesVariable GetBonuses { get; }
    }
}