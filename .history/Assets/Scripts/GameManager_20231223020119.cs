using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public List<GameObject> PlayerSoldiers;
    public string PlayerRace;
    public Color playerLandColor;
    
    public string CurrentEnemyName;
    public string CurrentEnemyRace;
    public List<GameObject> CurrentEnemySoldiers;

    public List<string> AllConqueredCityNames;
    public List<GameObject> AllNeighbours;
    public bool allLandsConquered;
    
    public int balance;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // If not, set this instance as the singleton instance
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this instance
            // Ensures there's only one GameManager throughout the game
            Destroy(gameObject);
        }

        // Ensure this instance persists between scenes
        DontDestroyOnLoad(gameObject);

        AllConqueredCityNames = new List<string>();
        AllNeighbours = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (allLandsConquered){
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
