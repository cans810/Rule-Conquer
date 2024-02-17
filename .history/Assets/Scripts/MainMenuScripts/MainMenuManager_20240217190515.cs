using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject creditsGameObject;
    public GameObject mainMenuUI;
    public GameObject loadGameUI;
    public GameObject howToUI;
    public GameObject continueGameButton;

    public void Awake(){
        creditsGameObject.SetActive(false);
        loadGameUI.SetActive(false);
        howToUI.SetActive(false);
        continueGameButton.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerSoldiers.Count != 0){
            continueGameButton.SetActive(true);
        }
    }

    public void LoadGameButton(){
        mainMenuUI.SetActive(false);
        creditsGameObject.SetActive(false);
        howToUI.SetActive(false);
        loadGameUI.SetActive(true);
        loadGameUI.GetComponent<LoadSoldiers>().LoadGame();
    }

    public void showCreditsScreen(){
        creditsGameObject.SetActive(true);
        gameObject.SetActive(false);
        howToUI.SetActive(false);
    }

    public void showHowToScreen(){
        howToUI.SetActive(true);
        creditsGameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void goBack(){
        creditsGameObject.SetActive(false);
        howToUI.SetActive(false);
        gameObject.SetActive(true);
    }

    public void advanceToRaceSelection(){
        DataPersistanceManager.instance.NewGame();
        SceneManager.LoadScene("RaceSelectionScene");
    }
}
