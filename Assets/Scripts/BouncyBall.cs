using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using TMPro;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;

    Rigidbody2D rb;

    int score = 0;
    int lives = 5;

    public TextMeshProUGUI scoreTxt;
    public GameObject[] livesImage;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;

    int brickCount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        brickCount = FindObjectOfType<LevelGenerator>().transform.childCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY){
            if(lives <= 0){
                GameOver();
            }else{
                transform.position = Vector3.zero;
                rb.velocity = Vector3.zero;
                lives--;
                livesImage[lives].SetActive(false);
            }
            
        }

        if(rb.velocity.magnitude>maxVelocity){
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        // Debug.Log(collision.gameObject.name);
        if(collision.gameObject.CompareTag("Brick")){
            Destroy(collision.gameObject);
            score+=10;
            scoreTxt.text = score.ToString("00000");
            brickCount--;
            if(brickCount <= 0){
                youWinPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    void GameOver(){
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
