using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LongPress AcceleratorButton;
    public LongPress BreakButton;
    public LongPress ClutchButton;
    public Slider RevCounter;

    public Text GoodShiftCountDisplay;
    public Text BadShiftCountDisplay;

    public float RevSpeed = 5f;
    
    private int goodShiftCount = 0;
    private int badShiftCount = 0;

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

        if(RevCounter.value <= 0)
        {
            return;
        }

        if (BreakButton.IsDown)
        {
            Break();
        }

        Decelerate();
	}

    public void DoShift()
    {
        if(ClutchButton.IsDown)
        {
            RecordGoodShift();
        }
        else
        {
            RecordBadShift();
        }

        UpdateDispalyOfShiftText();
    }

    private void RecordGoodShift()
    {
        goodShiftCount++;
    }

    private void RecordBadShift()
    {
        badShiftCount++;
    }

    private void UpdateDispalyOfShiftText()
    {
        GoodShiftCountDisplay.text = goodShiftCount.ToString();
        BadShiftCountDisplay.text = badShiftCount.ToString();
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
