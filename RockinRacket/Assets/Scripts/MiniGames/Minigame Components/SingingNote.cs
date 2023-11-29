using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingNote : MonoBehaviour
{
    [SerializeField] int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position.y, 0) * Time.deltaTime);
    }
}
