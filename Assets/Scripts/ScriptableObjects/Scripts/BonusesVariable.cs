using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект хранения всех летящих бонусов на сцене
    /// </summary>
    [CreateAssetMenu(fileName = "BonusesVariable", menuName = "Variable/Bonuses Variable")]
    public class BonusesVariable : RuntimeSet<Bonus>
    {
        public Bonus[] PrefabsBonuses;
    }
}