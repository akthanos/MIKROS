using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBehavior : MonoBehaviour
{
    public String parameterName;
    public void UpdateValue(float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }
}