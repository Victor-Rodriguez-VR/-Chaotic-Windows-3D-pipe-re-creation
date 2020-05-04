using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject turnPrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.


    /*
        All possible prientations our pipes can face.
    */
    private static Quaternion[] childOrientations ={
        Quaternion.Euler(90f, 0f, 0f), // +x direction
		Quaternion.Euler(-90f, 0f, 0f), // -x direction
        Quaternion.Euler(0f, 90f, 0f), // +y direction
		Quaternion.Euler(0f, -90f, 0f), // -y direction
        Quaternion.Euler(0f,0f,90f), // +z direction 
        Quaternion.Euler(0f,0f,-90f), // -z direction
    };



    // Start is called before the first frame update
    void Start(){
        spawnAPrefabSomewhere();
        /*GameObject objec = Instantiate(pipePrefab, new Vector3(0,0,0), Quaternion.Euler(90f, 0, 0));
        continuePipe(objec); */
        GameObject o = Instantiate(pipePrefab, new Vector3(3,0,0), Quaternion.Euler(90, 0, 0f));
        o.GetComponent<Renderer>().material.color =  Color.red;
        continuePipe(o,true);
    }

    // Update is called once per frame
    void Update(){
        

    }


    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, Quaternion.Euler(90,0f,0f));
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        continuePipe(objec);
    }

    /*
        Continues to create pipes after one has been placed.

        @ param parentPipe - the original pipe you'd like to expand.
    */
    public void continuePipe(GameObject parentPipe){
        Transform parentTransform = parentPipe.transform; // Position, scale, and rotation of our parent pipe prefab.

        Vector3 direction = correctDirection(parentPipe);
        /*
            Here will be an else if, which will determine whether or not our pipe will go in a random direction. 
            If it does, we will change the the Quaternion obj to be that of our correct object rotation.

        */
        GameObject kid = Instantiate(pipePrefab, parentTransform.position + direction , parentTransform.rotation );
        kid.GetComponent<Renderer>().material.color = parentPipe.GetComponent<Renderer>().material.color;
        kid.transform.parent = parentTransform;
    }



    /*
        Continues to create pipes after one has been placed.
        Its just like the same function, but this one is for extensive testing.

        @ param parentPipe - the original pipe you'd like to expand.
        @ param alwaysTurns - boolean to test bugs when the pipes turn. 
    */
    public void continuePipe(GameObject parentPipe, bool alwaysTurns){


        
        Transform parentTransform = parentPipe.transform; // Position, scale, and rotation of our parent pipe prefab.

        Vector3 direction = correctDirection(parentPipe);


        /*
            Here will be an else if, which will determine whether or not our pipe will go in a random direction. 
            If it does, we will change the the Quaternion obj to be that of our correct object rotation.

        */


        // First we must spawn the sphere and make it look good
        if(alwaysTurns){
            Quaternion temp = childOrientations[Random.Range(0,childOrientations.Length)];

            GameObject sphere = Instantiate(turnPrefab, parentTransform.position + direction, temp);
            Vector3 test = turnAround(parentPipe, temp);
            if(test != Vector3.zero){
                sphere.transform.eulerAngles = test;
            }


            Vector3 newDirection = correctDirection(sphere);
            GameObject next = Instantiate(pipePrefab, sphere.transform.position + newDirection, sphere.transform.rotation);

            sphere.GetComponent<Renderer>().material.color = parentPipe.GetComponent<Renderer>().material.color;
            next.GetComponent<Renderer>().material.color = sphere.GetComponent<Renderer>().material.color;
        }
        else{
            GameObject kid = Instantiate(pipePrefab, parentTransform.position + direction , parentTransform.rotation );
            kid.GetComponent<Renderer>().material.color = parentPipe.GetComponent<Renderer>().material.color;
            kid.transform.parent = parentTransform;
        }
    }







    /*
        Determines the correct direction for a newly inserted pipe.
    */
    public Vector3 correctDirection(GameObject parent){
        Quaternion parentOrientation = parent.transform.rotation;
        if( parentOrientation.x != 0f){
            if(parentOrientation.x <0f){
                return new Vector3(0f,0f,1f);
            }
            return new Vector3(0f,0f,-1f);
        }
        else if(parentOrientation.y !=0f){
            if(parentOrientation.y <0f){
                return new Vector3(0f,-1f,0f);
            }
            return new Vector3(0f,1f,0f);
        }
        else if(parentOrientation.z != 0f){
            if(parentOrientation.z <0f){
                return new Vector3(-1f,0f,0f);
            }
            return new Vector3(1f,0f,0f);
        }
        else{
            return new Vector3(0f,1f,0f);
        }
    }

    /*
        Determines whether or not the pipe will take a turn.
    */
    public bool randomDirectionChance(){
        int directionIndex = Random.Range(0,100);
        return directionIndex <=50;
    }

    /*
        This function prevents the pipe from rotate into itself. Admittedly I don't like how brute force-y it is, but I until I figure out 
        another vaiable solution this will have to suffice.

        @param pipeBeforeSphere - the pipe whose orientation we do not want to conflict with.
        @param angle - the new angle we want to impose upon the new spheres and pipes following pipeBeforeSphere.
    */
    public Vector3 turnAround(GameObject pipeBeforeSphere, Quaternion angle){
            float theX = angle.eulerAngles.x;
            float theY = angle.eulerAngles.y;
            float theZ = angle.eulerAngles.z;
            if(theX >180){
                theX = theX - 360;
            }
            if(theY>180){
                theY = theY-360;
            }
            if(theZ >180){
                theZ = theZ-360;
            }
            if(theX == pipeBeforeSphere.transform.localEulerAngles.x*-1 && theX != 0){
                Debug.Log("I entered x");
                return  new Vector3(theX*-1.0f, theY, theZ);
            }
            if(theY == pipeBeforeSphere.transform.localEulerAngles.y*-1&& theY != 0){
                Debug.Log(angle);
                Debug.Log("I entered y");
                return  new Vector3(theX, theY*-1.0f, theZ);
            }
            if(theZ == pipeBeforeSphere.transform.localEulerAngles.z*-1&& theZ != 0){
                return  new Vector3(theX, theY, theZ*-1.0f);
            }
            return Vector3.zero;
    }

}
