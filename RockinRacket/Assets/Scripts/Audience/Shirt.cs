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
    public List<Sprite> sprites;
    //public Color color1 = Color.red;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, decayTime);
    }

    public void SetColor(ConcertAttendee.RequestableItem item)
    {
        ShirtType = item;
        switch(ShirtType)
        {
            case RequestableItem.RedShirt:
                sr.sprite = sprites[0];
                break;
            case RequestableItem.BlackShirt:
                sr.sprite = sprites[1];
                break;
            case RequestableItem.WhiteShirt:
                sr.sprite = sprites[2];
                break;
        }
    }
}
