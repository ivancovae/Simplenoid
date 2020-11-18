using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Interface
{
    public interface IBoardData
    {
        BoardVariable GetBoard { get; }
        BordersVariable GetBorders { get; }
        BonusesVariable GetBonuses { get; }
    }
}