using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class StickerSaver
{
    private static readonly string saveFolderPath = "Player/SaveFiles/Stickers/";
    private static readonly string saveFileName = "Sticker.json";

    public static StickerData[] stickerDatas = new StickerData[4];

    public static void SaveStickerData()
    {
        Directory.CreateDirectory(saveFolderPath);

        foreach (StickerData stickerData in stickerDatas)
        {
            string filePath = saveFolderPath + stickerData.bandmate.ToString() + saveFileName;
            string jsonData = JsonUtility.ToJson(stickerData, prettyPrint: true);
            Debug.Log("Saved: " + filePath);
            File.WriteAllText(filePath, jsonData);
        }
    }

    public static void LoadStickerData()
    {
        Directory.CreateDirectory(saveFolderPath);

        foreach (Bandmate bandmate in Enum.GetValues(typeof(Bandmate)))
        {
            string filePath = saveFolderPath + bandmate.ToString() + saveFileName;
            string jsonData = File.ReadAllText(filePath);
            Debug.Log("Loaded: " + filePath);
            StickerData loadedData = JsonUtility.FromJson<StickerData>(jsonData);
            stickerDatas[(int)bandmate] = loadedData;
        }
    }

    public static void Reset()
    {
        foreach (Bandmate bandmate in Enum.GetValues(typeof(Bandmate)))
        {
            StickerData stickerData = new()
            {
                bandmate = bandmate.ToString(),
                childIndex = (int)bandmate
            };
            stickerDatas[(int)bandmate] = stickerData;
        }
        SaveStickerData();
    }

    public static void UpdateSticker(Bandmate bandmate, Transform sticker)
    {
        StickerData stickerData = GetStickerData(bandmate);
        UpdateChildIndices(bandmate, stickerData.childIndex);

        stickerData.xPos = sticker.localPosition.x;
        stickerData.yPos = sticker.localPosition.y;
    }

    private static StickerData GetStickerData(Bandmate bandmate)
    {
        foreach (StickerData stickerData in stickerDatas)
        {
            if (stickerData.bandmate == bandmate.ToString())
            {
                return stickerData;
            }
        }
        Debug.Log("ERROR: no stickerData for that bandmate");
        return null;
    }

    private static void UpdateChildIndices(Bandmate bandmate, int index)
    {
        foreach (StickerData stickerData in stickerDatas)
        {
            if (stickerData.bandmate == bandmate.ToString())
            {
                stickerData.childIndex = 3;
            }
            else if (stickerData.childIndex > index)
            {
                stickerData.childIndex -= 1;
            }
        }
    }

    [Serializable]
    public class StickerData
    {
        public string bandmate;
        public int childIndex;
        public float xPos = 0;
        public float yPos = 0;
    }
}