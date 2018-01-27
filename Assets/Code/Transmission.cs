﻿using UnityEngine;
using System.Collections;
using CantFindItGrindIt.UI;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using CantFindItGrindIt.Managers;

namespace CantFindItGrindIt
{
    public class Transmission
    {
        public Gear CurrentGear;

        private PedalButton acceleratorPedal;
        private PedalButton breakPedal;
        private PedalButton clutchPedal;
        private Slider gearShifter;

        private GameManager gameManager;
        private PlayerCar playerCar;
        private GuageCluster guageCluster;
        
        private float rpmIncreaseSpeed = 0.7f;
        private ShifterState requiredShiftState;


        private ShifterState currentShiftState
        {
            get
            {
                return (ShifterState)Mathf.RoundToInt(gearShifter.value);
            }
        }

        public Transmission(GameManager gameManager, InputManager inputManager, PlayerCar playerCar, GuageCluster guageCluster)
        {
            this.gameManager = gameManager;
            this.playerCar = playerCar;
            this.guageCluster = guageCluster;

            requiredShiftState = ShifterState.EvenGear;
            CurrentGear = new Gear(1, 0f, 50f);

            List<GameObject> transmissionGameObjects = inputManager.TransmissionGameObjects;

            acceleratorPedal = transmissionGameObjects.Where(butt => butt.name == "Accelerator").FirstOrDefault().GetComponent<PedalButton>();
            breakPedal = transmissionGameObjects.Where(butt => butt.name == "Break").FirstOrDefault().GetComponent<PedalButton>();
            clutchPedal = transmissionGameObjects.Where(butt => butt.name == "Clutch").FirstOrDefault().GetComponent<PedalButton>();

            gearShifter = transmissionGameObjects.Where(butt => butt.name == "Shifter").FirstOrDefault().GetComponent<Slider>();

            gearShifter.onValueChanged.AddListener(delegate { GearShifterMoved(); });
        }

        public void Tick()
        {
            if (acceleratorPedal.IsBeingHeldDown)
            {
                Accelerate();
                return;
            }

            if (guageCluster.CurrentRPM <= 0)
            {
                return;
            }

            if (breakPedal.IsBeingHeldDown)
            {
                Break();
            }

            Decelerate();
        }

        public void GearShifterMoved()
        {
            if (!clutchPedal.IsBeingHeldDown)
            { 
                return;
            }
            
            if (currentShiftState == requiredShiftState && guageCluster.IsTachometerInShiftZone())
            {
                NextGear();
            }            
        }

        private void Accelerate()
        {
            guageCluster.IncreaseRPM(rpmIncreaseSpeed);
        }

        private void Break()
        {
            float breakingRPMDecrease = ((rpmIncreaseSpeed / 2) * (1 + (guageCluster.CurrentRPM / guageCluster.RedLineRPM)));

            guageCluster.DecreaseRPM(breakingRPMDecrease);
        }

        private void Decelerate()
        {
            float deceleraingRPMDecrease = ((rpmIncreaseSpeed / 3) * (1 + (guageCluster.CurrentRPM / guageCluster.RedLineRPM)));

            guageCluster.DecreaseRPM(deceleraingRPMDecrease);
        }

        private void NextGear()
        {
            requiredShiftState = (requiredShiftState == ShifterState.OddGear ? ShifterState.EvenGear : ShifterState.OddGear);
            
            int nextGearInSequence = CurrentGear.NumberInSequence + 1;

            float nextMinSpeed = CurrentGear.SpeedMax * 0.8f;
            float nextMaxSpeed = nextMinSpeed + (CurrentGear.SpeedMax - CurrentGear.SpeedMin);

            CurrentGear = new Gear(nextGearInSequence, nextMinSpeed, nextMaxSpeed);
        }
    }
}