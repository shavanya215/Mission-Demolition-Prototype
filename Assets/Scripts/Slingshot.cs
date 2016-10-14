using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
    static public        Slingshot S;
    public GameObject    prefabProjectile;
    public float         velocityMult = 4f;
    public bool          _________________________________; 
   
    // fields set dynamically 
    public GameObject     launchPoint;
    public Vector3        launchPos;
    public GameObject     projectile;
    public bool           aimingMode;

	// Use this for initialization
    void Awake () {
        // Set this Slingshot singleton S
        S = this;
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
        // if Slingshot is not in aimingMode, don't run this cose 
        if (!aimingMode) return;
        //Get the current Mouse position in 2d screen coordinates 
        Vector3 mousePos2D = Input.mousePosition;
        // Convert the mouse position to 3D world coordinates 
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //find the delta from the launchpos to the mousepos3d
        Vector3 mouseDelta = mousePos3D-launchPos;
        //Limit mouseDelta to the radius of the slingshot sphereCollider; 
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        //Move the projectile to this new position 
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if ( Input.GetMouseButtonUp(0) ) {
            // The mouse has been released 
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCam.S.poi = projectile;
            projectile = null;

        }


        }

	}

