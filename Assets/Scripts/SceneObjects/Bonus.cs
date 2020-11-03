using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Helpers;

namespace Simplenoid
{
    /// <summary>
    /// Компонент Бонуса
    /// </summary>
    public class Bonus : BaseObjectScene, ICollidable
    {
        [SerializeField] private float _speed = 3.0f;

        [SerializeField] private TypesBonuses _type = TypesBonuses.None;
        /// <summary>
        /// Тип бонуса
        /// </summary>
        public TypesBonuses Type => _type;

        [SerializeField] private Vector2 _size;
        /// <summary>
        /// Размер объекта Бонус
        /// </summary>
        public Vector2 Size => _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Vector3 _delta = new Vector3(0.0f, -1.0f, 0.0f);
        /// <summary>
        /// Следующая позиция бонуса
        /// </summary>
        public Vector3 NextPosition => Position + _delta * _speed * Time.deltaTime;
        /// <summary>
        /// Состояние применения бонуса
        /// </summary>
        public bool IsUsed { get; private set; } = false;
        /// <summary>
        /// Использование бонуса
        /// </summary>
        public void UseBonus()
        {
            IsUsed = true;
        }

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _size = _spriteRenderer.bounds.size;
        }
        /// <summary>
        /// Перемещение бонуса
        /// </summary>
        /// <param name="deltaTime">Параметр сглаживания относительно времени между кадрами</param>
        public void Move(float deltaTime)
        {
            var direction = _delta * _speed * deltaTime;
            Position += direction;
        }
    }
}