using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class RaceSelectionController : MonoBehaviour
{

    public GameObject racesContainerGameObject;
    public GameObject currentRaceSelectedText;

    public int currentRace;

    public GameObject currentSoldierShowcase;
    public List<SpriteLibraryAsset> RaceModels;
    public GameObject[] baseHumanSoldiers;
    public GameObject[] baseElfSoldiers;
    public GameObject[] baseOrcSoldiers;
    public GameObject[] baseDemonSoldiers;
    public GameObject[] baseTrollSoldiers;
    public GameObject[] baseEasternHumanSoldiers;
    public GameObject[] baseWraithSoldiers;

    public List<string> raceInfos;
    public GameObject infoText;
    public GameObject LockObject;

    // Start is called before the first frame update
    void Start()
    {
        currentRace = 0;
        currentRaceSelectedText.GetComponent<TextMeshProUGUI>().text = racesContainerGameObject.GetComponent<RacesManager>().racesNames[currentRace];

        raceInfos.Add("+ SwordsMan HP");
        raceInfos.Add("+ Archer Damage");
        raceInfos.Add("+ SwordsMan Damage\n + AxeMan Damage");
        raceInfos.Add("+ SpearMan Damage\n + SpearMan HP");
        raceInfos.Add("+ Walking Speed");
        raceInfos.Add("+ SwordsMan Damage\n + SwordsMan HP");
        raceInfos.Add("+ Walking Speed\n + Sorcerer Damage");
        
        infoText.GetComponent<TextMeshProUGUI>().text = raceInfos[currentRace];
    }

    // Update is called once per frame
    void Update()
    {
        currentSoldierShowcase.GetComponent<SpriteLibrary>().spriteLibraryAsset = RaceModels[currentRace];
    }

    public void nextRace(){
        if (currentRace < racesContainerGameObject.GetComponent<RacesManager>().racesNames.Count-1){
            currentRace++;

            currentRaceSelectedText.GetComponent<TextMeshProUGUI>().text = racesContainerGameObject.GetComponent<RacesManager>().racesNames[currentRace];
            infoText.GetComponent<TextMeshProUGUI>().text = raceInfos[currentRace];

            if (infoText.GetComponent<TextMeshProUGUI>)
        }
    }

    public void prevRace(){
        if (currentRace > 0){
            currentRace--;

            currentRaceSelectedText.GetComponent<TextMeshProUGUI>().text = racesContainerGameObject.GetComponent<RacesManager>().racesNames[currentRace];
            infoText.GetComponent<TextMeshProUGUI>().text = raceInfos[currentRace];
        }
    }

    public void setCurrentRaceAndContinue(){
        GameManager.Instance.PlayerRace = currentRaceSelectedText.GetComponent<TextMeshProUGUI>().text;
        
        if (GameManager.Instance.PlayerRace.Equals("Human")){
            GameManager.Instance.PlayerSoldiers.AddRange(baseHumanSoldiers);
        }
        if (GameManager.Instance.PlayerRace.Equals("Elf")){
            GameManager.Instance.PlayerSoldiers.AddRange(baseElfSoldiers);
        }
        if (GameManager.Instance.PlayerRace.Equals("Orc")){
            GameManager.Instance.PlayerSoldiers.AddRange(baseOrcSoldiers);
        }
        if (GameManager.Instance.PlayerRace.Equals("Demon")){
            GameManager.Instance.PlayerSoldiers.AddRange(baseDemonSoldiers);
        }
        if (GameManager.Instance.PlayerRace.Equals("Troll")){
            GameManager.Instance.PlayerSoldiers.AddRange(baseTrollSoldiers);
        }
        if (GameManager.Instance.PlayerRace.Equals("EasternHuman")){
            GameManager.Instance.PlayerSoldiers.AddRange(baseEasternHumanSoldiers);
        }
        if (GameManager.Instance.PlayerRace.Equals("Wraith")){
            GameManager.Instance.PlayerSoldiers.AddRange(baseWraithSoldiers);
        }

        foreach (GameObject soldier in GameManager.Instance.PlayerSoldiers){
            GameManager.Instance.playerSoldierIDs.Add(soldier.GetComponent<Entity>().soldierID);
        }


        SceneManager.LoadScene("MapScene");

    }
}
