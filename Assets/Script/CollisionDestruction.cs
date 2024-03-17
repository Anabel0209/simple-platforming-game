using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CollisionDestruction : MonoBehaviour
{
    public float tempsDestructionApresCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision avec le robot
        if (collision.gameObject.CompareTag("Robot"))
        {
            //Animation de l'accessoire
            gameObject.GetComponent<PlayableDirector>().Play();

            //Destruction du collider apres impact
            if (gameObject.CompareTag("Bonus"))
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            }

            //Destruction de l'accessoire
            Destroy(gameObject, tempsDestructionApresCollision);
        }
    }


}
