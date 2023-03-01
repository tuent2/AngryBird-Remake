using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monter : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Box"){
            gameObject.SetActive(false);
        }
    }
}
