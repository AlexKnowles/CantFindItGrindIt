using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CantFindItGrindIt.Managers
{
    public class GameManager : MonoBehaviour
    {
        public Text GoodShiftCountDisplay;
        public Text BadShiftCountDisplay;

        private int goodShiftCount = 0;
        private int badShiftCount = 0;

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
        
        public void RecordGoodShift()
        {
            goodShiftCount++;
        }

        public void RecordBadShift()
        {
            badShiftCount++;
        }

        public void UpdateDispalyOfShiftText()
        {
            GoodShiftCountDisplay.text = goodShiftCount.ToString();
            BadShiftCountDisplay.text = badShiftCount.ToString();
        }        
    }
}