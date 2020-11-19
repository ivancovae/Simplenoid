using Simplenoid.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Simplenoid.Interface;

namespace Simplenoid
{
    /// <summary>
    /// Менеджер бонусов
    /// </summary>
    public class ManagerBonuses
    {
        public float ActiveTime { get; private set; } = 3.0f;
        private BonusesVariable _bonuses;
        private UnusedBonusesVariable _unusedBonuses;

        public ManagerBonuses(IBonusData bonusData)
        {
            _bonuses = bonusData.GetBonuses;
            _unusedBonuses = bonusData.GetUnusedBonuses;
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
                bonus.Delta = new Vector3(0.0f, -1.0f, 0.0f);
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

        public void ActiveBonus(Bonus bonus)
        {
            bonus.Delta = Vector3.zero;
            bonus.InstanceObject.SetActive(false);
            _unusedBonuses.Add(bonus);
        }
    }
}