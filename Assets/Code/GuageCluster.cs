using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;
using CantFindItGrindIt.Managers;

namespace CantFindItGrindIt
{
    public class GuageCluster
    {
        public float CurrentRPM
        {
            get
            {
                return tachometer.value;
            }

            private set
            {
                tachometer.value = value;
            }
        }

        public float RedLineRPM
        {
            get
            {
                return (tachometer.maxValue * 0.9f);
            }
        }

        private Slider tachometer;
        private Scrollbar shiftZone;

        public GuageCluster(InputManager inputManager)
        {
            tachometer = inputManager.GuageClusterGameObjects.Where(go => go.name == "Tachometer").FirstOrDefault().GetComponent<Slider>();
            shiftZone = inputManager.GuageClusterGameObjects.Where(go => go.name == "ShiftZone").FirstOrDefault().GetComponent<Scrollbar>();
        }

        public bool IsTachometerInShiftZone()
        {
            float currentScrollValue = shiftZone.value;
            float currentScrollWidth = shiftZone.size / 2;
            float scrollBarMinBound = currentScrollValue - currentScrollWidth;
            float scrollBarMaxBound = currentScrollValue + currentScrollWidth;

            return (CurrentRPM >= scrollBarMinBound && CurrentRPM <= scrollBarMaxBound);
        }      

        public void IncreaseRPM(float increaseAmount)
        {
            CurrentRPM += (increaseAmount * Time.deltaTime);
        }

        public void DecreaseRPM(float rpmIncreaseSpeed)
        {
            CurrentRPM -= (rpmIncreaseSpeed * Time.deltaTime);
        }
    }
}
