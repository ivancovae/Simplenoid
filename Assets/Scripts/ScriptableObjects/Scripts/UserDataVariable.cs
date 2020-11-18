using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simplenoid.Interface;

namespace Simplenoid
{
    [CreateAssetMenu(fileName = "UserDataVariable", menuName = "Variable/User Data Variable")]
    public class UserDataVariable : ScriptableObject, IBonusData, IBallsData, IBoardData, IBallsControllerData, ILevelsData, IBonusControllerData
    {
        public BallsVariable Balls;
        public BallsVariable GetBalls => Balls;

        public BoardVariable Board;
        public BoardVariable GetBoard => Board;

        public BlocksVariable Blocks;
        public BlocksVariable GetBlocks => Blocks;

        public BordersVariable Borders;
        public BordersVariable GetBorders => Borders;

        public LevelsVariable Levels;
        public LevelsVariable GetLevels => Levels;

        public BonusesVariable Bonuses;
        public BonusesVariable GetBonuses => Bonuses;
        


        // bonuses 
        public BoolVariable IsFaster;
        public BoolVariable GetIsFaster => IsFaster;
        public BoolVariable IsSlowly;
        public BoolVariable GetIsSlowly => IsSlowly;
        public BoolVariable IsLongBoard;
        public BoolVariable GetIsLongBoard => IsLongBoard;
        public FloatVariable Divider;
        public FloatVariable GetDivider => Divider;

        [Header("Bonus time")]
        public FloatVariable ActiveTime;
    }
}