using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
    This script is a scriptable object for easily making items in the editor and placing it onto a shop, inventory, or event reward
*/

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Test Item", order = 1)]
public class ItemTest : ScriptableObject
{
    public enum Minigame { Guitar, Microphone, Popcorn, Fire, TShirt };
    public enum Type { Instrument, Stage, Minigame };

    public int ID;
    public string itemName;
    public Sprite sprite;
    public Type itemType;
    public string description;
    public int cost;
    public double scoreMultiplier = 1;
    public GameObject minigameToSpawn;


    public List<Minigames> MinigameBonuses;

    [System.Serializable]
    public class Minigames
    {
        public Minigames(Minigame minigame, double frequency = 1, double difficulty = 1)
        {
            minigameName = minigame;
            freqMod = frequency;
            diffMod = difficulty;
        }
        public Minigame minigameName;
        public double freqMod;
        public double diffMod;
    }
}
