using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject spherePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.
    int x = 0; // for testing


    // Start is called before the first frame update
    void Start(){
        GameObject o = Instantiate(pipePrefab, new Vector3(1,0,0), Quaternion.Euler(0f, 0f, 0f));
        o.GetComponent<Renderer>().material.color =  Color.red;
        continuePipe(o);      
        //z.transform.position = z.transform.up;

        // Go based on x, y, ,z/
    }


    // Update is called once per frame
    void Update(){
        

    }


    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, Quaternion.Euler(0f,0f,0f));
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
    }




    public void continuePipe(GameObject previousPipe){
        x++;
        if(x>3){
            return;
        }

        GameObject spherey = Instantiate(spherePrefab, previousPipe.transform.position + newDirection(previousPipe), Quaternion.Euler(0f, 0f, 0f));
        spherey.transform.SetParent(previousPipe.transform, true);
        GameObject piper = Instantiate(pipePrefab, spherey.transform.position + newDirection(spherey), Quaternion.Euler(0f,0f,0f));
        piper.transform.SetParent(spherey.transform,true );
        Debug.Log(randomTransform());
        continuePipe(piper);
    }



    public string randomTransform(){
        int randomNumber = Random.Range(0,99);
        if(randomNumber < 33 ){
            return "x";
        }
        else if(randomNumber <66 ){
            return "y";
        }
        else{
            return "z";
        }
    }

    public Vector3 newDirection(GameObject previousObject){
        string newDirection = randomTransform();
        if(newDirection == "x"){
            return previousObject.transform.right;
        }
        else if (newDirection == "y"){

            return previousObject.transform.up;
        }
        else{
            return previousObject.transform.forward;
        }

    }
   

   // Make a function that takes in a Vector3 (newDirection) and returns a new Quaternion / orientation. 
   // You'd use this in ContinuePipe right after making a new Sphere you'd want to know where it went. Based off of where it went it'd be
   // easy to rotate to make the parts connect.
}
