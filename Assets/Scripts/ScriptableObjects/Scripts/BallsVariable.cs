using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект хранения всех шаров на сцене
    /// </summary>
    [CreateAssetMenu(fileName = "BallsVariable", menuName = "Variable/Balls Variable")]
    public class BallsVariable : RuntimeSet<Ball>
    {
        public Ball PrefabBall;
    }
}