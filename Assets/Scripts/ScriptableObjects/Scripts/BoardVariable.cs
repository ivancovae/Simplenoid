using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект хранения доски на сцене
    /// </summary>
    [CreateAssetMenu(fileName = "BoardVariable", menuName = "Variable/Board Variable")]
    public class BoardVariable : ScriptableObject
    {
        public Board PrefabBoard;
        public Board ObjectOnScene;
    }
}