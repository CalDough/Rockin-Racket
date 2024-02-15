using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerManager : MonoBehaviour
{
    public Sticker[] stickers;
    public void SaveToJSON()
    {
        StickerSaver.SaveStickerData();
    }
    public void LoadFromJSON()
    {
        StickerSaver.LoadStickerData();
        UpdateStickers();
    }
    public void Reset()
    {
        StickerSaver.Reset();
    }

    private void Start()
    {
        UpdateStickers();
    }

    private void UpdateStickers()
    {
        Transform[] childOrder = new Transform[4];
        foreach (StickerSaver.StickerData stickerData in StickerSaver.stickerDatas)
        {
            //print(stickerData.bandmate);
            foreach (Sticker sticker in stickers)
            {
                if (sticker.bandmate.ToString() == stickerData.bandmate)
                {
                    sticker.transform.localPosition = new Vector3(stickerData.xPos, stickerData.yPos, 0);
                    childOrder[stickerData.childIndex] = sticker.transform;
                }
            }
        }
        foreach (Transform childTransform in childOrder)
        {
            childTransform.SetAsLastSibling();
        }
    }
}
