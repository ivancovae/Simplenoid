using Simplenoid.Helpers;
using Simplenoid.Interface;
using UnityEngine;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Контроллер управления шарами
    /// </summary>
    public class BallsController : BaseController
    {
        [SerializeField] private BallsVariable _balls;
        [SerializeField] private BoardVariable _board;
        [SerializeField] private BlocksVariable _blocks;
        [SerializeField] private BordersVariable _borders;

        [SerializeField] private BoolVariable _isFaster;
        [SerializeField] private BoolVariable _isSlowly;
        [SerializeField] private FloatVariable _divider;

        private ManagerBalls _managerBalls;
        private ManagerBonuses _managerBonuses;

        protected override void Update()
        {
            base.Update();

            for (var i = 0; i < _balls.Items.Count; i++)
            {
                var ball = _balls.Items[i];
                Move(ball);
            }
        }

        public void InitController(ManagerBalls managerBalls, ManagerBonuses managerBonuses, IBallsControllerData data)
        {
            _managerBalls = managerBalls;
            _managerBonuses = managerBonuses;

            _balls = data.GetBalls;
            _board = data.GetBoard;
            _blocks = data.GetBlocks;
            _borders = data.GetBorders;
            _isFaster = data.GetIsFaster;
            _isSlowly = data.GetIsSlowly;
            _divider = data.GetDivider;
        }

        private Vector3 GetNextPosition(Ball ball)
        {
            var direction = ball.Delta * Time.deltaTime;
            if (_isFaster.Value)
            {
                direction *= _divider.Value;
            }
            if (_isSlowly.Value)
            {
                direction /= _divider.Value;
            }
            return ball.Position + direction;
        }

        private void Move(Ball ball)
        {
            if (ball.Delta.magnitude > Mathf.Epsilon)
            {
                var nextPosition = GetNextPosition(ball);
                bool isBreakMoving = CheckBorders(ball, nextPosition);
                if (isBreakMoving)
                {
                    return;
                }
                isBreakMoving = CheckBoard(ball, nextPosition);
                if (isBreakMoving)
                {
                    return;
                }
                isBreakMoving = CheckBlocks(ball, nextPosition);
                if (!isBreakMoving)
                {
                    ball.Position = nextPosition;
                }
            }
        }
                
        private bool OnCenter(Ball ball)
        {
            var centerBall = ball.Position.x + (ball.Size.x / 2);
            if ((centerBall < (_board.ObjectOnScene.Position.x + _board.ObjectOnScene.Size.x - _board.ObjectOnScene.Size.x / 4)) && (centerBall > (_board.ObjectOnScene.Position.x + _board.ObjectOnScene.Size.x / 4)))
            {
                return true;
            }
            return false;
        }

        private bool OnTheLeftSide(Ball ball)
        {
            var centerBall = ball.Position.x + (ball.Size.x / 2);
            var centerBoard = _board.ObjectOnScene.Position.x + (_board.ObjectOnScene.Size.x / 2);

            return centerBall < centerBoard;
        }

        private bool Collide(Ball ball, Vector3 newPos, ICollidable collideObject)
        {
            if (collideObject != null)
            {
                if (newPos.x + ball.Size.x > collideObject.Position.x &&
                    newPos.x < collideObject.Position.x + collideObject.Size.x &&
                    newPos.y + ball.Size.y > collideObject.Position.y &&
                    newPos.y < collideObject.Position.y + collideObject.Size.y)
                {
                    return true;
                }
            }
            return false;
        }

        private void BumpBorder(Ball ball, Vector3 newPos, Border border)
        {
            var ballVelocity = ball.Velocity;
            var ballDelta = ball.Delta;
            // horizontal collision
            if ((newPos.y > border.Position.y) && (newPos.y + ball.Size.y < border.Position.y + border.Size.y))
            {
                // from right
                if ((newPos.x < border.Position.x + border.Size.x) && (newPos.x + ball.Size.x > border.Position.x + border.Size.x))
                {
                    ball.Delta = new Vector3(ballVelocity.x, ballDelta.y, 0.0f);
                    return;
                }
                // from left
                if ((newPos.x + ball.Size.x > border.Position.x) && (newPos.x < border.Position.x))
                {
                    ball.Delta = new Vector3(-ballVelocity.x, ballDelta.y, 0.0f);
                    return;
                }
            }
            // vertical collision
            if ((newPos.x > border.Position.x) && (newPos.x + ball.Size.x < border.Position.x + border.Size.x))
            {
                // from bottom
                if ((newPos.y + ball.Size.y > border.Position.y) && (newPos.y < border.Position.y))
                {
                    ball.Delta = new Vector3(ballDelta.x, -ballVelocity.y, 0.0f);
                    return;
                }
                // from top
                if ((newPos.y < border.Position.y + border.Size.y) && (newPos.y + ball.Size.y > border.Position.y + border.Size.y))
                {
                    ball.Delta = Vector3.zero;
                    _managerBalls.RemoveBall(ball);
                    return;
                }
            }
        }

        private bool CheckBorders(Ball ball, Vector3 newPos)
        {
            foreach (var border in _borders.Items)
            {
                if (Collide(ball, newPos, border))
                {
                    BumpBorder(ball, newPos, border);
                    return true;
                }
            }
            return false;
        }


        private void BumpBoard(Ball ball)
        {
            if (OnCenter(ball))
            {
                ball.Delta = new Vector3(0.0f, ball.Velocity.y, 0.0f);
                return;
            }
            ball.Delta = new Vector3(OnTheLeftSide(ball) ? -ball.Velocity.x : ball.Velocity.x, ball.Velocity.y, 0.0f);
        }


        private bool CheckBoard(Ball ball, Vector3 newPos)
        {
            if (Collide(ball, newPos, _board.ObjectOnScene))
            {
                BumpBoard(ball);
                return true;
            }
            return false;
        }

        private void BumpBlock(Ball ball, Block block)
        {
            ball.Delta = new Vector3(ball.Delta.x, -ball.Delta.y, 0.0f);

            var damageableblock = block.GetComponent<ISetDamage>();
            damageableblock?.SetDamage(ball.Damage);

            if (block.Type == TypesBlocks.WithBonus)
            {
                _managerBonuses.InstantiateBonus(block);
            }
            Toolbox.Instance.Get<LevelController>().CheckLevel();
        }

        private bool CheckBlocks(Ball ball, Vector3 newPos)
        {
            foreach (var block in _blocks.Items)
            {
                if (!block.isAlive)
                {
                    continue;
                }

                if (Collide(ball, newPos, block))
                {
                    BumpBlock(ball, block);
                    return true;
                }
            }
            return false;
        }
    }
}