using UnityEngine;
using System.Collections;

namespace CantFindItGrindIt
{
    public class Gear 
    {
        public int NumberInSequence;
        public float SpeedMin;
        public float SpeedMax;

        public Gear(int numberInSequence, float speedMin, float speedMax)
        {
            NumberInSequence = numberInSequence;
            SpeedMin = speedMin;
            SpeedMax = speedMax;
        }
    }
}