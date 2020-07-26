using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class acts like a struct - all variables are public. May actually create a struct if it will help performance. 
[System.Serializable]
public class ObjectPoolItem {
  public GameObject objectToPool;
  public int amountToPool;
  public bool shouldExpand;
}

public class PipePooler : MonoBehaviour {

	public static PipePooler SharedInstance; // To instantiate as few objects as possible, all GeneratePipe files running in parallel will share objs.
  public List<ObjectPoolItem> objectsToBePooled; //  List of all Object types we need to pool. Instantiated in Unity's editor.
  public List<GameObject> pipeAndSpherePool;  // The actual list of all Uninstantiated GameObjects.
  public Camera gameCamera;
  public  AudioSource song;
	void Awake() {
	  SharedInstance = this; 
	}

  void Start () {
    song =  GetComponent<AudioSource>();
    song.time = 73.0f;
    pipeAndSpherePool = new List<GameObject>();
    Vector3 newCameraPosition = new Vector3(Random.Range(-20.0f, -15.0f), Random.Range(-10.0f, 5.0f) , Random.Range(-13.0f, -3.0f));
    gameCamera.transform.position = newCameraPosition;
    gameCamera.transform.LookAt( new Vector3(0f,0f,0f ));
    foreach (ObjectPoolItem item in objectsToBePooled) {
      for (int i = 0; i < item.amountToPool; i++) {
        GameObject obj = (GameObject)Instantiate(item.objectToPool);
        obj.SetActive(false);
        pipeAndSpherePool.Add(obj);
      }
    }
  }
  // Future plans: make the application go along with the beat and move the camera to different locations for extra depth. Because it sounds cool! :) Or not depends.
	
  /*
    * Gets the most readibly accessible Pipe object that is instantiated but inactive.
    * @param tag - The name of the Pipe object we are trying to get. (Ex: Pipe, Sphere)
    * @return - If an inactive GameObject shares the same tag, return said GameObject. Otherwise would return null.
  */
  public GameObject GetPooledObject(string tag, int index) {
    Debug.Log(song.time);
    if (index != -50) {
      return pipeAndSpherePool[index];
    }
    
    foreach (ObjectPoolItem item in objectsToBePooled) { // Checks our objectsToBePooled to confirm whether or not we can instantiate more.
        if (item.objectToPool.tag == tag) {
            if (item.shouldExpand) {
            GameObject obj = (GameObject)Instantiate(item.objectToPool);
            obj.SetActive(false);
            pipeAndSpherePool.Add(obj);
            return obj;
            }
        }
    }
    return null;
  }

  /*
    * Deactivates an object within pipeAndSpherePool.
    * @param poolIndex - the index of the object we are trying to delete from 
  */
  public void RemovePooledObject(int poolIndex){
      pipeAndSpherePool[poolIndex].SetActive(false);
  }

  /*
    * Retrieves the index of the most available object.

    * @param tag - the tag of the most available object. 
  */
  public int getPoolIndex(string tag){
    for (int i = 0; i < pipeAndSpherePool.Count; i++) {
      if (!pipeAndSpherePool[i].activeInHierarchy && pipeAndSpherePool[i].tag == tag) {
        return i;
      }
    }
    return -50; // some random value.
  }
}
