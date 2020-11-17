using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simplenoid
{
    [CreateAssetMenu(fileName = "UserDataVariable", menuName = "Variable/User Data Variable")]
    public class UserDataVariable : ScriptableObject
    {
        public BallsVariable Balls;
        public BoardVariable Board;
        public BlocksVariable Blocks;
        public BordersVariable Borders;
        public LevelsVariable Levels;
        public BonusesVariable Bonuses;

        // bonuses 
        public BoolVariable IsFaster;
        public BoolVariable IsSlowly;
        public BoolVariable IsLongBoard;
        public FloatVariable Divider;
        
        [Header("Bonus time")]
        public FloatVariable ActiveTime;
    }
}