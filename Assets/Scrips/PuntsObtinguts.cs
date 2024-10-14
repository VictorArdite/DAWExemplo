using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntsObtinguts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = "Punts obtinguts : " + DadesGlobals.punts.ToString();
        Invoke("AnarAPantallaInici", 5f);
    }

    private void AnarAPantallaInici()
    {
        SceneManager.LoadScene("PantallaInici");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
