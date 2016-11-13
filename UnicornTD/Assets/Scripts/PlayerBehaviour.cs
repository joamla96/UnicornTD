using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    private int coins = 100;


    public void AddToCoins(int valueToAdd)
    {
        coins += valueToAdd;
    }

    public void RemoveToCoins(int valueToAdd)
    {
        coins -= valueToAdd;
    }
}
