using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    // A static field accessible by code anywhere 
    static public bool        goalMet = false; 


	// Use this for initialization
	void OnTriggerEnter( Collider other) {
        //When the trigger is hit by something
        //cheeck to see if it's a projectile 
        if ( other.gameObject.tag == "Projectile") {
            //if so, set goalMet to true 
            Goal.goalMet = true;

            // Also set the alpha of the color to higher opactiy 
            Color c = GetComponent<Renderer>().material.color;
            c.a = 0.9f;
            GetComponent<Renderer>().material.color = c;


        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
