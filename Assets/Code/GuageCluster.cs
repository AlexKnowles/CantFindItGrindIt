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
        private AudioManager audioManager;

        public GuageCluster(InputManager inputManager, AudioManager audioManager)
        {
            this.audioManager = audioManager;
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
            if(CurrentRPM == 0) {
                this.audioManager.playIdleSound();
            }
            else if(CurrentRPM < 0.3)
            {
                this.audioManager.playLowRpm();
            }
            else if(CurrentRPM >= 0.3 && CurrentRPM <= 0.6)
            {
                this.audioManager.playMedRpm();
            }
            else if(CurrentRPM > 0.6 && CurrentRPM <= 0.9)
            {
                this.audioManager.playHighRpm();
            }
            else if(CurrentRPM > 0.95)
            {
                this.audioManager.playMaxRpm();
            }
            CurrentRPM += (increaseAmount * Time.deltaTime);
        }

        public void DecreaseRPM(float rpmIncreaseSpeed)
        {
            if(CurrentRPM > 0.95)
            {
                this.audioManager.playMaxRpmOff();
            }
            else if(CurrentRPM > 0.6 && CurrentRPM <= 0.9)
            {
                this.audioManager.playMidRpmOff();
            }
            else if(CurrentRPM < 0.6 && CurrentRPM > 0)
            {
                this.audioManager.playLowOff();
            }
            CurrentRPM -= (rpmIncreaseSpeed * Time.deltaTime);
        }

        public void changeShiftZoneSize()
        {
            float sizeMin = 0f;
            float sizeMax = 0.5f;

            float valueMin = 0.5f;
            float valueMax = 1f;

            float randomSize = UnityEngine.Random.Range(sizeMin, sizeMax);
            float randomValue = UnityEngine.Random.Range(valueMin, valueMax);

            shiftZone.size = randomSize;
            shiftZone.value = randomValue;
        }
    }
}
