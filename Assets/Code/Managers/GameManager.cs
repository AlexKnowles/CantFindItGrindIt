using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CantFindItGrindIt.Managers
{
    public class GameManager : MonoBehaviour
    {
        public float GameTime;
        public float TopSpeed;
        public GameObject GameOverPopup;
        public GameObject playerCarModel;

        public bool GameOver { get; private set; }

        private InputManager inputManager;
        private PlayerCar playerCar;
        private Vector3 playerCarStartingPosition;

        // Use this for initialization
        void Start()
        {
            inputManager = GetComponent<InputManager>();

            playerCarStartingPosition = playerCarModel.transform.position;
            RestartGame();
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

            inputManager.DeactivateAllControls();

            GameOverPopup.GetComponentInChildren<Text>().text = string.Format("You compeleted the 1/4 mile in {0} seconds, hitting a top speed of {1} MPH!", GameTime.ToString("0.##"), playerCar.TopSpeedInMPH.ToString("#"));

            GameOverPopup.SetActive(true);
        }

        public void RestartGame()
        {
            GameTime = 0f;
            TopSpeed = 0f;

            playerCarModel.transform.position = playerCarStartingPosition;

            GameOverPopup.SetActive(false);
            inputManager.ActivateAllControls();

            playerCar = new PlayerCar(this, inputManager, playerCarModel);

            GameOver = false;
        }
    }
}