using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePipes : MonoBehaviour{

    public GameObject pipePrefab = null; // Our prefab in the Unity Editor's assets. Resembles a cylinder.
    public GameObject turnPrefab = null; // Our prefab in the Unity Editor's assets. Resembles a sphere.
    GameObject[]  pipeParts; // Was originally going to


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

        GameObject objec = Instantiate(pipePrefab, new Vector3(0,0,0), Quaternion.Euler(-90f, 0, 0));
        continuePipe(objec);



        //x -  tried -90, 90
        //y - tried -90, 90
        //z -  90
        GameObject o = Instantiate(pipePrefab, new Vector3(3,0,0), Quaternion.Euler(0f, 0f, 0f));
        o.GetComponent<Renderer>().material.color =  Color.red;
        continuePipe(o);
    }

    // Update is called once per frame
    void Update(){
        

    }


    public void spawnAPrefabSomewhere(){
        Vector3 spawnLocation = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.5f, 5.0f) , 0);
        GameObject objec = Instantiate(pipePrefab, spawnLocation, Quaternion.Euler(0f,0f,0f));
        objec.GetComponent<Renderer>().material.color = new Color(Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));
        continuePipe(objec);
    }



    public void continuePipe(GameObject parentPipe){
        Transform parentTransform = parentPipe.transform;

        Vector3 direction = correctDirection(parentPipe);
        GameObject kid = Instantiate(pipePrefab, parentTransform.position + direction , parentTransform.rotation );
        kid.GetComponent<Renderer>().material.color = parentPipe.GetComponent<Renderer>().material.color;
        kid.transform.parent = parentTransform;


    }








    /*


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
}
