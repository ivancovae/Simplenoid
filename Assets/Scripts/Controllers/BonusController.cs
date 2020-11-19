using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;
using Simplenoid.Helpers;

namespace Simplenoid.Controllers
{
    /// <summary>
    /// Контролер управления бонусами
    /// </summary>
    public class BonusController : BaseController
    {
#pragma warning disable 0649
        [SerializeField] private BonusesVariable _bonuses;
        [SerializeField] private UnusedBonusesVariable _unusedBonuses;
        [SerializeField] private BordersVariable _borders;
#pragma warning restore 0649

#pragma warning disable 0649
        private BoolVariable _isFaster;
        private BoolVariable _isSlowly;
        private BoolVariable _isLongBoard;
        private FloatVariable _activeTime;
#pragma warning restore 0649

        private ManagerBonuses _managerBonuses;
        private ManagerBalls _managerBalls;
        
        private Coroutine _fasterRoutine;
        private Coroutine _slowlyRoutine;
        private Coroutine _longBoardRoutine;

        protected override void Update()
        {
            base.Update();

            for (var i = 0; i < _bonuses.Items.Count; i++)
            {
                var bonus = _bonuses.Items[i];
                Move(bonus);
            }

            for (var i = 0; i < _unusedBonuses.Items.Count; i++)
            {
                var bonus = _unusedBonuses.Items[i];
                ActiveBonus(bonus);
            }
        }

        public void InitController(ManagerBonuses managerBonuses, ManagerBalls managerBalls, IBonusControllerData data)
        {
            _bonuses = data.GetBonuses;
            _bonuses.Clear();
            _unusedBonuses = data.GetUnusedBonuses;
            _unusedBonuses.Clear();

            _borders = data.GetBorders;
            _managerBonuses = managerBonuses;
            _managerBalls = managerBalls;

            _isFaster = data.GetIsFaster;
            _isSlowly = data.GetIsSlowly;
            _isLongBoard = data.GetIsLongBoard;
            _activeTime = data.GetActiveTime;
        }
        public void Move(Bonus bonus)
        {
            if (bonus.Delta.magnitude > Mathf.Epsilon)
            {
                var nextPosition = GetNextPosition(bonus);
                bool isBreakMoving = CheckBorders(bonus, nextPosition);
                if (isBreakMoving)
                {
                    _managerBonuses.RemoveBonus(bonus);
                    return;
                }
                if (!isBreakMoving)
                {
                    bonus.Position = nextPosition;
                }
            }
        }
        private Vector3 GetNextPosition(Bonus bonus)
        {
            var direction = bonus.Delta * Time.deltaTime;
            return bonus.Position + direction;
        }
        private bool Collide(Bonus bonus, Vector3 newPos, ICollidable collideObject)
        {
            if (collideObject != null)
            {
                if (newPos.x + bonus.Size.x > collideObject.Position.x &&
                    newPos.x < collideObject.Position.x + collideObject.Size.x &&
                    newPos.y + bonus.Size.y > collideObject.Position.y &&
                    newPos.y < collideObject.Position.y + collideObject.Size.y)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckBorders(Bonus bonus, Vector3 newPos)
        {
            foreach (var border in _borders.Items)
            {
                if (Collide(bonus, newPos, border))
                {
                    return true;
                }
            }
            return false;
        }

        IEnumerator EnableBonus(BoolVariable variable)
        {
            variable.Value = true;
            yield return new WaitForSeconds(_activeTime.Value);
            variable.Value = false;
        }

        private void ActiveBonus(Bonus bonus)
        {
            if (!bonus.IsUsed)
            {
                bonus.IsUsed = true;
                _managerBonuses.RemoveBonus(bonus);
                switch (bonus.Type)
                {
                    case TypesBonuses.DoubleBalls:
                        {
                            _managerBalls.InstantiateBall();
                        }
                        break;
                    case TypesBonuses.FasterBalls:
                        {
                            if (_fasterRoutine != null)
                            {
                                StopCoroutine(_fasterRoutine);
                                _fasterRoutine = null;
                            }
                            var routine = EnableBonus(_isFaster);
                            _fasterRoutine = StartCoroutine(routine);
                        }
                        break;
                    case TypesBonuses.LongBoard:
                        {
                            if (_longBoardRoutine != null)
                            {
                                StopCoroutine(_longBoardRoutine);
                                _longBoardRoutine = null;
                            }
                            var routine = EnableBonus(_isLongBoard);
                            _longBoardRoutine = StartCoroutine(routine);
                        }
                        break;
                    case TypesBonuses.SlowerBalls:
                        {
                            if (_slowlyRoutine != null)
                            {
                                StopCoroutine(_slowlyRoutine);
                                _slowlyRoutine = null;
                            }
                            var routine = EnableBonus(_isSlowly);
                            _slowlyRoutine = StartCoroutine(routine);
                        }
                        break;
                    default:
                        {
                            Debug.LogError("Unknown type bonus");
                        }
                        break;
                }
            }
        }
    }
}