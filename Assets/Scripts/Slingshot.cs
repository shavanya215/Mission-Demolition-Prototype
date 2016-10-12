using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
    public GameObject    prefabProjectile;
    public bool          _________________________________; 
   
    // fields set dynamically 
    public GameObject     launchPoint;
    public Vector3        launchPos;
    public GameObject     projectile;
    public bool           aimingMode;

	// Use this for initialization
    void Awake () {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

	void OnMouseEnter () {
        print("Slingshot : OnMouseEnter()");
        launchPoint.SetActive(true);
	}
	
    void OnMouseExit() {
        print("Slingshot : OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown() {
        //The player has pressed the mouse button while over Slingshot aimingMode = true; 
        aimingMode = true;
        // Instantiate a projectile 
        projectile = Instantiate(prefabProjectile) as GameObject;
        //Start it as the launchPoint
        projectile.transform.position = launchPos;
        //set it to iaKinematic for now 
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
