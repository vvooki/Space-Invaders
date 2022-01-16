using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public Sprite[] bunkerSprites;
    public SpriteRenderer spriteRenderer;
    public int i = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            Debug.Log("zniszcz bunkry");
            this.gameObject.SetActive(false);
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            Debug.Log("i: " + i);
            if(i>4)
            {
                this.gameObject.SetActive(false);
            } else
            {
                spriteRenderer.sprite = this.bunkerSprites[i];
                i++;
            }

            
        }
    }
}
