using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    public interface IBonusData
    {
        BonusesVariable GetBonuses { get; }
        UnusedBonusesVariable GetUnusedBonuses { get; }
    }
}