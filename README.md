# Unity2D-Breakout
# Play game Breakout
![image](https://github.com/user-attachments/assets/3ec05e57-7cd7-44cc-b4c1-417a7a3c9f48)
# Game Over
![image](https://github.com/user-attachments/assets/2845b7ee-7d10-494f-b509-e27ca3d7449d)
# BouncyBall
~~~
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
~~~
# LevelGenerator
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{

    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPrefab;
    public Gradient gradient;
    
    private void Awake(){
        for(int i = 0; i < size.x; i++){
            for(int j = 0; j < size.y; j++){
                GameObject newBrick = Instantiate(brickPrefab, transform);
                newBrick.transform.position = transform.position + new Vector3((float)((size.x-1)*.5f- i) * offset.x, j * offset.y, 0);
                newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)j/(size.y - 1));
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
```
# PlayMovement
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;
    public float maxX = 7.5f;
    float movementHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementHorizontal = Input.GetAxis("Horizontal");
        if((movementHorizontal>0 && transform.position.x<maxX) || (movementHorizontal<0 && transform.position.x > -maxX))
        {
            transform.position += Vector3.right*movementHorizontal*speed*Time.deltaTime;
        }
        
    }
}

```
