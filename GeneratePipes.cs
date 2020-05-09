using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject spherePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.
    int x = 0; // for testing


    // Start is called before the first frame update
    void Start(){
        continuePipe(spawnAPrefabSomewhere(), "");      
    }


    // Update is called once per frame
    void Update(){
        

    }


    public GameObject spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, Quaternion.Euler(0f,0f,0f));
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        return objec;
    }

    public void continuePipe(GameObject previousPipe, string previousDirection){ 
        string direction = randomTransform(previousDirection);
        Color parentColor = previousPipe.GetComponent<Renderer>().material.color;
        x++;
        if(x>3){
            return;
        }
        if(previousDirection == direction ){
            GameObject piper = Instantiate(pipePrefab,previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(previousPipe.transform.eulerAngles.x, previousPipe.transform.eulerAngles.y, previousPipe.transform.eulerAngles.z));
            piper.transform.SetParent(previousPipe.transform, true);
            piper.GetComponent<Renderer>().material.color = parentColor;
            continuePipe(piper, direction);
        }
        else{
            GameObject spherey = Instantiate(spherePrefab, previousPipe.transform.position + previousPipe.transform.up, Quaternion.Euler(0f, 0f, 0f));
            spherey.transform.SetParent(previousPipe.transform, true);
            spherey.GetComponent<Renderer>().material.color = parentColor;
            GameObject piper = Instantiate(pipePrefab, spherey.transform.position + newDirection(spherey, direction), idk(direction));
            piper.transform.SetParent(spherey.transform,true);
            piper.GetComponent<Renderer>().material.color = parentColor;
            continuePipe(piper, direction);
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

   public Quaternion idk(string dumb){
       // These rotations are nice and all, but they're somewhat limited.
       // Our spheres spawn nicely. But where's the fun in this??
       // Next I will make it so the engine rolls to do different rotations in x and y not just limited to 90 degree rotations.
       if(dumb == "x"){
            Debug.Log("x");
           return Quaternion.Euler(0f,0f,-90f);
       }
       if(dumb == "-x"){
           Debug.Log("-x");
           return Quaternion.Euler(0f,0f,90f);
       }
       if(dumb == "z"){
           Debug.Log("z");
           return Quaternion.Euler(90f,0f,0f);
       }
       if( dumb == "-z"){
           Debug.Log("-z");
           return Quaternion.Euler(-90f,0f,0f);
       }
       else if (dumb == "y"){
           Debug.Log("y");
           return Quaternion.Euler(0f,180f,0f);
       }
       else if (dumb == "-y"){
           Debug.Log("-y");
           return Quaternion.Euler(180f,0f,0f);
       }
       return Quaternion.Euler(0f,0f,0f);
   }
}
