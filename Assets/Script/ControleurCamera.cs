using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleurCamera : MonoBehaviour
{
    //publiques
    public Transform personnage;

    //Limites de deplacement de camera
    public Transform pointHG;
    public Transform pointBD;

    //Privees
    Transform transfo;

    //taille camera
    float cameraDemiLargeur;
    float cameraDemiHauteur;


    void Start()
    {
        transfo = gameObject.transform;
        Camera cam = gameObject.GetComponent<Camera>();

        cameraDemiHauteur = cam.orthographicSize;
        cameraDemiLargeur = cam.aspect * cameraDemiHauteur;
    }


    void Update()
    {
        //Limite le deplacement de la camera
        float posHoriz, posVert;

        //Deplacement horizontal
        posHoriz = Mathf.Clamp(personnage.position.x, pointHG.position.x + cameraDemiLargeur, pointBD.position.x - cameraDemiLargeur);

        //Deplacement vertical
        posVert = Mathf.Clamp(personnage.position.y, pointBD.position.y + cameraDemiHauteur, pointHG.position.y - cameraDemiHauteur);

        //Suivi du personnage
        transfo.position = new Vector3(posHoriz, posVert, transfo.position.z);
    }
}
