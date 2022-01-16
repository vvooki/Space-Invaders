using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryShip : MonoBehaviour
{
    private Vector3 _direction = Vector2.right;
    public int speed = 2;
    bool sound = true;

    void Update()
    {
        if(sound)
            Sounds.PlaySound("ufo");
        this.transform.position += _direction * speed * Time.deltaTime;
        sound = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.gameObject.SetActive(false);
            Manager.points += 30;
            sound = false;
        }

        if (collision.gameObject.tag == "Respawn")
        {
            this.gameObject.SetActive(false);
            sound = false;
        }
    }

}
