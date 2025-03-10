using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int Orbs = 0, Coins = 0;
    public TextMeshProUGUI CoinText, OrbText;
    public Image[] Items;

    private void Awake()
    {
        if (GameManager.gameManager != null && GameManager.gameManager != this)
            Destroy(gameObject);
        else
        {
            GameManager.gameManager = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadGame();  
    }

    // Método para coleccionar orbes
    public void OrbCollected()
    {
        Orbs++;
        OrbText.text = "Orbs: " + Orbs;
    }

  
    public void CoinCollected(int i)
    {
        Coins += i;
        CoinText.text = "Coins: " + Coins;
    }

    
    public void ItemCollected(Sprite sprite, int id)
    {
        Items[id].sprite = sprite;
    }

  
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("Orbs", Orbs);

    
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);

  
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].sprite != null)
            {
                PlayerPrefs.SetString("Item_" + i, Items[i].sprite.name);
                Debug.Log("Item " + i + " saved: " + Items[i].sprite.name);
            }
        }

 
        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
        Debug.Log("Coins: " + Coins + ", Orbs: " + Orbs);
        Debug.Log("Player Position: " + transform.position);
    }

    public void LoadGame()
    {
       
        if (PlayerPrefs.HasKey("Coins"))
        {
            Coins = PlayerPrefs.GetInt("Coins");
            Debug.Log("Coins Loaded: " + Coins);
        }

        if (PlayerPrefs.HasKey("Orbs"))
        {
            Orbs = PlayerPrefs.GetInt("Orbs");
            Debug.Log("Orbs Loaded: " + Orbs);
        }

        
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            transform.position = new Vector3(x, y, z);
            Debug.Log("Player Position Loaded: " + transform.position);
        }

     
        for (int i = 0; i < Items.Length; i++)
        {
            if (PlayerPrefs.HasKey("Item_" + i))
            {
                string spriteName = PlayerPrefs.GetString("Item_" + i);
                Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
                if (loadedSprite != null)
                {
                    Items[i].sprite = loadedSprite;
                    Debug.Log("Item " + i + " Loaded: " + loadedSprite.name);
                }
                else
                {
                    Debug.LogWarning("Item " + i + " not found in resources: " + spriteName);
                }
            }
        }

      
        CoinText.text = "Coins: " + Coins;
        OrbText.text = "Orbs: " + Orbs;

        Debug.Log("Game Loaded!");
    }

 
    private void Update()
    {
     
        if (Input.GetKeyDown(KeyCode.Alpha0))  
        {
            SaveGame();  
            Debug.Log("Game Saved via 0 Key!");
        }
    }
}
