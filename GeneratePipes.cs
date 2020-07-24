using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeneratePipes : MonoBehaviour{

    public GameObject previousPipe = null; // The last created pipe in our Pipes. Might be overhauled depending on how I implement Object Pooling.
    public Color allPipeColors;
    private int previousDirection = -50;
    private float spawnAndDeleteTime = 0.05f; // The delays after which the program shall spawn and delete pipes.
    private static Quaternion[] pipeRotations={
                            Quaternion.Euler(0f,0f,-90f), Quaternion.Euler(0f,0f,90f), Quaternion.Euler(90f,0f,0f), 
                            Quaternion.Euler(-90f,0f,0f), Quaternion.Euler(0f,180f,0f), Quaternion.Euler(180f,0f,0f)
    };
    string[] variables = {"x", "-x", "z", "-z", "y",  "-y",};
    bool morePipes = true;
    float elapsed = 0f;
    public int amountToPool;
    public Queue<int> poolIndexes = new Queue<int>(); // will be used to keep track of deletion. 
    void Update(){
        elapsed += Time.deltaTime;
        if (elapsed >= spawnAndDeleteTime && morePipes) {
            elapsed = elapsed % spawnAndDeleteTime;
            AttachPipe();
        }
        else if (elapsed >= spawnAndDeleteTime && !morePipes && poolIndexes.Count > 0) {
            elapsed = elapsed % spawnAndDeleteTime; 
            PipePooler.SharedInstance.RemovePooledObject(poolIndexes.Peek());
            poolIndexes.Dequeue();
            determineRestart();
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
            if(isAlreadyFilled(previousPipe.transform.position + previousPipe.transform.up*2f, .90f)){
                morePipes = false;
                return;
            }
            InstantiatePipePart("Sphere", previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(0f, 0f, 0f));
            Vector3 newLocation = newPosition( previousPipe,direction)*2;
            while(isAlreadyFilled( previousPipe.transform.position + newLocation, .90f)){ 
                direction = randomTransform();
                newLocation = newPosition( previousPipe,direction)*2;
            }
            InstantiatePipePart( "Pipe",previousPipe.transform.position + newLocation *0.5f, rotation(direction));
            previousDirection = direction;
        }
        else {
            if(isAlreadyFilled(previousPipe.transform.position + previousPipe.transform.up *2, .90f )){
                return;
            }
            InstantiatePipePart("Pipe",previousPipe.transform.position + previousPipe.transform.up *2, Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
        }
    }
    
    /*
        Instantiates a prefrab in a random location. 
    */
    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-15.0f, 15.0f), Random.Range(-5.0f, 5.0f) , Random.Range(-3.0f, 9.0f));
        while(isAlreadyFilled(spawnLocation,1.0f)){
            spawnLocation = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-4.5f, 5.0f) , 0);
        }
        allPipeColors = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        InstantiatePipePart( "Pipe",spawnLocation, rotation(randomTransform()));
    }

    /*
        * Gets the appropriate Quaternion based on index. 

        * @param rotationIndex - the index of which our desired rotation is at.
        * @return - The Quaternion containing our desired x,y,z rotation values. 
    */
    public Quaternion rotation(int rotationIndex){
        return pipeRotations[rotationIndex];
    }

    /*
        * Generates and returns a random direction to go to (integer).
    */
    public int randomTransform(){ 
        return  Random.Range(0,6);
    }

    /*
        * Determines whether the next pipe's direction will be different from the current pipe.
        * @return True, we next pipe will be in a different direction than the previous. Otherwise false, and the next pipe will be in the same direction.
    */
    public bool changesDirection(){
        int randomNumber = Random.Range(0,1000);
        if(randomNumber > 300){
            return true;
        }
        return false;
    }

    /*
        * Returns the new position a GameObject 

        * @GameObject previousObject - the previous object we are basing our next direction off of. (Maybe rename to newLocation?)
        * @randomTransform - the index of which we use to determine whether the direction will be right, forward, or up
        * @return - A Vector3 based on the previousObject, of which its position will be changed by one (x, y, or z).
    */
    public Vector3 newPosition(GameObject previousObject, int randomTransform){
        // a very good idea will be to make all transform options (right, foward, up) and storing them into an array.
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

    /*
        Determines whether a transform of an object is out of bounds.

        @param pipe - The transform (position, rotation, scale) of an object.
        @return - True: the object's position was out of bounds, otherwise false and the object remains in bounds.
    */
    public bool outOfBounds(Transform pipe){
        if(pipe.position.x <-25 || pipe.position.x > 30 || pipe.position.y <-10 || pipe.position.y > 10 || pipe.position.z <-6 || pipe.position.z > 24){
            morePipes = false;
            return true;
        }
        return false;
   }

    /*
        * Determines whether a location in the gameworld has an object instantiated in it.

        * @param direction - The location (x,y,z) we are checking if any objects are instantiated in.
        * @return - true: the location does have a pre-existing object on it, otherwise false and no objects exists at said location. 
    */ 
    public bool isAlreadyFilled(Vector3 direction , float radius){
        if(Physics.CheckSphere( direction, radius )) { // .9 is close enough to 1 (1.8 total) in distance and prevents major stuttering.                             
            return true;
        }
        return false;
    }
	
    /*
        * Instantiates a Pipe Object object in the game world.

        * @param tagName - the tag associated with which Pipe object we wan (Sphere, Pipe).
        * @param newLocation - the world-location of which the the object will be instantiated in.
        * @param newRotation - the rotation of the object will have (relative to itself).
    */
    public void InstantiatePipePart(string tagName , Vector3 newLocation , Quaternion newRotation){
        int index = PipePooler.SharedInstance.getPoolIndex(tagName);
        GameObject newPipe = PipePooler.SharedInstance.GetPooledObject(tagName, index);
        if(newPipe != null){
            newPipe.transform.position = newLocation;
            newPipe.transform.rotation = newRotation;
            newPipe.GetComponent<Renderer>().material.color = allPipeColors;
            previousPipe = newPipe;
            newPipe.SetActive(true);
            newPipe.name = tagName;
            if(index == -50){ // temporary fix without making a new class or new instance variable.
                poolIndexes.Enqueue(PipePooler.SharedInstance.pipeAndSpherePool.Count-1);
                return;
            } 
            poolIndexes.Enqueue(index);
        }
    }

    public void determineRestart(){
        if(poolIndexes.Count == 0){
            morePipes = true;
            previousPipe = null; // Null works for the program's purposes, but maybe sometihng else would work better.
        }
    }
}

