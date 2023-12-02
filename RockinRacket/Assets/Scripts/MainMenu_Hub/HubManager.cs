using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    [SerializeField] private GameObject boardScreen;
    [SerializeField] private GameObject car;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private TransitionData concertTransition;

    private readonly float animationDuration = 2f;
    private bool carAnim;

    public void LookAtBoard()
    {
        boardScreen.SetActive(true);
    }

    public void Drive()
    {
        if (!carAnim)
        {
            StartCoroutine(CarAnim());
            carAnim = true;
            // start changing scene when animation is 1 second from finishing
            StartCoroutine(ChangeScene(concertTransition, animationDuration - 1));
        }
    }

    private void Start()
    {
        boardScreen.SetActive(false);
    }

    private IEnumerator CarAnim()
    {
        Vector3 endPosition = new(-2000f, -160f, 0f);
        float counter = 0;

        //Get the current position of the object to be moved
        Vector3 startPosition = car.transform.localPosition;

        while (counter < animationDuration)
        {
            counter += Time.deltaTime;
            car.transform.localPosition = Vector3.Lerp(startPosition, endPosition, counter / animationDuration);
            yield return null;
        }
    }

    private IEnumerator ChangeScene(TransitionData transitionData, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        sceneLoader.SwitchScene(transitionData);
    }
}
