using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CantFindItGrindIt.UI;
using System;
using UnityEngine.UI;

namespace CantFindItGrindIt.Managers
{
    public class InputManager : MonoBehaviour
    {
        public List<GameObject> TransmissionGameObjects = new List<GameObject>();
        public List<GameObject> GuageClusterGameObjects = new List<GameObject>();

        public Text DebugText;


        public void ActivateAllControls()
        {
            GuageClusterGameObjects.ForEach(go => go.SetActive(true));
            TransmissionGameObjects.ForEach(go => go.SetActive(true));
        }

        public void DeactivateAllControls()
        {
            GuageClusterGameObjects.ForEach(go => go.SetActive(false));
            TransmissionGameObjects.ForEach(go => go.SetActive(false));
        }

    }
}
