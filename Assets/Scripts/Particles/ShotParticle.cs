using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotParticle : MonoBehaviour {

    public Transform particlePrefab;
    Transform particle;

    public GameObject currentParticle;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            Debug.Log("Click");

            currentParticle.GetComponentInChildren<ParticleSystem>().Emit(1);
        }
	}
}
