using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект хранения всех блоков на сцене
    /// </summary>
    [CreateAssetMenu(fileName = "BlocksVariable", menuName = "Variable/Blocks Variable")]
    public class BlocksVariable : RuntimeSet<Block>
    {
    }
}