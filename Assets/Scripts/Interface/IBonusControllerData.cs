using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    public interface IBonusControllerData
    {
        BonusesVariable GetBonuses { get; }
        BordersVariable GetBorders { get; }
    }
}