using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;

namespace Simplenoid.Controllers
{
    public class BonusController : BaseController
    {
        [SerializeField] private BonusesVariable _bonuses;
        [SerializeField] private BordersReference _borders;

        private ManagerBonuses _managerBonuses;

        protected override void Update()
        {
            base.Update();

            foreach (var bonus in _bonuses.Items)
            {
                Move(bonus);
            }
        }

        public void InitController(ManagerBonuses managerBonuses, BonusesVariable bonuses)
        {
            _bonuses = bonuses;
            _managerBonuses = managerBonuses;
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
            var direction = bonus.Delta;
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
            foreach (var border in _borders.Value)
            {
                if (Collide(bonus, newPos, border))
                {
                    return true;
                }
            }
            return false;
        }
    }
}