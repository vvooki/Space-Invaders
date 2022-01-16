using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs; //array typów przeciwników 
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    public Projectile missilePrefab; 
    public float missileAttackRate = 1.0f;
    public int score;
    public Text ScoreText;
    public Text LivesText;
    public int amountKilled { get; private set; } //publiczny getter, prywatny setter
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders; 

    private Vector3 _direction = Vector2.right;



    private void Awake()
    {
        //points and hp set
        this.ScoreText.text = " " + Manager.points;
        this.LivesText.text = " " + Manager.hp;


        for (int row = 0; row < this.rows; row++)
        {
            //ca³kowity obszar ekranu
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2); //œrodek ekranu
            Vector3 rowPosition = new Vector3(centering.x, centering.y + row * 2.0f, 0.0f); //2.0f to odstêp

            for(int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void ReSpawn()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + row * 2.0f, 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Update()
    {
        //przemieœæ przreciwników = kierunek * | zwieksz predkosc im wiecej przeciwników zabitych |
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        //koordynaty lewej i prawej krawêdzi
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        //dla kazdego invadera w naszym Transform
        foreach(Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy) //jesli invader zabity przejdz do sprawdzania nastepnego invadera
            {
                continue;
            }
            
            //jeœli Invader porusza siê w prawo i jego pozycja jest >= prawej krawêdzi (znajduje siê poza obszarem ekranu)
            if(_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
            {
                AdvancedRow();
            }
            //jeœli Invader porusza siê w lewo i jego pozycja jest <= prawej krawêdzi
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
            {
                AdvancedRow();
            }
        }
    }

    private void AdvancedRow()
    {
        _direction.x *= -1.0f; //zmien kierunek poruszania

        //obni¿ pozycjê Invadera o jeden w dó³
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if(Random.value < (1.0f / (float)this.amountAlive))
            {
                Sounds.PlaySound("invaderShoot");
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKilled()
    {
        this.amountKilled++;
        Manager.points += 10;
        this.ScoreText.text = " " + Manager.points;

        if(this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
