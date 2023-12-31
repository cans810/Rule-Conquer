using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    //public GameObject BattleGround;
    public GameObject PlayerSummonPoints;
    public GameObject PlayerSoldierContainers;
    public int currentSelectedSummonPoint;
    public int currentSelectedSoldierContainer;

    public float playerProgress;

    public bool playerWon;
    public bool playerLost;
    public bool battleEnded;
    
    public int enemyDeathCounterUlti;
    public int playerDeathCounterUlti;
    public int enemyDeathCounterCoin;

    public GameObject pressQText;
    public GameObject winCanvas;
    public GameObject loseCanvas;

    public void Awake(){
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        pressQText.SetActive(false);
        Transform parentTransformSoldierContainers = PlayerSoldierContainers.transform;
        parentTransformSoldierContainers.gameObject.GetComponent<PlayerSideSoldierContainersManager>().initContainers();

        currentSelectedSummonPoint = 0;
        currentSelectedSoldierContainer = 0;

        Transform parentTransformSummonPoints = PlayerSummonPoints.transform;
        parentTransformSummonPoints.GetChild(currentSelectedSummonPoint).GetComponent<SummonPointManager>().selected = true;
        parentTransformSoldierContainers.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().selected = true;

        playerProgress = 50;
        enemyDeathCounterCoin = 0;

        playerWon = false;
        playerLost = false;
        battleEnded = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!battleEnded){
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentSelectedSummonPoint-1 >= 0){
                    Transform parentTransform = PlayerSummonPoints.transform;
                    parentTransform.GetChild(currentSelectedSummonPoint).GetComponent<SummonPointManager>().selected = false;

                    currentSelectedSummonPoint--;
                    
                    parentTransform.GetChild(currentSelectedSummonPoint).GetComponent<SummonPointManager>().selected = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentSelectedSummonPoint+1 <= 7){
                    Transform parentTransform = PlayerSummonPoints.transform;
                    parentTransform.GetChild(currentSelectedSummonPoint).GetComponent<SummonPointManager>().selected = false;

                    currentSelectedSummonPoint++;
                    
                    parentTransform.GetChild(currentSelectedSummonPoint).GetComponent<SummonPointManager>().selected = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentSelectedSoldierContainer - 1 >= 0)
                {
                    Transform parentTransform = PlayerSoldierContainers.transform;
                    parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().selected = false;

                    currentSelectedSoldierContainer--;

                    parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().selected = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentSelectedSoldierContainer + 1 < GameManager.Instance.PlayerSoldiers.Count)
                {
                    Transform parentTransform = PlayerSoldierContainers.transform;
                    parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().selected = false;

                    currentSelectedSoldierContainer++;

                    parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().selected = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Transform parentTransform = PlayerSoldierContainers.transform;

                if (parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().canSummon){
                    Transform transformSummonPoint = PlayerSummonPoints.transform;

                    Transform summonPoint = transformSummonPoint.GetChild(currentSelectedSummonPoint);

                    float summonPointHeight = summonPoint.GetComponent<Renderer>().bounds.size.y;

                    Vector3 spawnPosition = summonPoint.position - new Vector3(0.5f, summonPointHeight / 2f, 0);

                    if (parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().SoldierContained.name.Equals("EasternLion")){
                        spawnPosition = summonPoint.position - new Vector3(0.5f, summonPointHeight / 0.9f, 0);
                        Debug.Log("aaaa");
                    }

                    GameObject playerSoldier = Instantiate(
                        parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().SoldierContained,
                        spawnPosition,
                        Quaternion.identity);

                    playerSoldier.tag = "Player";
                    playerSoldier.GetComponent<Entity>().direction = "right";
                    playerSoldier.GetComponent<Entity>().spawnedAtRow = currentSelectedSummonPoint;

                    // summonladıktan sonra hepsini resetle
                    ResetPlayerSoldierContainers();
                }
            }

            if (enemyDeathCounterUlti >= 20){
                pressQText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Transform parentTransform = PlayerSoldierContainers.transform;

                    if (parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().canSummon){
                        Transform transformSummonPoint = PlayerSummonPoints.transform;

                        for (int i=0;i<8;i++){
                            Transform summonPoint = transformSummonPoint.GetChild(i);

                            float summonPointHeight = summonPoint.GetComponent<Renderer>().bounds.size.y;

                            Vector3 spawnPosition = summonPoint.position - new Vector3(0.5f, summonPointHeight / 2f, 0);

                            GameObject playerSoldier = Instantiate(
                                parentTransform.GetChild(currentSelectedSoldierContainer).GetComponent<SoldierContainerManager>().SoldierContained,
                                spawnPosition,
                                Quaternion.identity);

                            playerSoldier.tag = "Player";
                            playerSoldier.GetComponent<Entity>().direction = "right";
                            playerSoldier.GetComponent<Entity>().spawnedAtRow = i;
                        }

                        // summonladıktan sonra hepsini resetle
                        ResetPlayerSoldierContainers();
                        enemyDeathCounterUlti = 0;
                    }
                }
            }
            else{
                pressQText.SetActive(false);
            }
        }

        if (playerWon && !battleEnded){
            GameManager.Instance.AllConqueredCityNames.Add(GameManager.Instance.CurrentEnemyName);
            battleEnded = true;
            winCanvas.SetActive(true);
            
            GameManager.Instance.balance += 300 + enemyDeathCounterCoin;
        }
        else if (playerLost && !battleEnded){
            battleEnded = true;
            loseCanvas.SetActive(true);

            GameManager.Instance.balance -= 50;
        }

    }


    public void ResetPlayerSoldierContainers(){
        Transform parentTransformSoldierContainers = PlayerSoldierContainers.transform;

        foreach (Transform child in parentTransformSoldierContainers){
            child.gameObject.GetComponent<SoldierContainerManager>().timer = 0;
            child.gameObject.GetComponent<SoldierContainerManager>().canSummon = false;
        }
    }
}
