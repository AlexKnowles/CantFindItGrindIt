using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CantFindItGrindIt.Managers;
using System;

namespace CantFindItGrindIt
{
    public class PlayerCar 
    {
        private GameManager gameManager;
        private InputManager inputManager;

        private Transmission transmission;
        private GuageCluster guageCluster;
        private Transform carModelTransform;
        private Vector3 carModelStartingPosition;

        public float CurrentDistanceInMeters { get; private set; }
        public float TopSpeedInKMH { get; private set; }
        public float TopSpeedInMPH
        {
            get
            {
                return TopSpeedInKMH * 0.621371f;
            }
        }

        public PlayerCar(GameManager gameManager, InputManager inputManager, GameObject carModel, AudioManager audioManager)
        {
            this.gameManager = gameManager;
            this.inputManager = inputManager;

            guageCluster = new GuageCluster(inputManager, audioManager);
            transmission = new Transmission(gameManager, inputManager, guageCluster);

            carModelTransform = carModel.GetComponent<Transform>();
            carModelStartingPosition = carModelTransform.position;
            CurrentDistanceInMeters = 0;
        }

        public void UpdateCarComponents()
        {
            transmission.Tick();

            UpdateDistance();
        }

        private void UpdateDistance()
        {
            float speedRange = (transmission.CurrentGear.SpeedMax - transmission.CurrentGear.SpeedMin);
            float currentSpeedInKMPerHour = transmission.CurrentGear.SpeedMin + (speedRange * guageCluster.CurrentRPM);
            float currentSpeedInMetersPerSecond = (((currentSpeedInKMPerHour * 1000) / 60) / 60);
            float currentSpeedInFrame = currentSpeedInMetersPerSecond * Time.deltaTime;

            CurrentDistanceInMeters += currentSpeedInFrame;

            Vector3 nextPosition = new Vector3(carModelStartingPosition.x - CurrentDistanceInMeters, carModelStartingPosition.y, carModelStartingPosition.z);

            carModelTransform.position = nextPosition;//Vector3.Lerp(carModelTransform.position, nextPosition, 5 * Time.deltaTime);

            UpdateTopSpeed(currentSpeedInKMPerHour);

            inputManager.DebugText.text = String.Format("Time: {0} s \nDistance: {1} Km \nGear: {2} \nSpeed: {3} KmH",
                                                         gameManager.GameTime.ToString("0.##"),
                                                         CurrentDistanceInMeters.ToString("#,#0"), 
                                                         transmission.CurrentGear.NumberInSequence, 
                                                         currentSpeedInKMPerHour.ToString("##,0"));

            if(CurrentDistanceInMeters >= 400)
            {
                gameManager.FinishGame();
            }
        }

        private void UpdateTopSpeed(float currentSpeedInKMPerHour)
        {
            if(currentSpeedInKMPerHour > TopSpeedInKMH)
            {
                TopSpeedInKMH = currentSpeedInKMPerHour;
            }
        }
    }
}