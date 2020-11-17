using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Controllers;

namespace Simplenoid
{
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
                return _delta * Time.deltaTime;
            }
            set
            {
                _delta = value;
            }
        }
        public Vector2 Size { get; private set; }
        private SpriteRenderer _spriteRenderer;

        [SerializeField] bool _isLongBoard = false;
        public bool IsLongBoard 
        {
            get => _isLongBoard;
            set
            {
                _isLongBoard = value;
                if (_isLongBoard)
                {
                    _spriteRenderer.size = new Vector2(_spriteRenderer.size.x + _extraSize, _spriteRenderer.size.y);
                } 
                else
                {
                    _spriteRenderer.size = _defaultSize;
                }
                Size = _spriteRenderer.bounds.size;
            }
        }
        [SerializeField] private Vector2 _defaultSize;
        [SerializeField] private float _extraSize = 3.0f;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Size = _spriteRenderer.bounds.size;
            _defaultSize = _spriteRenderer.size;
        }
    }
}
