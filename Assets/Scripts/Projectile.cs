using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction; //kierunek wystrza�u

    public float speed; //pr�dko��

    public System.Action destroyed; //czy zniszczony

    private void Update()
    {
        //wystrzel pocisk
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) //je�li mamy kolizj�
    {
        if(this.destroyed != null)
        {
            this.destroyed.Invoke();
        }
        Destroy(this.gameObject); //zniszcz obiekt
        
    }
}
