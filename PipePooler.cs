using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePooler : MonoBehaviour {
    public static PipePooler sharedInstance;

    public List<GameObject> pipePool; // A list of all the pipes we will use.
    // Might have to make one for spheres. 
    public GameObject pipePrefab;
    public int amountToPool;
    // Awake is called on all objects before start.
    void Awake(){
        sharedInstance = this;
    }
    // Start is called before the first frame update
    void Start() {
        pipePool = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++){
            GameObject pipe = (GameObject) Instantiate(pipePrefab);
            pipe.SetActive(false);
            pipePool.Add(pipe);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
    // returns the first non-active pipe.
    public GameObject GetPooledPipe(){
        for(int i =0; i < pipePool.Count; i++){
            if(!pipePool[i].activeInHierarchy){
                return pipePool[i];
            }
        }
        return null;
    }

}
