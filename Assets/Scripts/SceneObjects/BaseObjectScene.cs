using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    /// <summary>
    /// Базовый объект на сцене
    /// </summary>
    public abstract class BaseObjectScene : MonoBehaviour
    {
        protected Vector3 _position;
        protected GameObject _instanceObject;
        protected Transform _transform;
        protected string _name;
        
        protected virtual void Awake()
        {
            _instanceObject = gameObject;
            _name = _instanceObject.name;
            _transform = transform;
        }
        protected virtual void Start()
        {

        }
        protected virtual void Update()
        {

        }
        protected virtual void FixedUpdate()
        {

        }
        protected virtual void LateUpdate()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        /// <summary>
        /// Ссылка на экземпляр объекта
        /// </summary>
        public GameObject InstanceObject => _instanceObject;
        // имя объекта
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                InstanceObject.name = _name;
            }
        }
        /// <summary>
        /// Позиция объекта на сцене
        /// </summary>
        public Vector3 Position
        {
            get
            {
                if (InstanceObject != null)
                {
                    _position = _transform.position;
                }
                return _position;
            }
            set
            {
                _position = value;
                if (InstanceObject != null)
                {
                    _transform.position = _position;
                }
            }
        }
        /// <summary>
        /// Сериализация трансформа объекта
        /// </summary>
        public Transform Transform => _transform;
    }
}
