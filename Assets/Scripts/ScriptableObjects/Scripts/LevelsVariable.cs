using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [CreateAssetMenu(fileName = "LevelsVariable", menuName = "Variable/Levels Variable")]
    public class LevelsVariable : RuntimeSet<Level>
    {
        [SerializeField] private List<Level> _prefabsLevels;
    }
}