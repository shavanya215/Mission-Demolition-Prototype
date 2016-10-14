using UnityEngine;
using System.Collections;

public class CloudCrafter : MonoBehaviour {


    //fields set in the unity Inspector pane
    public int numClouds =       40;     // The # ofr clouds to make 
    public GameObject[]          cloudPrefabs; // the prefabs for the clouds 
    public Vector3               cloudPosMin; //Min position of each cloud 
    public Vector3               cloudPosMax; // Max position of each cloud 
    public float                 cloudScaleMin = 1; //Min Scale of each cloud 
    public float                 cloudScaleMax = 5; // Max scale of each cloud 
    public float                 cloudSpeedMult = 0.5f; // Adjusts speed of clouds 

    public bool                  ___________________________;

    //fields set dynamically 
    public GameObject[]          cloudInstances;

    void Awake() {
        //Make an arrary large enough to hold all the Cloud_instances
        cloudInstances = new GameObject[numClouds];
        //Find the CloudAnchor parent GameObeject 
        GameObject anchor = GameObject.Find("CloudAnchor");
        //iterate through and make Cloud_s
        GameObject cloud;
        for (int i=0; i<numClouds; i++) {
            //Pick an int between 0 and CloudPrefabs.Length-1
            //Random .Range will not eveer pick as high as the top number 
            int prefabNum = Random.Range( 0, cloudPrefabs.Length);
            //Make and instance
            cloud = Instantiate( cloudPrefabs[prefabNum]) as GameObject;
            //Position cloud 
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMax.y, cloudPosMax.y);
            //scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            //smaller clouds (with smaller scaleU) should be nearer to the ground
            cPos.z = 100 - 90 * scaleU;
            //Apple these transforms to the cloud 
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //Make cloud a child of the anchor
            cloud.transform.parent = anchor.transform;
            //Add the cloud to cloudInstances
            cloudInstances[i] = cloud;

        }

    }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Interate over each clous that was created 
        foreach (GameObject cloud in cloudInstances) {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //Move larger clouds faster 
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult; 
            //if cloud has moved too far to the left 
            if (cPos.x <= cloudPosMin.x) {
                // move it to the far right 
                cPos.x = cloudPosMax.x;
            }
            //Apply the new position to cloud
            cloud.transform.position = cPos;
        }
	
	}
}
