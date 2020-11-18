using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    public interface IBonusData
    {
        BonusesVariable GetBonuses { get; }
        BoardVariable GetBoard { get; }
        BoolVariable GetIsFaster { get; }
        BoolVariable GetIsSlowly { get; }
        BoolVariable GetIsLongBoard { get; }
    }
}