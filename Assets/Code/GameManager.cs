using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LongPress AcceleratorButton;
    public LongPress BreakButton;
    public Slider RevCounter;

    public float RevSpeed = 5f;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (AcceleratorButton.IsDown)
        {
            Accelerate();
            return;
        }

        if (BreakButton.IsDown)
        {
            Break();
        }

        Decelerate();
	}

    private void Accelerate()
    {
        RevCounter.value += (RevSpeed * Time.deltaTime);
    }

    private void Break()
    {
        RevCounter.value -= (((RevSpeed / 2) * (1 + (RevCounter.value / RevCounter.maxValue))) * Time.deltaTime);
    }
    
    private void Decelerate()
    {
        RevCounter.value -= (((RevSpeed/3) * (1 + (RevCounter.value/RevCounter.maxValue))) * Time.deltaTime);
    }
}
