using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Controllers;

namespace Simplenoid
{
    /// <summary>
    /// Компонент Доски
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Board : BaseObjectScene, ICollidable
    {
        private string _horizontal = "Horizontal";
        private string _jump = "Jump";
        /// <summary>
        /// Мячик при начале движения
        /// </summary>
        public Ball Ball { get; set; }

        [SerializeField] private Vector3 _velocity = new Vector3(1.0f, 0.0f, 0.0f);

        private Vector3 _delta = Vector3.zero;
        [SerializeField] private Vector2 _size;
        /// <summary>
        /// Размер Доскии
        /// </summary>
        public Vector2 Size => _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] bool _isLongBoard = false;
        /// <summary>
        /// Состояние режима удлененной доски
        /// </summary>
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
                _size = _spriteRenderer.bounds.size;
            }
        }
        [SerializeField] private Vector2 _defaultSize;
        [SerializeField] private float _extraSize = 3.0f;
        /// <summary>
        /// Определение следующей позиции Доски
        /// </summary>
        public Vector3 NextPosition => Position + _delta * Time.deltaTime;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _size = _spriteRenderer.bounds.size;
            _defaultSize = _spriteRenderer.size;
        }

        protected override void Update()
        {
            base.Update();
            _delta = new Vector3(Input.GetAxis(_horizontal) * _velocity.x, 0.0f, 0.0f);
            if (Input.GetButtonUp(_jump))
            {
                ReleaseBall();
            }

            if (_delta.magnitude > Mathf.Epsilon)
            {
                var direction = _delta * Time.deltaTime;
                Move(direction);
            }
        }

        private void ReleaseBall()
        {
            if (Ball != null)
            {
                Ball.Jump();
                Ball = null;
            }
        }

        private void Move(Vector3 direction)
        {
            Position += direction;

            if (Ball != null)
            {
                Ball.Position += direction;
            }
        }
        /// <summary>
        /// Остановка движения Доски при столкновение с границей
        /// </summary>
        /// <param name="border">Граница</param>
        public void Stop(Border border)
        {
            var nextPos = NextPosition;
            if ((nextPos.x < border.Position.x + border.Size.x) && (nextPos.x + Size.x > border.Position.x + border.Size.x))
            {
                Position = new Vector3(border.Position.x + border.Size.x, Position.y, Position.z);
            }

            if ((nextPos.x + Size.x > border.Position.x) && (nextPos.x < border.Position.x))
            {
                Position = new Vector3(border.Position.x - Size.x, Position.y, Position.z);
            }

            _delta = Vector2.zero;
            if (Ball != null)
            {
                Ball.Delta = Vector3.zero;
            }
        }
    }
}
