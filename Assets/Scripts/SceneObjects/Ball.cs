using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;

namespace Simplenoid
{
    /// <summary>
    /// Компонент Мячика
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : BaseObjectScene, ICollidable
    {
        [SerializeField] private float _speed = 2.0f;
        private Vector3 _velocity = new Vector3(1.0f, 1.0f, 0.0f);
        public Vector3 Velocity => _velocity * _speed;
        [SerializeField] private Vector3 _delta = Vector3.zero;

        /// <summary>
        /// Дельта перемещения (dx, dy, dz)
        /// </summary>
        public Vector3 Delta { 
            get 
            {
                return _delta;
            }
            set
            {
                _delta = value;
            }
        }

        /// <summary>
        /// Размер Мячика
        /// </summary>
        public Vector2 Size { get; private set; }
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private int _damage = 1;
        public int Damage => _damage;

        public Vector3 PointLT => new Vector3(Position.x, Position.y + Size.y, Position.z);

        public Vector3 PointRT => new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z);

        public Vector3 PointRB => new Vector3(Position.x + Size.x, Position.y, Position.z);

        public Vector3 PointLB => Position;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Size = _spriteRenderer.bounds.size;
        }
    }
}