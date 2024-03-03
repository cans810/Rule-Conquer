using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndLineController : MonoBehaviour
{
    public GameObject battleController;
    public float fillAmount;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag.Equals("Player")){
            if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("TrollGiant")){
                fillAmount = 7;
            }
            else if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("Mammoth")){
                fillAmount = 7;
            }
            else if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("OrcBeast")){
                fillAmount = 7;
            }
            else if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("Minotaur")){
                fillAmount = 7;
            }
            else if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("StormBringer")){
                fillAmount = 6;
            }
            else if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("Warlord")){
                fillAmount = 6;
            }
            else if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("WraithCaller")){
                fillAmount = 4;
            }
            else if (otherCollider.gameObject.GetComponent<Entity>().soldierType.Equals("Cthulhu")){
                fillAmount = 7;
            }
            else{
                fillAmount = 2.5f;
            }
            battleController.GetComponent<BattleController>().playerProgress += fillAmount;
            Destroy(otherCollider.gameObject);
        }
    }
}
