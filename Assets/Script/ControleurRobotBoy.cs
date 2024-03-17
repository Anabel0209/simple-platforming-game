using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControleurRobotBoy : MonoBehaviour
{
    //Publiques
    public float vitesse;
    public float impulsion;
    public GameObject imageVies;
    public AudioClip musiqueMort;
    public int nbViesMax = 5;

    //Privees
    int nbVies = 1;
    float deplacement;
    bool saute;
    int nbSauts;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // Initialisation
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();

        deplacement = 0;
        saute = false;
        nbSauts = 0;
    }

    // Controle + logique 
    void Update()
    {
        deplacement = Input.GetAxis("Horizontal");
        anim.SetFloat("Etat", Mathf.Abs(deplacement));

        //direction du robot
        if (deplacement < 0)
        {
            sr.flipX = true;
        }
        if (deplacement > 0)
        {
            sr.flipX = false;
        }

        //gestion de saut
        if (Input.GetKeyDown("w") && nbSauts < 2)
        {
            anim.SetTrigger("Saut") ;
            saute = true;
            nbSauts ++;
        }

        //gestion accroupissement
        if (Input.GetKeyDown("s"))
        {
            anim.SetBool("Accroupissement", true);
            vitesse = vitesse/2.0f;
        }
        if (Input.GetKeyUp("s"))
        {
            anim.SetBool("Accroupissement", false);
            vitesse = vitesse * 2.0f;
        }

        //gestion de roulade
        if (Input.GetKeyDown("x"))
        {
            anim.SetTrigger("Roulade");
        }

    }

    //Physique
    private void FixedUpdate()
    {
        //deplacement horizontal
        rb.AddRelativeForce(Vector2.right * vitesse * deplacement);

        //saut
        if (saute)
        {
            rb.AddRelativeForce(Vector2.up * impulsion, ForceMode2D.Impulse);
            saute = false;
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        nbSauts = 0;
        GestionCollisons(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GestionCollisons(collision.gameObject);
    }

    //Objets bonus, malus ou mortem
    void GestionCollisons(GameObject objet)
    {
        if (objet.CompareTag("Bonus"))
        {
            if(nbVies< nbViesMax) 
            { 
                nbVies++;
                AffichageVies();
            }
        }

        if (objet.CompareTag("Malus"))
        {
            nbVies--;
            AffichageVies();
        }

        if (objet.CompareTag("Mortem") || nbVies == 0)
        {
            nbVies = 0;
            AffichageVies();

            //Le robot arrete de bouger
            impulsion = 0;
            vitesse = 0;
            rb.simulated = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            anim.SetTrigger("Mort");

            //Audio
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().PlayOneShot(musiqueMort);

            //On recommence le jeu
            Invoke("Recommencer", 5f);
        }
    }
    
    //Redemarre le jeu
    void Recommencer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Realise l'affichage des coeurs
    void AffichageVies()
    {
        imageVies.GetComponent<RectTransform>().localScale = new Vector3(nbVies, 1f, 1f);
        imageVies.GetComponent<RawImage>().uvRect = new Rect(0, 0, nbVies, 1f);
    }
}
