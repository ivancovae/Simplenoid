using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Объект хранения значения типа float
    /// </summary>
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Variable/Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}