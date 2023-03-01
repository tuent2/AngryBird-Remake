using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPosition;
    public Transform center;
    public Transform idlePosition;
    public Vector3 currentPossition;
    public float maxLength;
    public float bottomBoundary;

    public GameObject birdFrefabs;
    public float birdPositionOffset;

    public float force;
    Rigidbody2D bird;
    Collider2D birdCollider;
     bool isMouseDown ;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPosition[0].position);
        lineRenderers[1].SetPosition(0, stripPosition[1].position);
        CreateBird();
    }

    void CreateBird(){
        bird = Instantiate(birdFrefabs).GetComponent<Rigidbody2D>();
        birdCollider = bird.GetComponent<Collider2D>();
        birdCollider.enabled = false;
        bird.isKinematic = true;
        ResetTrips();
    }
    // Update is called once per frame
    void Update()
    {
        if(isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPossition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPossition = center.position+Vector3.ClampMagnitude(currentPossition-center.position,maxLength);
            currentPossition = ClampBoundary(currentPossition);
            SetTrips(currentPossition);
            if(birdCollider){
                birdCollider.enabled = true;
            }
        }else
        {
            ResetTrips();
        }
    }

    private void OnMouseDown() {
        isMouseDown = true;
    }

    private void OnMouseUp() {
        isMouseDown = false;
        Shot();
        currentPossition = idlePosition.position;
    }

    void ResetTrips(){
        currentPossition = idlePosition.position;
        SetTrips(currentPossition);
    }

    void SetTrips(Vector3 position){
        lineRenderers[0].SetPosition(1,position);
        lineRenderers[1].SetPosition(1,position);

        Vector3 dir = position - center.position;
        bird.transform.position =position+dir.normalized*birdPositionOffset;
        bird.transform.right= -dir.normalized; 
    }

    Vector3 ClampBoundary(Vector3 vector){
        vector.y = Mathf.Clamp(vector.y,bottomBoundary,1000);
        return vector;
    }

    void Shot(){
        bird.isKinematic = false;
        Vector3 birdForce = (currentPossition - center.position) * force * -1;
        bird.velocity = birdForce;

        bird = null;
        birdCollider = null;
        Invoke("CreateBird",2);
    }
}
