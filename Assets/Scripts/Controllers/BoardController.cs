using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;

namespace Simplenoid.Controllers
{
    public class BoardController : BaseController
    {
        private string _horizontal = "Horizontal";
        private string _jump = "Jump";

        [SerializeField] private BoardReference _board;
        [SerializeField] private BordersReference _borders;
        [SerializeField] private BonusesReference _bonuses;

        private ManagerBalls _managerBalls;
        private ManagerBonuses _managerBonuses;

        protected override void Update()
        {
            base.Update();

            if (Input.GetButtonUp(_jump))
            {
                ReleaseBall();
            }

            _board.Value.Delta = new Vector3(Input.GetAxis(_horizontal) * _board.Value.Velocity.x, 0.0f, 0.0f);

            Move();
        }

        private Vector3 GetNextPosition()
        {
            var direction = _board.Value.Delta;
            return _board.Value.Position + direction;
        }
        
        private void Move()
        {
            if (_board.Value.Delta.magnitude > Mathf.Epsilon)
            {
                var nextPosition = GetNextPosition();
                bool isBreakMoving = CheckBorders(_board.Value, nextPosition);
                if (isBreakMoving)
                {
                    return;
                }
                isBreakMoving = CheckBonus(_board.Value, nextPosition);
                if (!isBreakMoving)
                {
                    _board.Value.Position = nextPosition;

                    if (_board.Value.Ball != null)
                    {
                        _board.Value.Ball.Position += _board.Value.Delta;
                    }
                }
            }
        }

        private void ReleaseBall()
        {
            if (_board.Value.Ball != null)
            {
                _managerBalls.Jump(_board.Value.Ball);
                _board.Value.Ball = null;
            }
        }

        public void Stop(Border border)
        {
            var nextPos = GetNextPosition();
            if ((nextPos.x < border.Position.x + border.Size.x) && (nextPos.x + _board.Value.Size.x > border.Position.x + border.Size.x))
            {
                _board.Value.Position = new Vector3(border.Position.x + border.Size.x, _board.Value.Position.y, _board.Value.Position.z);
            }

            if ((nextPos.x + _board.Value.Size.x > border.Position.x) && (nextPos.x < border.Position.x))
            {
                _board.Value.Position = new Vector3(border.Position.x - _board.Value.Size.x, _board.Value.Position.y, _board.Value.Position.z);
            }

            _board.Value.Delta = Vector2.zero;
            if (_board.Value.Ball != null)
            {
                _board.Value.Ball.Delta = Vector3.zero;
            }
        }

        private bool Collide(Board board, Vector3 newPos, ICollidable collideObject)
        {
            if (collideObject != null)
            {
                if (newPos.x + board.Size.x > collideObject.Position.x &&
                    newPos.x < collideObject.Position.x + collideObject.Size.x &&
                    newPos.y + board.Size.y > collideObject.Position.y &&
                    newPos.y < collideObject.Position.y + collideObject.Size.y)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckBorders(Board board, Vector3 newPos)
        {
            foreach (var border in _borders.Value)
            {
                if (Collide(board, newPos, border))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckBonus(Board board, Vector3 newPos)
        {
            foreach (var bonus in _bonuses.Value)
            {
                if (Collide(board, newPos, bonus))
                {
                    _managerBonuses.ActiveBonus(bonus);
                    return false;
                }
            }
            return false;
        }
    }
}
