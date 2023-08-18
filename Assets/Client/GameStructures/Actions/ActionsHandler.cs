using SpaceTraveler.GameStructures.Stats;
using SpaceTraveler.GameStructures.Stats.Chances;
using System;
using System.Collections;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Actions
{
    public static class ActionsHandler
    {

        public static bool TryToEvadeHit(ActionChance chance , ActionLuck isLuck = ActionLuck.None)
        {
            var randomValue1 = UnityEngine.Random.Range(0, 100.1f);
            var randomValue2 = UnityEngine.Random.Range(0, 100.1f);
            
            float maxValue = MathF.Max(randomValue1, randomValue2);
            float minValue = MathF.Min(randomValue1, randomValue2);


            float chanceValue;
            
            
            if (chance != null)
            {
                chanceValue = chance.Value;
                switch (isLuck)
                {
                    case ActionLuck.None:
                        {
                            if (chanceValue >= randomValue1)
                                return true;

                            break;
                        }
                    case ActionLuck.Luck:
                        {

                            if (chanceValue >= minValue)
                                return true;

                            break;
                        }
                    case ActionLuck.Failure:
                        {
                            if (chanceValue >= maxValue)
                                return true;

                            break;

                        }
                }
            }

            return false;
        }

    }

    public enum ActionLuck
    {
        None,
        Luck,
        Failure
    }
}