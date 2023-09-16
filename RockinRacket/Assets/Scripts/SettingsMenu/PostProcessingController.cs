using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
/*
    This will be placed on the Global volume in each scene and will modify that scenes cameras and colors based
    on user settings. Currently it only adjusts Gamma, Brightness, Contrast.

*/
public class PostProcessingController : MonoBehaviour
{
    [SerializeField]
    private Volume globalVolume;

    private ColorAdjustments colorAdjustments;
    private LiftGammaGain liftGammaGain;

    private void Start()
    {
        if (globalVolume == null)
        {
            Debug.Log("Global Volume not assigned!");
            return;
        }

        if (!globalVolume.profile.TryGet(out colorAdjustments))
        {
            Debug.Log("No Color Adjustments found!");
        }
        
        if (!globalVolume.profile.TryGet(out liftGammaGain))
        {   
            Debug.Log("No Lift, Gamma, Gain adjustments found!");
        }
    }

    public void SetBrightness(float value)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = value;
        }
    }

    public void SetContrast(float value)
    {
        if (colorAdjustments != null)
        {   
            colorAdjustments.contrast.value = value;
        }
    }

    public void SetGamma(float value)
    {
        if (liftGammaGain != null)
        {
             liftGammaGain.gamma.value = new Vector4(value, value, value, value);
        }
    }
}
