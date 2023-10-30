using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IpodOption : MonoBehaviour
{
    public IEnumerator ScaleOverTime(Vector3 toScale, Quaternion toRotation, float duration)
    {
        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = transform.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            //transform.rotation = 
            yield return null;
        }
    }
}
