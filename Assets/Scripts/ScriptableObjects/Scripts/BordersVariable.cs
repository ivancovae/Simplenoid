using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект хранения всех границ на экране
    /// </summary>
    [CreateAssetMenu(fileName = "BordersVariable", menuName = "Variable/Borders Variable")]
    public class BordersVariable : RuntimeSet<Border>
    {
        public GameObject PrefabBorder;
    }
}