using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null;
    public GameObject turnPrefab = null;
    GameObject[]  pipeParts;





    private static Quaternion[] childOrientations ={
        Quaternion.identity,
        Quaternion.Euler(0f,0f,-90f),
        Quaternion.Euler(0f,0f,90f),
        Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f)
    };
    // Start is called before the first frame update
    void Start(){
        spawnAPrefabSomewhere();

        GameObject objec = Instantiate(pipePrefab, new Vector3(0,0,0), Quaternion.Euler(0, 0, 0));

    }

    // Update is called once per frame
    void Update(){
        

    }


    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, Quaternion.identity);
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        continuePipe(objec);
    }



    public void continuePipe(GameObject parentPipe){
        Transform parentinTransform = parentPipe.transform;
        Vector3 newLocation = new Vector3(parentinTransform.position.x, parentinTransform.position.y+1,parentinTransform.position.z);
        GameObject kid = Instantiate(pipePrefab, newLocation,Quaternion.identity);
        kid.GetComponent<Renderer>().material.color = parentPipe.GetComponent<Renderer>().material.color;
        kid.transform.parent = parentinTransform;
        kid.transform.position = newLocation;


    }
}
