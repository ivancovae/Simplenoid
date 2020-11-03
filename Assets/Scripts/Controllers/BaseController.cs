using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Базовый класс контроллеров общего поведения
    /// </summary>
    public abstract class BaseController : MonoBehaviour
    {
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }
    }

}