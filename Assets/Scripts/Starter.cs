using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Предустановка загружаемой сцены
    /// </summary>
    public abstract class Starter : MonoBehaviour
    {
        protected virtual void Awake() {}
        protected virtual void Start() {}
    }
}