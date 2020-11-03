using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Simplenoid.Interface;

namespace Simplenoid
{
    /// <summary>
    /// Компонент границы
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Border : BaseObjectScene, ICollidable
    {
        [SerializeField] private Vector2 _size;
        /// <summary>
        /// Размер границы
        /// </summary>
        public Vector2 Size => _size;

        /// <summary>
        /// Следующая позиция для статических объектов = текущей позиции
        /// </summary>
        public Vector3 NextPosition => Position;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _size = _spriteRenderer.bounds.size;
        }
    }
}