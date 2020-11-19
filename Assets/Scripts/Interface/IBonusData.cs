using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    /// <summary>
    /// Интерфейс для менеджера по бонусам
    /// </summary>
    public interface IBonusData
    {
        BonusesVariable GetBonuses { get; }
        UnusedBonusesVariable GetUnusedBonuses { get; }
    }
}