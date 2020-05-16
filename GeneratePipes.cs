using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject spherePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.


    // Start is called before the first frame update
    void Start(){
        continuePipe(spawnAPrefabSomewhere(), "y");      
    }


    // Update is called once per frame
    void Update(){

    }

    public GameObject spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, Quaternion.Euler(0f,180f,0f));
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        return objec;
    }

    public void continuePipe(GameObject previousPipe, string previousDirection){ 
        if(outOfBounds(previousPipe.transform)){
            return;
        }
        string direction = randomTransform(previousDirection);
        Color parentColor = previousPipe.GetComponent<Renderer>().material.color;
        if(changesDirection()){
            if(previousDirection == direction ){
                GameObject piper = Instantiate(pipePrefab,previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
                piper.transform.SetParent(previousPipe.transform, true);
                piper.GetComponent<Renderer>().material.color = parentColor;
                continuePipe(piper, previousDirection);
            }
            else{
                GameObject spherey = Instantiate(spherePrefab, previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(0f, 0f, 0f));
                spherey.transform.SetParent(previousPipe.transform, true);
                spherey.GetComponent<Renderer>().material.color = parentColor;
                GameObject piper = Instantiate(pipePrefab, spherey.transform.position + newDirection(spherey, direction), rotation(direction));
                piper.transform.SetParent(spherey.transform,true);
                piper.GetComponent<Renderer>().material.color = parentColor;
                continuePipe(piper, direction);
            }
        }
        else{
            GameObject piper = Instantiate(pipePrefab,previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
            piper.transform.SetParent(previousPipe.transform, true);
            piper.GetComponent<Renderer>().material.color = parentColor;
            continuePipe(piper, previousDirection);
        }

    }
    /*
        Returns a string of a direction (x or y or z)

        @param previousDirection - 
    */
    public string randomTransform(string previousDirection){
        int randomNumber = Random.Range(0,198);
        string transformAxis = "";
        if(randomNumber < 33 ){
            transformAxis =  "x";
            if(previousDirection == "-x"){
                return randomTransform(previousDirection);
            }
        }
        else if(randomNumber < 66){
            transformAxis = "-x";
            if(previousDirection == "x"){
                return randomTransform(previousDirection);
            }
        }
        else if(randomNumber <99 ){
            transformAxis = "y";
            if(previousDirection == "-y"){
                return randomTransform(previousDirection);
            }
        }
        else if(randomNumber < 132){
            transformAxis = "-y";
            if(previousDirection == "y"){
                return randomTransform(previousDirection);
            }
        }
        else if(randomNumber < 165){
            transformAxis = "z";
            if(previousDirection == "-z"){
                return randomTransform(previousDirection);
            }
        }
        else{
            transformAxis = "-z";
            if(previousDirection == "z"){
                return randomTransform(previousDirection);
            }
        }
        return transformAxis;
        
    }

    /*
        Determines a new direction.
        @param previousObject - the previous object we'd like to get a new direction from.
        @param randomTransform - a string (x or y or z) that tell us which direction our object will go.

        @return a Vector3 that determines in which direction the pipe will go.
    */
    public Vector3 newDirection(GameObject previousObject, string randomTransform){
        if(randomTransform == "x"){
            return previousObject.transform.right;
        }
        else if (randomTransform == "-x"){
            return previousObject.transform.right *-1;
        }
        else if (randomTransform == "y"){
            return previousObject.transform.up;
        }
        else if (randomTransform == "-y"){
            return previousObject.transform.up*-1; 
        }
        else if (randomTransform == "-z"){
            return previousObject.transform.forward *-1;
        }
        else{
            return previousObject.transform.forward;
        }

    }
   

   // Make a function that takes in a Vector3 (newDirection) and returns a new Quaternion / orientation. 
   // You'd use this in ContinuePipe right after making a new Sphere you'd want to know where it went. Based off of where it went it'd be
   // easy to rotate to make the parts connect.


    /*
        Returns eulerAngles (x,y,z) in Quaternion form based on axis of rotation.


        @param rotationAxis - the desired axis of rotation (ex: from our randomTransform we got x so we are going to rotate around x).
    */
   public Quaternion rotation(string rotationAxis){
       // These rotations are nice and all, but they're somewhat limited.
       // Our spheres spawn nicely. But where's the fun in this??
       // Next I will make it so the engine rolls to do different rotations in x and y not just limited to 90 degree rotations.
       if(rotationAxis == "x"){
            Debug.Log("x");
           return Quaternion.Euler(0f,0f,-90f);
       }
       if(rotationAxis == "-x"){
           Debug.Log("-x");
           return Quaternion.Euler(0f,0f,90f);
       }
       if(rotationAxis == "z"){
           Debug.Log("z");
           return Quaternion.Euler(90f,0f,0f);
       }
       if( rotationAxis == "-z"){
           Debug.Log("-z");
           return Quaternion.Euler(-90f,0f,0f);
       }
       else if (rotationAxis == "y"){
           Debug.Log("y");
           return Quaternion.Euler(0f,180f,0f);
       }
       else if (rotationAxis == "-y"){
           Debug.Log("-y");
           return Quaternion.Euler(180f,0f,0f);
       }
       Debug.Log("I go here");
       return Quaternion.Euler(0f,0f,0f);
   }

   public bool changesDirection(){
       int randomNumber = Random.Range(0,1000);
       if(randomNumber > 601){
           return true;
       }
       return false;
   }

   public bool outOfBounds(Transform pipe){
       if(pipe.position.x <-10 || pipe.position.x > 10 || pipe.position.y <-10 || pipe.position.y > 10 || pipe.position.z <-10 || pipe.position.z > 10){
           Debug.Log("I stopped");
           return true;
       }
       return false;
   }

   public bool alreadyFilled(GameObject pipe, Vector3 direction ){
       if(Physics.CheckSphere(pipe.transform.position + direction, 0.5f )){
           Debug.Log("IT WAS FILLED AT " + pipe.transform.position + direction )
           return true;
       }
       return false;
   }
}

