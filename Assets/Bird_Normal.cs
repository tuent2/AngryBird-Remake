using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bird_Normal : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public float speed;
    public float maxDrag;
    Vector2 startPosition;
    Vector2 currentPosition;
    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        startPosition = rb.position;
        rb.isKinematic = true;
        
    }

   
    void Update()
    {
        
    }

    void OnMouseDown() {
        sprite.color = Color.red;
    }

    void OnMouseUp() {
        currentPosition = rb.position;
        direction=startPosition - currentPosition;
        direction.Normalize();
        rb.isKinematic = false;

        rb.AddForce(direction*speed);
        sprite.color = Color.white;
    }

    void OnMouseDrag() {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // transform.position = new Vector3(mousePosition.x,mousePosition.y, transform.position.z);

        Vector2 desiredPosition = mousePosition;
        

        float distance = Vector2.Distance(desiredPosition,startPosition);
        if(distance > maxDrag){
            direction = desiredPosition - startPosition;
            direction.Normalize();
            desiredPosition = startPosition +(direction * maxDrag);
        }
        if(desiredPosition.x > startPosition.x  ){
            desiredPosition.x = startPosition.x;  
        }
        rb.position = desiredPosition;
    }

     void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            StartCoroutine(RestartDelay());
        }
        
    }

    IEnumerator RestartDelay() {
        yield return new WaitForSeconds(3f);
        // rb.position = startPosition;
        // rb.velocity = Vector2.zero;
        // rb.isKinematic = true;

        SceneManager.LoadScene(0);
    }
}
