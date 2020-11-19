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

        public Vector3 PointLT => new Vector3(Position.x, Position.y + Size.y, Position.z);

        public Vector3 PointRT => new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z);

        public Vector3 PointRB => new Vector3(Position.x + Size.x, Position.y, Position.z);

        public Vector3 PointLB => Position;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _size = _spriteRenderer.bounds.size;
        }
        protected override void Update()
        {
            base.Update();

            Debug.DrawLine(PointLB, PointLT, Color.blue);
            Debug.DrawLine(PointLT, PointRT, Color.blue);
            Debug.DrawLine(PointRT, PointRB, Color.blue);
            Debug.DrawLine(PointRB, PointLB, Color.blue);
        }
    }
}