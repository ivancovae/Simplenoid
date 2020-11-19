using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект всех уровней на сцене
    /// </summary>
    [CreateAssetMenu(fileName = "LevelsVariable", menuName = "Variable/Levels Variable")]
    public class LevelsVariable : RuntimeSet<Level>
    {
        public List<Level> PrefabsLevels;
        public int CurrentIndexLevel = 0;

        public Level ObjectOnScene;
    }
}