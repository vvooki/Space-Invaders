using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed = 6.0f;

    private bool _laserActive;
    private bool moveLeft = false, moveRight = false;

    public Sprite explosion;
    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || moveLeft)
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || moveRight)
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) /*|| Input.GetMouseButtonDown(0)*/)
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }


    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void StopMoving()
    {
        moveRight = false;
        moveLeft = false;
    }


    public void Shoot()
    {
        if(!_laserActive)
        {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity); //identity bez obrotów
            Sounds.PlaySound("shoot");
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
        
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            Manager.hp--;
            Sounds.PlaySound("playerKilled");
            _laserActive = true;
            spriteRenderer.sprite = explosion;

            if (Manager.points > PlayerPrefs.GetInt("HighScore"))
            {
                Manager.hiScore = Manager.points;
                PlayerPrefs.SetInt("HighScore", Manager.hiScore);
            }

            if (Manager.hp < 1)
            {
                Manager.hp = 3;
                Manager.endPoints = Manager.points;
                Manager.points = 0;
                SceneManager.LoadScene(2);
            }
            
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
