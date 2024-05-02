using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBarScript : MonoBehaviour
{
    public Slider slider;
    public int maxVal;
    public int minVal;

    void Start(){
        slider.maxValue = maxVal;
        slider.value = minVal;
    }
    public void SetVal(int val){
        slider.value = val;
    }

    public void IncreaseVal(){
        slider.value+= 1;
    }

    public void ResetSlider(){
        slider.value = 0;
    }
}
