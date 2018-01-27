using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CantFindItGrindIt.Managers
{
    public class GameManager : MonoBehaviour
    {
        public float GameTime;

        public bool GameOver { get; private set; }

        private InputManager inputManager;
        private PlayerCar playerCar;

        // Use this for initialization
        void Start()
        {
            GameOver = false;
            GameTime = 0f;

            inputManager = GetComponent<InputManager>();

            playerCar = new PlayerCar(this, inputManager);
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameOver)
            {
                GameTime += Time.deltaTime;
            }

            playerCar.UpdateCarComponents();
        }        

        public void FinishGame()
        {
            GameOver = true;

            inputManager.GuageClusterGameObjects.ForEach(go => go.SetActive(false));
            inputManager.TransmissionGameObjects.ForEach(go => go.SetActive(false));

        }
    }
}