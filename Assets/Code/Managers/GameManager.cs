using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CantFindItGrindIt.Managers
{
    public class GameManager : MonoBehaviour
    {
        private InputManager inputManager;
        private PlayerCar playerCar;

        // Use this for initialization
        void Start()
        {
            inputManager = GetComponent<InputManager>();

            playerCar = new PlayerCar(this, inputManager);
        }

        // Update is called once per frame
        void Update()
        {
            playerCar.UpdateCarComponents();
        }        
    }
}