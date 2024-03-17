using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireAccessoires : MonoBehaviour
{
    //Biscuit
    public GameObject biscuit;
    public int nbBiscuit;

    //Coeur
    public GameObject coeur;
    public int nbCoeur;

    //Flocon
    public GameObject flocon;
    public int nbFlocon;
    public float intervalleFlocons;

    //Limites de la scene
    public Transform pointHG;
    public Transform pointBD;
    public Transform pointHauteurPlateforme; //pour ne pas que les accessoires apparaissent dans les plateformes 
    public Transform pointSafeZone; //pour ne pas qu'un malus apparaisse directement sur le robot en début de partie 

    void Start()
    {
        //Biscuits
        for (int i = 0; i < nbBiscuit; i++)
        {
            //Position aleatoire
            float x = Random.Range(pointSafeZone.position.x, pointBD.position.x);
            float y = Random.Range(pointHauteurPlateforme.position.y, pointHG.position.y);

            //Creation d'objet
            Instantiate(biscuit, new Vector2(x, y), Quaternion.identity, gameObject.transform.parent);
        }

        //Coeurs
        for (int i = 0; i < nbCoeur; i++)
        {
            //Position aleatoire
            float x = Random.Range(pointHG.position.x, pointBD.position.x);
            float y = Random.Range(pointHauteurPlateforme.position.y, pointHG.position.y);

            //Creation d'objet
            Instantiate(coeur, new Vector2(x, y), Quaternion.identity, gameObject.transform.parent);
        }

        //Flocons
        InvokeRepeating("CreationFlocons", intervalleFlocons, intervalleFlocons);

    }
    //Fonctions qui cree des flocons
    void CreationFlocons()
    {
        for (int i = 0; i < nbFlocon; i++)
        {
            //Position aleatoire
            float x = Random.Range(pointHG.position.x, pointBD.position.x);
            float y = pointHG.position.y;

            //Creation d'objet
            Instantiate(flocon, new Vector2(x, y), Quaternion.identity, gameObject.transform.parent);
        }
    }

}
