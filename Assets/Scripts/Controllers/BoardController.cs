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

        [SerializeField] private BoardVariable _board;
        [SerializeField] private BordersVariable _borders;
        [SerializeField] private BonusesVariable _bonuses;

        private ManagerBalls _managerBalls;
        private ManagerBonuses _managerBonuses;

        protected override void Update()
        {
            base.Update();

            if (Input.GetButtonUp(_jump))
            {
                ReleaseBall();
            }

            _board.ObjectOnScene.Delta = new Vector3(Input.GetAxis(_horizontal) * _board.ObjectOnScene.Velocity.x, 0.0f, 0.0f);

            Move();
        }

        private Vector3 GetNextPosition()
        {
            var direction = _board.ObjectOnScene.Delta;
            return _board.ObjectOnScene.Position + direction;
        }
        
        private void Move()
        {
            if (_board.ObjectOnScene.Delta.magnitude > Mathf.Epsilon)
            {
                var nextPosition = GetNextPosition();
                bool isBreakMoving = CheckBorders(_board.ObjectOnScene, nextPosition);
                if (isBreakMoving)
                {
                    return;
                }
                isBreakMoving = CheckBonus(_board.ObjectOnScene, nextPosition);
                if (!isBreakMoving)
                {
                    _board.ObjectOnScene.Position = nextPosition;

                    if (_board.ObjectOnScene.Ball != null)
                    {
                        _board.ObjectOnScene.Ball.Position += _board.ObjectOnScene.Delta;
                    }
                }
            }
        }

        private void ReleaseBall()
        {
            if (_board.ObjectOnScene.Ball != null)
            {
                _managerBalls.Jump(_board.ObjectOnScene.Ball);
                _board.ObjectOnScene.Ball = null;
            }
        }

        public void Stop(Border border)
        {
            var nextPos = GetNextPosition();
            if ((nextPos.x < border.Position.x + border.Size.x) && (nextPos.x + _board.ObjectOnScene.Size.x > border.Position.x + border.Size.x))
            {
                _board.ObjectOnScene.Position = new Vector3(border.Position.x + border.Size.x, _board.ObjectOnScene.Position.y, _board.ObjectOnScene.Position.z);
            }

            if ((nextPos.x + _board.ObjectOnScene.Size.x > border.Position.x) && (nextPos.x < border.Position.x))
            {
                _board.ObjectOnScene.Position = new Vector3(border.Position.x - _board.ObjectOnScene.Size.x, _board.ObjectOnScene.Position.y, _board.ObjectOnScene.Position.z);
            }

            _board.ObjectOnScene.Delta = Vector2.zero;
            if (_board.ObjectOnScene.Ball != null)
            {
                _board.ObjectOnScene.Ball.Delta = Vector3.zero;
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
            foreach (var border in _borders.Items)
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
            foreach (var bonus in _bonuses.Items)
            {
                if (Collide(board, newPos, bonus))
                {
                    _managerBonuses.ActiveBonus(bonus);
                    return false;
                }
            }
            return false;
        }

        public void InitController(ManagerBalls managerBalls, ManagerBonuses managerBonuses, IBoardData data)
        {
            _board = data.GetBoard;
            _bonuses = data.GetBonuses;
            _borders = data.GetBorders;
            _managerBalls = managerBalls;
            _managerBonuses = managerBonuses;
        }
    }
}
