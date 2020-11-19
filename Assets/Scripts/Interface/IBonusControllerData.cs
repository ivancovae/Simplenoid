using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс для контроллера управления летящими бонусами
    /// </summary>
    public interface IBonusControllerData
    {
        BonusesVariable GetBonuses { get; }
        UnusedBonusesVariable GetUnusedBonuses { get; }
        BordersVariable GetBorders { get; }

        BoolVariable GetIsFaster { get; }
        BoolVariable GetIsSlowly { get; }
        BoolVariable GetIsLongBoard { get; }
        FloatVariable GetActiveTime { get; }
    }
}