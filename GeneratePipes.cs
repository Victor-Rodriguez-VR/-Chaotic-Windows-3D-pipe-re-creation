using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab;

    // Start is called before the first frame update
    void Start(){
        GameObject initalPipe = Instantiate(pipePrefab,new Vector3(0,0,0), Quaternion.identity);
        initalPipe.GetComponent<MeshRenderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
    }

    // Update is called once per frame
    void Update(){
        

    }
}
