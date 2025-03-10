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

    public GameObject playerObject; // Referencia al objeto del jugador (con CharacterController)

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

    // Método para coleccionar monedas
    public void CoinCollected(int i)
    {
        Coins += i;
        CoinText.text = "Coins: " + Coins;
    }

    // Método para coleccionar objetos
    public void ItemCollected(Sprite sprite, int id)
    {
        Items[id].sprite = sprite;
    }

    // Guardar el juego
    public void SaveGame()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.SetInt("Orbs", Orbs);

        PlayerPrefs.SetFloat("PlayerPosX", playerObject.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", playerObject.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", playerObject.transform.position.z);

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
        Debug.Log("Player Position: " + playerObject.transform.position);
    }

    // Cargar el juego
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

            // Llamar a la corrutina para cambiar la posición del jugador
            StartCoroutine(ChangePlayerPositionWithDelay(x, y, z));
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

    // Corrutina para cambiar la posición del jugador con un pequeño retraso y desactivar/reactivar CharacterController
    private IEnumerator ChangePlayerPositionWithDelay(float x, float y, float z)
    {
        // Obtener el CharacterController del objeto del jugador
        CharacterController characterController = playerObject.GetComponent<CharacterController>();

        // Verificar si el CharacterController está presente
        if (characterController != null)
        {
            characterController.enabled = false;  // Desactivar CharacterController
            Debug.Log("CharacterController disabled");
        }
        else
        {
            Debug.LogWarning("CharacterController no encontrado en el objeto.");
        }

        // Cambiar la posición del jugador
        playerObject.transform.position = new Vector3(x, y, z);
        Debug.Log("Player Position Updated: " + playerObject.transform.position);

        // Esperar un pequeño retraso antes de reactivar el CharacterController
        yield return new WaitForSeconds(0.1f);

        // Reactivar el CharacterController
        if (characterController != null)
        {
            characterController.enabled = true;
            Debug.Log("CharacterController enabled");
        }
    }

    // Guardar el juego al presionar la tecla '0'
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))  
        {
            SaveGame();  
            Debug.Log("Game Saved via 0 Key!");
        }
    }
}

