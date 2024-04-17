using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    public int rotation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveNote(30, .3f));
    }

    private IEnumerator MoveNote(float animationTime, float animationSpeed)
    {
        int direction = 1;
        float counter = 0;
        float moveCounter = 0;
        while (counter < animationTime)
        {
            counter += Time.unscaledDeltaTime;
            moveCounter += Time.unscaledDeltaTime;
            if (moveCounter > animationSpeed)
            {
                transform.Rotate(0, 0, rotation * direction);
                moveCounter = 0;
                direction *= -1;
            }
            yield return null;
        }
    }
}
