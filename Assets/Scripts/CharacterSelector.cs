using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    //Singleton
    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("There is already an instance of CharacterSelector in the scene");
            Destroy(this);
        }
    }

 public static CharacterScriptableObject GetData()
    {
        return instance.characterData;
    }

    public void SetCharacterData(CharacterScriptableObject character)
    {
        characterData = character;
    }

    // To be called immediately after assigning the character data in the palyerstats script in the game scene
    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
