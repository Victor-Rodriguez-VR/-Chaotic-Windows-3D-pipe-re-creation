using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject spherePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.
    private GameObject dumbass; 
    private Queue<GameObject> pipes = new Queue<GameObject>();
    private Queue<GameObject> stoopid = new Queue<GameObject>();
    private int x = 0;
    string[] variables = {"x","-x","z","-z", "y","-y",};
    private static Quaternion[] variableRotations={
        Quaternion.Euler(0f,0f,-90f),
        Quaternion.Euler(0f,0f,90f),
        Quaternion.Euler(90f,0f,0f),
        Quaternion.Euler(-90f,0f,0f),
		Quaternion.Euler(0f,180f,0f), 
        Quaternion.Euler(180f,0f,0f)
    };



    // Start is called before the first frame update
    void Start(){
        dumbass  = Instantiate(pipePrefab, new Vector3(0,0,0), Quaternion.identity);
        
        pipes.Enqueue(dumbass);
    }


    // Update is called once per frame
    bool morePipes = true;
    float elapsed = 0f;
    void Update(){
        elapsed += Time.deltaTime;
        if (elapsed >= 0.3f && morePipes) {
            elapsed = elapsed % 0.3f;
            AttachPipe(dumbass);
        }
        else if (elapsed >= 0.3f && !morePipes && pipes.Count >=1) {
            elapsed = elapsed % 0.3f;
             Destroy(pipes.Dequeue());
        }
    }

    public void AttachPipe(GameObject pipe){
        Vector3 nextPipesPosition = new Vector3(-50,-50,-50);
 
        nextPipesPosition = pipe.transform.position + pipe.transform.up *2;
        if(objectExistsHere(nextPipesPosition)){
            Debug.Log("STOP");
            return;
        }
        GameObject nextPipe = Instantiate(pipePrefab, nextPipesPosition, Quaternion.identity);
        pipes.Enqueue(nextPipe);
        dumbass = nextPipe;
        x++; 
        if(x>=4){
            morePipes = false;
        }
    }
    
   public bool objectExistsHere(Vector3 direction ){
       if(Physics.CheckSphere( direction, 0.6f )){
           Debug.Log("IT WAS FILLED AT " +  direction );
           return true;
       }
       return false;
   }
}

