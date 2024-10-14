using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


/*
 * Repas
 * Que hem vist:
 *      - Crea objectes a l'escena.
 *      -Crear EmptyObjects (per exemple per fer el GeneradorNumeros).
 *      -Prefabs (per crear objectes quan el joc esta en execusio).
 *         - Per crear-los : l'objecte que ja teniam creat l'arrosseguem a la carpeta Prefabs.
 *         - Per crear un prefab a l'escena en execucio: metode Instantiate(variablePrefab).
 *              -variablePrefab: variable de tipus gameObject
 *              
 *      - Trobar posicio objecte actual: transform.position
 *      - Trobar marges panbtalla: Camera.main.ViewportToWorldPoint().
 *      - [SerializeField]: per fer que una variable private de la classe es mostri a l'editor de Unity.
 *      - Utilizar una imatge/sprite com si fos mes d'una subimatge.
 *          - Seleccionem l'spirte
 *          - En l'opcio Spirte Mode canviem de Single a Multiple, i cliquem boto Apply.
 *          - Fem servir les opcions del boto Sprite Editor
 *          
 *      - Destruir objecte actual : Destroy(gameObject).
 *      - Crida un metode al acap de x segons: Invoke("NomMetode" , xf).
 *      - Cridar un metode al cap de x segons i cada y segons: InvokeRepeating("NomMetode", xf ,yf).
 *      - Com aturar un InvokeRepeating: CancelInvoke("NomMetode).
 *      - Detectar objecte toca a un altre:
 *          - Afegir als objectes que volem que es toquin, els components: BoxCollider2D i Rigidbody2D.
 *          - En BoxCollider2D : activar checkbox IsTrigger.
 *          - En Rigibody2D: GravityScale posar-lo a 0.
 *      
 */




public class NauJugador : MonoBehaviour
{
    private float _vel;

    private Vector2 minPantalla, maxPantalla;

    [SerializeField] private GameObject prefabProjectil;
    [SerializeField] private GameObject prefabExplosio;

    [SerializeField] private TMPro.TextMeshProUGUI componentTextVides;

    private int videsNau;


    // Start is called before the first frame update
    void Start()
    {
        _vel = 8f;
        minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float meitatMidaImatgeX = GetComponent<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x / 2;
        float meitatMidaImatgeY = GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y / 2;

        minPantalla.x = minPantalla.x + 0.75f;
        maxPantalla.x = maxPantalla.x - 0.75F;
        minPantalla.y += 0.75f;
        maxPantalla.y -= 0.75f;

        videsNau = 3;
    }

    // Update is called once per frame
    void Update()
    {
        MoureNau();
        DisparaProjectil();
        
    }

    private void DisparaProjectil()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject projectil = Instantiate(prefabProjectil);
            projectil.transform.position = transform.position;
        }
    }
    private void MoureNau()
    {

        float direccioIndicadaX = Input.GetAxisRaw("Horizontal");
        float direccioIndicadaY = Input.GetAxisRaw("Vertical");

        Vector2 direccioIndicada = new Vector2(direccioIndicadaX, direccioIndicadaY).normalized;

        Vector2 novaPos = transform.position;
        novaPos = novaPos + direccioIndicada * _vel * Time.deltaTime;

        novaPos.x = Mathf.Clamp(novaPos.x, minPantalla.x, maxPantalla.x);
        novaPos.y = Mathf.Clamp(novaPos.y, minPantalla.y, maxPantalla.y);

        transform.position = novaPos;
    }

    private void OnTriggerEnter2D(Collider2D objecteTocat)
    {
        if(objecteTocat.tag == "Numero")
        {
            videsNau--;
            componentTextVides.text = "Vides : " + videsNau.ToString();

            if(videsNau <= 0)
            {
                GameObject explosio = Instantiate(prefabExplosio);
                explosio.transform.position = transform.position;

                SceneManager.LoadScene("PantallaResultats");


                Destroy(gameObject);
            }
           
        }
    }
}


