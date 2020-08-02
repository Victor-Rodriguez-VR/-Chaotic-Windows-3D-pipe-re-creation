using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class acts like a struct - all variables are public. 
[System.Serializable]
public class ObjectPoolItem {
  public GameObject objectToPool; // The prefab we are making a gameobject.
  public int amountToPool;  // The number of objects we instantiate upon start.
  public bool shouldExpand; // Determines whether more Gameobjects should be added to the pool.
}

public class PipePooler : MonoBehaviour {

	public static PipePooler SharedInstance; // To instantiate as few objects as possible, all GeneratePipe files running in parallel will share objs.
  public List<ObjectPoolItem> objectsToBePooled; //  List of all Object we plan to pool.
  public List<GameObject> ObjectPool;  // The list of all active and inactive prefabs. 
  public Camera gameCamera; // The main camera in the game world.
  public  AudioSource song; // can be any song :)

	void Awake() {
	  SharedInstance = this; 
    Vector3 newCameraPosition = new Vector3(Random.Range(-20.0f, -15.0f), Random.Range(-10.0f, 5.0f) , Random.Range(-13.0f, -3.0f));
    gameCamera.transform.position = newCameraPosition;
    gameCamera.transform.LookAt( new Vector3(0f,0f,0f ));
    song =  GetComponent<AudioSource>();
    if(song.time != null){
      song.time = 73.0f;
    }
    ObjectPool = new List<GameObject>();
    foreach (ObjectPoolItem item in objectsToBePooled) {
      for (int i = 0; i < item.amountToPool; i++) {
        GameObject obj = (GameObject)Instantiate(item.objectToPool);
        obj.SetActive(false);
        ObjectPool.Add(obj);
      }
    }
  }

	
  /*
    * Gets the most readibly accessible Pipe object that is instantiated but inactive.
    * @param tag - The name of the Pipe object we are trying to get. (Ex: Pipe, Sphere)
    * @return - If an inactive GameObject shares the same tag, return said GameObject. Otherwise would return null.
  */
  public GameObject GetPooledObject(string tag, int index) {
    if (index != -50) {
      return ObjectPool[index];
    }
    
    foreach (ObjectPoolItem item in objectsToBePooled) { // Checks our objectsToBePooled to confirm whether or not we can instantiate more.
        if (item.objectToPool.tag == tag) {
            if (item.shouldExpand) {
            GameObject obj = (GameObject)Instantiate(item.objectToPool);
            obj.SetActive(false);
            ObjectPool.Add(obj);
            return obj;
            }
        }
    }
    return null;
  }

  /*
    * Deactivates an object within ObjectPool.
    * @param poolIndex - the index of the object we are trying to delete from 
  */
  public void RemovePooledObject(int poolIndex){
      ObjectPool[poolIndex].SetActive(false);
  }

  /*
    * Retrieves the index of the most available object.

    * @param tag - the tag of the object type we want. 
  */
  public int getPoolIndex(string tag){
    for (int i = 0; i < ObjectPool.Count; i++) {
      if (!ObjectPool[i].activeInHierarchy && ObjectPool[i].tag == tag) {
        return i;
      }
    }
    return -50; // some random value that denotes no index found.
  }
}
