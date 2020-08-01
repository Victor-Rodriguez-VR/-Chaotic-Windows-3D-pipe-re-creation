using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GeneratePipes : MonoBehaviour{

    public GameObject previousPipe = null; // The previously created pipe. 
    public Renderer lastRender; // The render of the previously created pipe.
    public Color UniformPipeColor; 
    private int previousDirection = -50; // The direction of previousPipe. 
    private static Quaternion[] pipeRotations = { // The rotations of which we are employing.
        Quaternion.Euler(0f,0f,-90f), Quaternion.Euler(0f,0f,90f), Quaternion.Euler(90f,0f,0f), 
        Quaternion.Euler(-90f,0f,0f), Quaternion.Euler(0f,180f,0f), Quaternion.Euler(180f,0f,0f)
    };
    string[] variables = {"x", "-x", "z", "-z", "y",  "-y",};
    private float spawnAndDeleteTime = 0.0349f; // The delays after which the program shall spawn and delete pipes.
    bool morePipes = true; 
    float elapsed = 0f;
    public Queue<int> poolDeletionIndexes = new Queue<int>(); // Records the indexes and order of which pipes will be removed. 
    public Dictionary<string, float> itemHeights = new Dictionary<string, float>(); // The height of every object to be pooled. 
    
    void Start(){
        itemHeights.Add("Pipe",PipePooler.SharedInstance.GetPooledObject("Pipe", 0).GetComponent<MeshFilter>().mesh.bounds.extents.y *2);
        itemHeights.Add("Sphere", PipePooler.SharedInstance.GetPooledObject("Sphere", 10).GetComponent<MeshFilter>().mesh.bounds.extents.y *2);
    }
    void Update(){
        elapsed += Time.deltaTime;
        if (elapsed >= spawnAndDeleteTime && morePipes) {
            elapsed = elapsed % spawnAndDeleteTime;
            AttachPipe();
        }
        else if (elapsed >= spawnAndDeleteTime && !morePipes && poolDeletionIndexes.Count > 0) {
            elapsed = elapsed % spawnAndDeleteTime; 
            PipePooler.SharedInstance.RemovePooledObject(poolDeletionIndexes.Peek());
            poolDeletionIndexes.Dequeue();
            determineRestart();
        }
    }

    /*
        Appends a pipe to the previously created pipe (preiousPipe). 
    */
    public void AttachPipe(){
        int direction =  randomTransform();
        if(previousPipe == null){ // the only time the pipe will be null is on start or restart. 
            spawnAPrefabSomewhere();
        }
        else if(outOfBounds(previousPipe.transform)){ 
            return;
        }
        else if (changesDirection() && previousDirection != direction){
            if(isAlreadyFilled(previousPipe.transform.position + previousPipe.transform.up* itemHeights["Pipe"], itemHeights["Pipe"] /2.5f)){ 
                morePipes = false;
                return;
            }
            InstantiatePipePart("Sphere", previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(0f, 0f, 0f));
            Vector3 newLocation = newPosition( previousPipe,direction)* itemHeights["Pipe"];
            while(isAlreadyFilled( previousPipe.transform.position + newLocation,itemHeights["Pipe"]/2.5f)){ 
                direction = randomTransform();
                newLocation = newPosition( previousPipe,direction)* itemHeights["Pipe"];
            }
            InstantiatePipePart( "Pipe",previousPipe.transform.position + newLocation * (itemHeights["Pipe"]/4.0f ), rotation(direction));
            previousDirection = direction;
        }
        else {
            if(isAlreadyFilled(previousPipe.transform.position + previousPipe.transform.up * itemHeights["Pipe"], itemHeights["Pipe"] /2.5f )){
                return;
            }
            InstantiatePipePart("Pipe",previousPipe.transform.position + previousPipe.transform.up * itemHeights["Pipe"], Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
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
        UniformPipeColor = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
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
        if(randomNumber > 200){
            return true;
        }
        return false;
    }

    /*
        * Returns the new position of a GameObject.

        * @GameObject previousObject - the previous object we are basing our next direction off of. (Maybe rename to newLocation?)
        * @randomTransform - the index of which we use to determine whether the direction will be right, forward, or up
        * @return - A Vector3 based on the previousObject, of which its position will be changed by one (x, y, or z).
    */
    public Vector3 newPosition(GameObject previousObject, int randomTransform){
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
        if( !lastRender.isVisible || pipe.position.z <-20 || pipe.position.z > 26){
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
            newPipe.GetComponent<Renderer>().material.color = UniformPipeColor;
            previousPipe = newPipe;
            newPipe.SetActive(true);
            newPipe.name = tagName;
            lastRender = newPipe.GetComponent<Renderer>();
            if(index == -50){ // temporary fix without making a new class or new instance variable. A pointer may have worked better. 
                poolDeletionIndexes.Enqueue(PipePooler.SharedInstance.ObjectPool.Count-1);
                return;
            } 
            poolDeletionIndexes.Enqueue(index);

        }
    }

    /*
        * Determines whether more pipes should be created. 
    */
    public void determineRestart(){
        if(poolDeletionIndexes.Count == 0){
            morePipes = true;
            previousPipe = null; 
            lastRender = null;
        }
    }
}


