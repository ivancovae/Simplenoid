﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Контроллер управления доской
    /// </summary>
    public class BoardController : BaseController
    {
        private string _horizontal = "Horizontal";
        private string _jump = "Jump";

        [SerializeField] private BoardVariable _board;
        [SerializeField] private BordersVariable _borders;
        [SerializeField] private BonusesVariable _bonuses;

        [SerializeField] private BoolVariable _isLongBoard;

        private ManagerBalls _managerBalls;
        private ManagerBonuses _managerBonuses;

        protected override void Update()
        {
            base.Update();

            _board.ObjectOnScene.ChangeSize(_isLongBoard.Value);

            if (Input.GetButtonUp(_jump))
            {
                ReleaseBall();
            }

            _board.ObjectOnScene.Delta = new Vector3(Input.GetAxis(_horizontal) * _board.ObjectOnScene.Velocity.x, 0.0f, 0.0f);

            Move();
        }

        private Vector3 GetNextPosition()
        {
            var direction = _board.ObjectOnScene.Delta * Time.deltaTime;
            return _board.ObjectOnScene.Position + direction;
        }

        private void Move()
        {
            var nextPosition = GetNextPosition();
            CheckBonus(_board.ObjectOnScene, nextPosition);
            bool isBreakMoving = CheckBorders(_board.ObjectOnScene, nextPosition);
            if (!isBreakMoving)
            {
                _board.ObjectOnScene.Position = nextPosition;

                if (_board.ObjectOnScene.Ball != null)
                {
                    _board.ObjectOnScene.Ball.Position += _board.ObjectOnScene.Delta * Time.deltaTime;
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
            if ((nextPos.x < border.PointRB.x) && (nextPos.x + _board.ObjectOnScene.Size.x > border.PointRB.x))
            {
                _board.ObjectOnScene.Position = new Vector3(border.PointRB.x, _board.ObjectOnScene.Position.y, _board.ObjectOnScene.Position.z);
            }

            if ((nextPos.x + _board.ObjectOnScene.Size.x > border.PointLB.x) && (nextPos.x < border.PointLB.x))
            {
                _board.ObjectOnScene.Position = new Vector3(border.PointLB.x - _board.ObjectOnScene.Size.x, _board.ObjectOnScene.Position.y, _board.ObjectOnScene.Position.z);
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
                if (newPos.x + board.Size.x > collideObject.PointLB.x &&
                    newPos.x < collideObject.PointRT.x &&
                    newPos.y + board.Size.y > collideObject.PointLB.y &&
                    newPos.y < collideObject.PointRT.y)
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
            for (var i = 0; i < _bonuses.Items.Count; i++)
            {
                var bonus = _bonuses.Items[i];
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
            _isLongBoard = data.GetIsLongBoard;
        }
    }
}
