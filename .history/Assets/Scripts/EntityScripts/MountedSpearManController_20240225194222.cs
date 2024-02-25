using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountedSpearManController : MonoBehaviour
{
    public bool playingAttackAnim;
    void Start(){
        GameObject entityObject = gameObject;
        Entity entity = entityObject.GetComponent<Entity>();

        if (entity.race.Equals("Human")){
            entity.HP = 14;
            entity.damage = 1.2f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.3f;
        }
        else if (entity.race.Equals("Orc")){
            entity.HP = 15;
            entity.damage = 1.2f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.3f;
        }
        else if (entity.race.Equals("Troll")){
            entity.HP = 15;
            entity.damage = 1.2f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.37f;
        }
        else if (entity.race.Equals("Demon")){
            entity.HP = 15;
            entity.damage = 1.2f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.3f;
        }
        else if (entity.race.Equals("Elf")){
            entity.HP = 14;
            entity.damage = 1.2f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.3f;
        }
        else if (entity.race.Equals("EasternHuman")){
            entity.HP = 14;
            entity.damage = 1.2f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.3f;
        }
        else if (entity.race.Equals("Wraith")){
            entity.HP = 14;
            entity.damage = 1.2f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.39f;
        }
        else if (entity.race.Equals("SeaElf")){
            entity.HP = 16;
            entity.damage = 1.4f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.2f;
            entity.speed = 1.39f;
        }
        entity.canGetKnockedBack = true;
        entity.canBurn = true;
        entity.canBeRipped = false;
        entity.canDarkMagicEffect = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Entity>().dead){
            if (GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().colliding && 
            GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().currentHittingOpponent != null && !playingAttackAnim && !GetComponent<Entity>().burning){

                gameObject.GetComponent<Entity>().animator.SetBool("MountedSoldier_Walk",false);

                int randomAttackAnim = Random.Range(0,2);

                playingAttackAnim = true;
                
                if (randomAttackAnim == 0){
                    gameObject.GetComponent<Entity>().animator.SetBool("MountedSpearman_Attack",true);
                }
                else if (randomAttackAnim == 1){
                    gameObject.GetComponent<Entity>().animator.SetBool("MountedSpearman_Attack_2",true);
                }
            }
            else if (!GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().colliding && !playingAttackAnim && !GetComponent<Entity>().burning){
                gameObject.GetComponent<Entity>().animator.SetBool("MountedSpearman_Attack",false);
                gameObject.GetComponent<Entity>().animator.SetBool("MountedSpearman_Attack_2",false);
                gameObject.GetComponent<Entity>().animator.SetBool("MountedSoldier_Walk",true);
                GetComponent<EntityCommonActions>().walk(GetComponent<Entity>().direction,GetComponent<Entity>().speed);
            }
        }
    }

    public void stopAttackAnim(){
        playingAttackAnim = false;
        gameObject.GetComponent<Entity>().animator.SetBool("MountedSpearman_Attack",false);
        gameObject.GetComponent<Entity>().animator.SetBool("MountedSpearman_Attack_2",false);
    }
}
