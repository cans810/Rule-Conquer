using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormBringerController : MonoBehaviour
{

    public bool playingAttackAnim;
    void Start(){
        GameObject entityObject = gameObject;
        Entity entity = entityObject.GetComponent<Entity>();

        if (entity.race.Equals("Human")){
            entity.HP = 34;
            entity.damage = 2.3f;
            entity.knockbackForce = 1f;
            entity.knockbackDuration = 0.1f;
            entity.speed = 0.85f;
        }
        entity.canGetKnockedBack = true;

        entity.canBurn = true;
        entity.canBeRipped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Entity>().dead){
            if (GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().colliding && 
            GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().currentHittingOpponent != null && !playingAttackAnim){

                gameObject.GetComponent<Entity>().animator.SetBool("Walk",false);

                int randomAttackAnim = Random.Range(0,100);

                playingAttackAnim = true;
                
                if (randomAttackAnim <= 15){
                    gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_1",true);
                }
                else if (randomAttackAnim > 15 && randomAttackAnim <= 52){
                    gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_2",true);
                }
                else if (randomAttackAnim > 52 && randomAttackAnim <= 100){
                    gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_3",true);
                }
            }
            else if (!GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().colliding && !playingAttackAnim){
                gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_1",false);
                gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_2",false);
                gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_3",false);
                gameObject.GetComponent<Entity>().animator.SetBool("Walk",true);
                GetComponent<EntityCommonActions>().walk(GetComponent<Entity>().direction,GetComponent<Entity>().speed);
            }
        }
    }

    public void damageSpecialAttack(){
        if (gameObject.GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().currentHittingOpponent != null)
        {
            Entity opponentEntity = gameObject.GetComponent<Entity>().HitBox.GetComponent<HitBoxController>().currentHittingOpponent.GetComponent<Entity>();
            
            if (opponentEntity != null)
            {
                opponentEntity.HP -= gameObject.GetComponent<Entity>().damage*6f;

                if (opponentEntity.canGetKnockedBack){
                    Vector2 direction = (opponentEntity.transform.position - transform.position).normalized;

                    opponentEntity.GetComponent<Rigidbody2D>().AddForce(direction * gameObject.GetComponent<Entity>().knockbackForce, ForceMode2D.Impulse);
                    opponentEntity.GetComponent<Entity>().gettingKnockedBack = true;

                    StartCoroutine(gameObject.GetComponent<Entity>().StopKnockback(opponentEntity.GetComponent<Rigidbody2D>()));
                }      
            }
        }
    }

    public void startSpecialAttack(){
        gameObject.GetComponent<Entity>().canGetKnockedBack = false;
    }

    public void playThunderSound(){
        gameObject.transform.Find("SoundManager").GetComponent<EntitySoundManager>().playSpecialSoldierSound(3);
    }

    public void stopAttackAnim(){
        playingAttackAnim = false;
        gameObject.GetComponent<Entity>().canGetKnockedBack = true;
        gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_1",false);
        gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_2",false);
        gameObject.GetComponent<Entity>().animator.SetBool("StormBringer_Attack_3",false);
    }
}
