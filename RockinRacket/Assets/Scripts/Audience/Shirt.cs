using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConcertAttendee;

public class Shirt : MonoBehaviour
{
    [SerializeField] private float decayTime = 2;
    public RequestableItem ShirtType;
    public Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    //public List<Sprite> sprites;
    //public Color color1 = Color.red;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, decayTime);
    }

    public void SetColor()
    {
        switch(ShirtType)
        {
            case RequestableItem.RedShirt:
                sr.color = Color.red;
                break;
            case RequestableItem.BlueShirt:
                sr.color = Color.blue;
                break;
            case RequestableItem.GreenShirt:
                sr.color = Color.green;
                break;
        }
    }
}
