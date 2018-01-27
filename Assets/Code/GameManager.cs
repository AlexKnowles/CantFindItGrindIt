﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LongPress AcceleratorButton;
    public LongPress BreakButton;
    public LongPress ClutchButton;
    public Slider RevCounter;
    public Slider Shifter;
    public Scrollbar ShiftZone;

    public Text GoodShiftCountDisplay;
    public Text BadShiftCountDisplay;
        
    private int goodShiftCount = 0;
    private int badShiftCount = 0;

    private float revSpeed = 0.8f;

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
            if (Shifter.value != 1 && IsRevcounterInRange())
            {
                RecordGoodShift();
            }
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
        RevCounter.value += (revSpeed * Time.deltaTime);
    }

    private void Break()
    {
        RevCounter.value -= (((revSpeed / 2) * (1 + (RevCounter.value / RevCounter.maxValue))) * Time.deltaTime);
    }
    
    private void Decelerate()
    {
        RevCounter.value -= (((revSpeed/3) * (1 + (RevCounter.value/RevCounter.maxValue))) * Time.deltaTime);
    }

    private bool IsRevcounterInRange()
    {
        float currentScrollValue = ShiftZone.value;
        float currentScrollWidth = ShiftZone.size / 2;
        float scrollBarMinBound = currentScrollValue - currentScrollWidth;
        float scrollBarMaxBound = currentScrollValue + currentScrollWidth;

        return (RevCounter.value >= scrollBarMinBound && RevCounter.value <= scrollBarMaxBound); 
    }

}
