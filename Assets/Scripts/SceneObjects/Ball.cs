using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Controllers;

namespace Simplenoid
{
    /// <summary>
    /// Компонент Мячика
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : BaseObjectScene, ICollidable
    {
        [SerializeField] private Vector3 _velocity = new Vector3(6.0f, 6.0f, 0.0f);
        [SerializeField] private Vector3 _delta = Vector3.zero;

        [SerializeField] private Vector2 _size;
        [SerializeField] private int _damage = 1;
        /// <summary>
        /// Размер Мячика
        /// </summary>
        public Vector2 Size => _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        /// <summary>
        /// Дельта перемещения (dx, dy, dz)
        /// </summary>
        public Vector3 Delta
        {
            get => _delta;
            set
            {
                _delta = value;
            }
        }
        /// <summary>
        /// Определение следующей позиции
        /// </summary>
        public Vector3 NextPosition => Position + Delta * Time.deltaTime;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _size = _spriteRenderer.bounds.size;
        }
        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="deltaTime">Параметр сглаживания относительно времени между кадрами</param>
        public void Move(float deltaTime)
        {
            var ballsController = Toolbox.Instance.Get<BallsController>();
            if (_delta.magnitude > Mathf.Epsilon)
            {
                var direction = _delta * deltaTime;
                if (ballsController.IsFaster)
                {
                    direction *= ballsController.Divider;
                }
                if (ballsController.IsSlowly)
                {
                    direction /= ballsController.Divider;
                }
                Position += direction;
            }
        }
        /// <summary>
        /// Начало движения(стартовый прыжок)
        /// </summary>
        public void Jump()
        {
            Delta = new Vector3(-_velocity.x, _velocity.y, 0.0f);
        }
        /// <summary>
        /// Прыжок от блока
        /// </summary>
        /// <param name="block">Блок</param>
        public void BumpBlock(Block block)
        {
            Delta = new Vector3(Delta.x, -Delta.y, 0.0f);
            var damageableblock = block.GetComponent<ISetDamage>();
            damageableblock?.SetDamage(_damage);
        }
        /// <summary>
        /// Прыжок от Доски
        /// </summary>
        /// <param name="board">Доска</param>
        public void BumpBoard(Board board)
        {
            if (OnCenter(board))
            {
                Delta = new Vector3(0.0f, _velocity.y, 0.0f);
                return;
            }
            Delta = new Vector3(OnTheLeftSide(board) ? -_velocity.x : _velocity.x, _velocity.y, 0.0f);
        }
        /// <summary>
        /// Определение попадания в центр доски
        /// </summary>
        /// <param name="board">Доска</param>
        /// <returns>Результат попадания в центр доски</returns>
        private bool OnCenter(Board board)
        {
            var centerBall = Position.x + (Size.x / 2);
            if ((centerBall < (board.Position.x + board.Size.x - board.Size.x / 4)) && (centerBall > (board.Position.x + board.Size.x/4)))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Определение попадания в левую сторону доской
        /// </summary>
        /// <param name="board">Доска</param>
        /// <returns>Результат попадания в левую часть доски доски</returns>
        private bool OnTheLeftSide(Board board)
        {
            var centerBall = Position.x + (Size.x / 2);
            var centerBoard = board.Position.x + (board.Size.x / 2);

            return centerBall < centerBoard;
        }
        /// <summary>
        /// Отскок от границ уровня
        /// </summary>
        /// <param name="border">Граница</param>
        public void BumpBorder(Border border)
        {
            var newPos = NextPosition;

            // horizontal collision
            if ((newPos.y > border.Position.y) && (newPos.y + Size.y < border.Position.y + border.Size.y))
            {
                // from right
                if ((newPos.x < border.Position.x + border.Size.x) && (newPos.x + Size.x > border.Position.x + border.Size.x))
                {
                    Delta = new Vector3(_velocity.x, Delta.y, 0.0f);
                    return;
                }
                // from left
                if ((newPos.x + Size.x > border.Position.x) && (newPos.x < border.Position.x))
                {
                    Delta = new Vector3(-_velocity.x, Delta.y, 0.0f);
                    return;
                }
            }
            // vertical collision
            if ((newPos.x > border.Position.x) && (newPos.x + Size.x < border.Position.x + border.Size.x))
            {
                // from bottom
                if ((newPos.y + Size.y > border.Position.y) && (newPos.y < border.Position.y))
                {
                    Delta = new Vector3(Delta.x, -_velocity.y, 0.0f);
                    return;
                }
                // from top
                if ((newPos.y < border.Position.y + border.Size.y) && (newPos.y + Size.y > border.Position.y + border.Size.y))
                {
                    Delta = Vector3.zero;
                    var ballsController = Toolbox.Instance.Get<BallsController>();
                    ballsController.RemoveBall(this);
                    return;
                }
            }
        }
    }
}