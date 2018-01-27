using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CantFindItGrindIt.Managers;

namespace CantFindItGrindIt
{
    public class PlayerCar 
    {
        private GameManager gameManager;
        private InputManager inputManager;

        private Transmission transmission;
        private GuageCluster guageCluster;

        public PlayerCar(GameManager gameManager, InputManager inputManager)
        {
            this.gameManager = gameManager;
            this.inputManager = inputManager;

            guageCluster = new GuageCluster(inputManager);
            transmission = new Transmission(gameManager, inputManager, this, guageCluster);
        }

        public void UpdateCarComponents()
        {
            transmission.Tick();
        }
    }
}