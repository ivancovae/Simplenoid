using Simplenoid.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Simplenoid.Interface;

namespace Simplenoid
{
    public class ManagerBonuses
    {
        public float ActiveTime { get; private set; } = 3.0f;
        private BoardVariable _board;
        private BonusesVariable _bonuses;

        [SerializeField] private BoolVariable _isFaster;
        [SerializeField] private BoolVariable _isSlowly;
        [SerializeField] private BoolVariable _isLongBoard;

        private ManagerBalls _managerBalls;

        public ManagerBonuses(ManagerBalls manager, IBonusData bonusData)
        {
            _bonuses = bonusData.GetBonuses;
            _board = bonusData.GetBoard;
            _managerBalls = manager;

        }

        public Vector3 GetDefaultPosition(Block block, Bonus bonus)
        {
            return new Vector3(block.Position.x + (block.Size.x / 2) - (bonus.Size.x / 2), block.Position.y + (block.Size.y / 2) - (bonus.Size.y / 2), 0.0f);
        }

        public void InstantiateBonus(Block block)
        {
            if (_bonuses.PrefabsBonuses.Any(b => b.Type == block.TypeBonus))
            {
                var prefabBonus = _bonuses.PrefabsBonuses.Where(b => b.Type == block.TypeBonus).First();
                var bonus = GameObject.Instantiate(prefabBonus, Vector3.zero, Quaternion.identity);
                bonus.Position = GetDefaultPosition(block, bonus);
                bonus.Delta = new Vector3(0.0f, -3.0f, 0.0f);
                _bonuses.Add(bonus);
            }
        }

        public void RemoveBonus(Bonus bonus)
        {
            bonus.InstanceObject.SetActive(false);
            _bonuses.Remove(bonus);
            GameObject.Destroy(bonus);
        }

        public void ClearBonuses()
        {
            foreach (var bonus in _bonuses.Items)
            {
                bonus.InstanceObject.SetActive(false);
                GameObject.Destroy(bonus);
            }
            _bonuses.Clear();
        }

        #region Bonuses Action
        private void DeactiveSlow()
        {
            _isSlowly.Value = false;
        }

        private void ActiveSlow()
        {
            _isSlowly.Value = true;
            _board.ObjectOnScene.Invoke("DeactiveSlow", ActiveTime);
        }

        private void DeactiveFast()
        {
            _isFaster.Value = false;
        }

        private void ActiveFast()
        {
            _isFaster.Value = true;
            _board.ObjectOnScene.Invoke("DeactiveFast", ActiveTime);
        }

        private void DeactiveLong()
        {
            _isLongBoard.Value = false;
        }

        private void ActiveLong()
        {
            _isLongBoard.Value = true;
            _board.ObjectOnScene.Invoke("DeactiveLong", ActiveTime);
        }

        private void ActiveDoubleBall()
        {
            _managerBalls.InstantiateBall();
        }
        #endregion

        public void ActiveBonus(Bonus bonus)
        {
            if (!bonus.IsUsed)
            {
                bonus.IsUsed = true;
                switch (bonus.Type)
                {
                    case TypesBonuses.DoubleBalls:
                        {
                            ActiveDoubleBall();
                        }
                        break;
                    case TypesBonuses.FasterBalls:
                        {
                            ActiveFast();
                        }
                        break;
                    case TypesBonuses.LongBoard:
                        {
                            ActiveLong();
                        }
                        break;
                    case TypesBonuses.SlowerBalls:
                        {
                            ActiveSlow();
                        }
                        break;
                    default:
                        {
                            Debug.LogError("Unknown type bonus");
                        }
                        break;
                }
                bonus.InstanceObject.SetActive(false);
            }
        }
    }
}