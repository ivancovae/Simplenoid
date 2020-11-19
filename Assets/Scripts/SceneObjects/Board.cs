using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Controllers;

namespace Simplenoid
{
    /// <summary>
    /// Компонент доска
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Board : BaseObjectScene, ICollidable
    {
        public Ball Ball { get; set; }

        [SerializeField] private Vector3 _velocity = new Vector3(1.0f, 0.0f, 0.0f);
        public Vector3 Velocity => _velocity;

        [SerializeField] private Vector3 _delta = Vector3.zero;

        public Vector3 Delta
        {
            get
            {
                return _delta;
            }
            set
            {
                _delta = value;
            }
        }
        public Vector2 Size { get; private set; }
        private SpriteRenderer _spriteRenderer;
        
        public Vector3 PointLT => new Vector3(Position.x, Position.y + Size.y, Position.z);
        public Vector3 PointRT => new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z);
        public Vector3 PointRB => new Vector3(Position.x + Size.x, Position.y, Position.z);
        public Vector3 PointLB => Position;
        public Vector2 DefaultSize { get; private set; }

        [SerializeField] private float _extraSize = 3.0f;
        public float ExtraSize => _extraSize;

        private bool _isLongSize = false;

        public void ChangeSize(bool isLongSize)
        {
            if (isLongSize == _isLongSize)
                return;

            if (isLongSize)
            {
                _spriteRenderer.size = new Vector2(_spriteRenderer.size.x + ExtraSize, _spriteRenderer.size.y);
            }
            else
            {
                _spriteRenderer.size = DefaultSize;
            }
            Size = _spriteRenderer.bounds.size;
            _isLongSize = isLongSize;
        }

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Size = _spriteRenderer.bounds.size;
            DefaultSize = _spriteRenderer.size;
        }
    }
}
