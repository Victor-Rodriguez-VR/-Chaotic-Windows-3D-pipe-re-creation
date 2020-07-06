using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeneratePipes : MonoBehaviour{
    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject spherePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.
    private GameObject previousPipe = null; // The last created pipe in our Pipes. Might be overhauled depending on how I implement Object Pooling.
    private List<GameObject> pipes = new List<GameObject>(); // Our Collection of pipes. Will DEFINITELY be overhauled. (maybe a list<Gameobject>)?
    private Color allPipeColors;
    private int x = 0;      // change to boolean soon (tm)
    private int previousDirection = -50;
    private float spawnAndDeleteTime = 0.05f; // The delays after which the program shall spawn and delete pipes.
    private static Quaternion[] variableRotations={
                            Quaternion.Euler(0f,0f,-90f), Quaternion.Euler(0f,0f,90f), Quaternion.Euler(90f,0f,0f), 
                            Quaternion.Euler(-90f,0f,0f), Quaternion.Euler(0f,180f,0f), Quaternion.Euler(180f,0f,0f)
    };
    string[] variables = {         "x",                     "-x",                       "z",
                                   "-z",                    "y",                        "-y",
                        };
    bool morePipes = true;
    float elapsed = 0f;
    int obama = 0;
    public int amountToPool;
    void Start() {
        pipes = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++){
            GameObject pipe = (GameObject) Instantiate(pipePrefab);
            pipe.SetActive(false);
            pipes.Add(pipe);
        }
    }


    void Update(){
        elapsed += Time.deltaTime;
        if (elapsed >= spawnAndDeleteTime && morePipes) {
            elapsed = elapsed % spawnAndDeleteTime;
            AttachPipe();
        }
        else if (elapsed >= spawnAndDeleteTime && !morePipes && obama < pipes.Count) {
            elapsed = elapsed % spawnAndDeleteTime; 
            pipes[obama].SetActive(false);
            obama+=1;
        }
    }

    /*
        Appends a pipe to the previously created pipe (preiousPipe).
    */
    public void AttachPipe(){
        int direction =  randomTransform();
        if(previousPipe == null){  
            spawnAPrefabSomewhere();
        }
        else if(outOfBounds(previousPipe.transform) && morePipes == false){
            morePipes = false;
            return;
        }
        else if (changesDirection() && previousDirection != direction){
            if(alreadyFilled(previousPipe.transform.position + previousPipe.transform.up*2f)){
                morePipes = false;
                return;
            }
            GameObject spherey = Instantiate(spherePrefab, previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(0f, 0f, 0f));
            pipes.Add(spherey);
            spherey.GetComponent<Renderer>().material.color = allPipeColors;
            Vector3 newLocation = newDirection(spherey,direction)*2;
            while(alreadyFilled(spherey.transform.position + newLocation)){ // Think of a replacement for while
                                                                            // or find a way to optimize associated functions.
                direction = randomTransform();
                newLocation = newDirection(spherey,direction)*2;
            }
            InstantiatePipe(spherey.transform.position + newLocation *0.5f, rotation(direction));
            previousDirection = direction;
        }
        else {
            if(alreadyFilled(previousPipe.transform.position + previousPipe.transform.up *2 )){
                return;
            }
            InstantiatePipe(previousPipe.transform.position + previousPipe.transform.up *2, Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
            }
            x++; 
        }

    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.5f, 5.0f) , 0);
        allPipeColors = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        InstantiatePipe(spawnLocation, rotation(randomTransform()));
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

    public Vector3 newDirection(GameObject previousObject, int randomTransform){
        // a very good idea will be to make all transform options (right, foward, up) and storing them into an array for a slight
        //                                                                                              performance and code cleanup.
        int rotation180 = 1;
        if(randomTransform % 2 == 1 ){
            rotation180 = -1;
        }
        if(randomTransform < 2){
            return previousObject.transform.right *  rotation180;
        }
        else if (randomTransform < 4 ){
            return previousObject.transform.forward *   rotation180; 
        }
        else{
            return previousObject.transform.up *  rotation180;
        }

    }    

    public bool outOfBounds(Transform pipe){
        if(pipe.position.x <-10 || pipe.position.x > 10 || pipe.position.y <-10 || pipe.position.y > 10 || pipe.position.z <-10 || pipe.position.z > 10){
            morePipes = false;
            return true;
        }
        return false;
   }

    public bool alreadyFilled(Vector3 direction ){
        if(Physics.CheckSphere( direction, .90f )) { // .9 is close enough to 1 (1.8 total) in distance and prevents major stuttering.                             
            return true;
        }
        return false;
    }
	
    // depending on Object Pooling this function will change next commit. 
    public void InstantiatePipe(Vector3 newLocation , Quaternion newRotation){
        GameObject newPipe = GetPooledPipe();
        if(newPipe != null){
            newPipe.transform.position = newLocation;
            newPipe.transform.rotation = newRotation;
            newPipe.GetComponent<Renderer>().material.color = allPipeColors;
            previousPipe = newPipe;
            newPipe.SetActive(true);
            newPipe.name = "PipePrefab";
        }
    }
    
    public GameObject GetPooledPipe(){
        for(int i =0; i < pipes.Count; i++){
            if(!pipes[i].activeInHierarchy){
                return pipes[i];
            }
        }
        return null;
    }
}

