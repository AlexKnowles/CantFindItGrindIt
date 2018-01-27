using UnityEngine;
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
        private PedalButton acceleratorPedal;
        private PedalButton breakPedal;
        private PedalButton clutchPedal;
        private Slider gearShifter;

        private GameManager gameManager;
        private GuageCluster guageCluster;

        private float rpmIncreaseSpeed = 0.8f;
        
        public Transmission(GameManager gameManager, InputManager inputManager, GuageCluster guageCluster)
        {
            this.gameManager = gameManager;
            this.guageCluster = guageCluster;

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
            if (clutchPedal.IsBeingHeldDown)
            {
                if (gearShifter.value != 1 && guageCluster.IsTachometerInShiftZone())
                {
                    gameManager.RecordGoodShift();
                }
            }
            else
            {
                gameManager.RecordBadShift();
            }

            gameManager.UpdateDispalyOfShiftText();
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
    }
}