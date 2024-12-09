using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, ICollectable
{
    public int ID;
    public Sprite sprite;

    public void OnCollected()
    {
        GameManager.gameManager.ItemCollected(sprite, ID);
        Destroy(gameObject);
    }
}
