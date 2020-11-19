using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Helpers;

namespace Simplenoid
{
    public class Bonus : BaseObjectScene, ICollidable
    {
        [SerializeField] private float _speed = 3.0f;

        [SerializeField] private TypesBonuses _type = TypesBonuses.None;
        public TypesBonuses Type => _type;

        [SerializeField] private Vector2 _size;

        public Vector2 Size => _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Vector3 _delta = new Vector3(0.0f, -1.0f, 0.0f);

        public Vector3 Delta
        {
            set
            {
                _delta = value;
            }
            get
            {
                return _delta * _speed;
            }
        }
        public bool IsUsed { get; set; } = false;

        public Vector3 PointLT => new Vector3(Position.x, Position.y + Size.y, Position.z);

        public Vector3 PointRT => new Vector3(Position.x + Size.x, Position.y + Size.y, Position.z);

        public Vector3 PointRB => new Vector3(Position.x + Size.x, Position.y, Position.z);

        public Vector3 PointLB => Position;

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _size = _spriteRenderer.bounds.size;
        }

        protected override void Update()
        {
            base.Update();

            Debug.DrawLine(PointLB, PointLT, Color.green);
            Debug.DrawLine(PointLT, PointRT, Color.green);
            Debug.DrawLine(PointRT, PointRB, Color.green);
            Debug.DrawLine(PointRB, PointLB, Color.green);
        }
    }
}