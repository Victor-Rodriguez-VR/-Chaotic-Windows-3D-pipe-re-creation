using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject spherePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    private GameObject dumbass; 
    private Queue<GameObject> pipes = new Queue<GameObject>();
    private Queue<GameObject> stoopid = new Queue<GameObject>();
=======
    private GameObject dumbass = null; 
    private Queue<GameObject> pipes = new Queue<GameObject>();
>>>>>>> Update README.md
    private int x = 0;
=======
    private GameObject previousPipe = null; 
    private Queue<GameObject> pipes = new Queue<GameObject>();
    private int x = 0;      // change to boolean soon (tm)
    private int previousDirection = -50;
>>>>>>> Update README.md
=======
    private GameObject previousPipe = null; 
    private Queue<GameObject> pipes = new Queue<GameObject>();
    private int x = 0;      // change to boolean soon (tm)
    private int previousDirection = -50;
>>>>>>> Update README.md
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
<<<<<<< HEAD
<<<<<<< HEAD
        dumbass  = Instantiate(pipePrefab, new Vector3(0,0,0), Quaternion.identity);
        
        pipes.Enqueue(dumbass);
    }
=======
>>>>>>> Update README.md
=======
>>>>>>> Update README.md
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
<<<<<<< HEAD
<<<<<<< HEAD
            AttachPipe(dumbass);
=======
            AttachPipe();
>>>>>>> Update README.md
        }
        else if (elapsed >= 0.3f && !morePipes && pipes.Count >=1) {
            elapsed = elapsed % 0.3f;
             Destroy(pipes.Dequeue());
        }
    }

    public void AttachPipe(){
        if(previousPipe == null){  
            spawnAPrefabSomewhere();
        }
        else if(outOfBounds(previousPipe.transform) && x >1){
            return;
        }
        else{
            int direction = randomTransform();
            Color parentColor = previousPipe.GetComponent<Renderer>().material.color;
            if(changesDirection() && previousDirection != direction){ 
                    GameObject spherey = Instantiate(spherePrefab, previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(0f, 0f, 0f));
                    pipes.Enqueue(spherey);
                    spherey.GetComponent<Renderer>().material.color = parentColor;
                    while(alreadyFilled(spherey.transform.position + newDirection(spherey, direction)*2)){ // what about no rotations?
                        direction = randomTransform();
                    }
                    GameObject newPipe = Instantiate(pipePrefab, spherey.transform.position + newDirection(spherey, direction), rotation(direction));
                    pipes.Enqueue(newPipe); // maybe move outside of else's. depends on code flow when streamlined.
                    newPipe.GetComponent<Renderer>().material.color = parentColor;
                    previousPipe = newPipe;
                    previousDirection = direction;
            }
            else{
                if(alreadyFilled(previousPipe.transform.position + previousPipe.transform.up *2 )){
                    Debug.Log("idk some straight issues");
                    return;
                }
                GameObject newPipe = Instantiate(pipePrefab,previousPipe.transform.position + previousPipe.transform.up *2, Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
                pipes.Enqueue(newPipe);
                newPipe.GetComponent<Renderer>().material.color = parentColor;
                previousPipe = newPipe;
            }
            x++; // Will remove later
        }
    }
    
   public bool objectExistsHere(Vector3 direction ){
       if(Physics.CheckSphere( direction, 0.6f )){
           Debug.Log("IT WAS FILLED AT " +  direction );
           return true;
       }
       return false;
=======
=======
>>>>>>> Update README.md
            AttachPipe();
        }
        else if (elapsed >= 0.3f && !morePipes && pipes.Count >=1) {
            elapsed = elapsed % 0.3f;
             Destroy(pipes.Dequeue());
        }
    }

    public void AttachPipe(){
        if(previousPipe == null){  
            spawnAPrefabSomewhere();
        }
        else if(outOfBounds(previousPipe.transform) && x >1){
            return;
        }
        else{
            int direction = randomTransform();
            Color parentColor = previousPipe.GetComponent<Renderer>().material.color;
            if(changesDirection() && previousDirection != direction){ 
                    GameObject spherey = Instantiate(spherePrefab, previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(0f, 0f, 0f));
                    pipes.Enqueue(spherey);
                    spherey.GetComponent<Renderer>().material.color = parentColor;
                    while(alreadyFilled(spherey.transform.position + newDirection(spherey, direction)*2)){ // what about no rotations?
                        direction = randomTransform();
                    }
                    GameObject newPipe = Instantiate(pipePrefab, spherey.transform.position + newDirection(spherey, direction), rotation(direction));
                    pipes.Enqueue(newPipe); // maybe move outside of else's. depends on code flow when streamlined.
                    newPipe.GetComponent<Renderer>().material.color = parentColor;
                    previousPipe = newPipe;
                    previousDirection = direction;
            }
            else{
                if(alreadyFilled(previousPipe.transform.position + previousPipe.transform.up *2 )){
                    Debug.Log("idk some straight issues");
                    return;
                }
                GameObject newPipe = Instantiate(pipePrefab,previousPipe.transform.position + previousPipe.transform.up *2, Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
                pipes.Enqueue(newPipe);
                newPipe.GetComponent<Renderer>().material.color = parentColor;
                previousPipe = newPipe;
            }
            x++; // Will remove later
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
        previousPipe = objec;
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
        // make this shorter
        int randomNumber = Random.Range(0,1000);
        if(randomNumber > 400){
            return true;
        }
       return false;
    }

    public Vector3 newDirection(GameObject previousObject, int randomTransform){
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
            Debug.Log("I went out of bounds");
            morePipes = false;
            return true;
        }
        return false;
   }

<<<<<<< HEAD
<<<<<<< HEAD
// c and p code
// make it work with a pointer like thing
// make a new script to manage that code
// 
=======

    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, rotation(randomTransform()));
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        previousPipe = objec;
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
        // make this shorter
        int randomNumber = Random.Range(0,1000);
        if(randomNumber > 400){
            return true;
        }
       return false;
    }

    public Vector3 newDirection(GameObject previousObject, int randomTransform){
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
            Debug.Log("I went out of bounds");
            morePipes = false;
            return true;
        }
        return false;
   }

=======
>>>>>>> Update README.md
   // Investigate this function! It is not determining whether two objects are **colliding**!!!!
    public bool alreadyFilled(Vector3 direction ){
        if(Physics.CheckSphere( direction, 0.6f )){
            Debug.Log("IT WAS FILLED AT " +  direction );
            return true;
        }
        return false;
    }
<<<<<<< HEAD
}
>>>>>>> Update README.md
=======
    // test test test
}
>>>>>>> Update README.md
