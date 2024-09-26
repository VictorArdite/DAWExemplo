using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NauJugador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _vel = 8f;
        minPantalla = Camera.main.ViewportToWorldPoint(Vector2(x0,y0));
        maxPantalla = Camera.main.ViewportToWorldPoint(Vector2(x0,y0));

        float meitarMidaImatgeX = GetComponnet<SpriteRenderer>().sprite.bounds.size.x * transform.localScale.x/2;
        float meitarMidaImatgeY = GetComponnet<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y /2;

        minPantalla.x = minPantalla.x +0.75f;
        maxPantalla.x = maxPantalla.x - 0.75f;
        minPantalla.y += meitarMidaImatgeY;
        maxPantalla.y -= meitarMidaImatgeY;
    }

    // Update is called once per frame
    void Update()
    {
        float direccioIndicadaX = Input.GetAxisRaw("Horizontal");
        float direccioIndicadaY = Input.GetAxisRaw("Vertical");

        Vector2 direccioIndicada = new Vector2(direccioIndicadaX,direccioIndicadaY).normalized;

        Vector2 novaPos = transform.position;
        novaPos = novaPos + direccioIndicada * _vel * Time.deltaTime;

        novaPos.x = Math.Clamp(novaPos.x,minPantalla.x,maxPantalla.x);
        novaPos.x = Math.Clamp(novaPos.y,minPantalla.y,maxPantalla.y);

        transform.position=novaPos;
    }
}
