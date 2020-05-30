using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject spherePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.
<<<<<<< HEAD
    private GameObject dumbass; 
    private Queue<GameObject> pipes = new Queue<GameObject>();
    private Queue<GameObject> stoopid = new Queue<GameObject>();
=======
    private GameObject dumbass = null; 
    private Queue<GameObject> pipes = new Queue<GameObject>();
>>>>>>> Update README.md
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
<<<<<<< HEAD
        dumbass  = Instantiate(pipePrefab, new Vector3(0,0,0), Quaternion.identity);
        
        pipes.Enqueue(dumbass);
    }
=======
>>>>>>> Update README.md

    }

    // Update is called once per frame
    bool morePipes = true;
    float elapsed = 0f;
    void Update(){
        elapsed += Time.deltaTime;
        if (elapsed >= 0.3f && morePipes) {
            elapsed = elapsed % 0.3f;
<<<<<<< HEAD
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
=======
            AttachPipe();
        }
        else if (elapsed >= 0.3f && !morePipes && pipes.Count >=1) {
            elapsed = elapsed % 0.3f;
             Destroy(pipes.Dequeue());
        }
    }

    public void AttachPipe(){
        if(dumbass == null){  
            spawnAPrefabSomewhere();
        }
        else if(outOfBounds(dumbass.transform) && x >1){
            return;
        }
        else{
            Vector3 nextPipesPosition = new Vector3(-50,-50,-50);
            nextPipesPosition = dumbass.transform.position + dumbass.transform.up *2;
            if(objectExistsHere(nextPipesPosition)){
                Debug.Log("STOP");
                return;
            }
            GameObject nextPipe = Instantiate(pipePrefab, nextPipesPosition, Quaternion.Euler(dumbass.transform.eulerAngles.x, dumbass.transform.eulerAngles.y, dumbass.transform.eulerAngles.z));
            nextPipe.GetComponent<Renderer>().material.color = dumbass.GetComponent<Renderer>().material.color;
            pipes.Enqueue(nextPipe);
            dumbass = nextPipe;
            x++; 
            if(x>=4){
                morePipes = false;
            }
        }
    }
    
   public bool objectExistsHere(Vector3 direction ){
       if(Physics.CheckSphere( direction, 0.6f )){
           Debug.Log("IT WAS FILLED AT " +  direction );
           return true;
       }
       return false;
   }


    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, rotation(randomTransform()));
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        dumbass = objec;
        pipes.Enqueue(objec);
    }

    public Quaternion rotation(int variable){
        return variableRotations[variable];
    }

    public int randomTransform(){
        int idk = Random.Range(0,6);
        return idk;
    }

    public bool changesDirection(){
        int randomNumber = Random.Range(0,1000);
        if(randomNumber > 400){
            return true;
        }
       return false;
    }

    public Vector3 newDirection(int randomTransform){
        int rotation180 = 1;
        if(randomTransform % 2 == 1 ){
            rotation180 = -1;
        }
        if(randomTransform < 2){
            return dumbass.transform.right *  rotation180;
        }
        else if (randomTransform < 4 ){
            return dumbass.transform.forward *   rotation180; 
        }
        else{
            return dumbass.transform.up *  rotation180;
        }

    }    
    public bool outOfBounds(Transform pipe){
        if(pipe.position.x <-10 || pipe.position.x > 10 || pipe.position.y <-10 || pipe.position.y > 10 || pipe.position.z <-10 || pipe.position.z > 10){
            Debug.Log("I stopped");
            return true;
        }
        return false;
>>>>>>> Update README.md
   }
}

// c and p code
// make it work with a pointer like thing
// make a new script to manage that code
// 