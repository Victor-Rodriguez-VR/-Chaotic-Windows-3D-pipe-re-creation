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

	public static PipePooler SharedInstance; // To instantiate as little objects as possible, all GeneratePipe files running in parallel will share objs.
  public List<ObjectPoolItem> objectsToBePooled; //  List of all Object types we need to pool. Instantiated in Unity's editor.
  public List<GameObject> pipeAndSpherePool;  // The actual list of all Uninstantiated GameObjects.

	void Awake() {
	  SharedInstance = this; 
	}

  void Start () {
    pipeAndSpherePool = new List<GameObject>();
    foreach (ObjectPoolItem item in objectsToBePooled) {
      for (int i = 0; i < item.amountToPool; i++) {
        GameObject obj = (GameObject)Instantiate(item.objectToPool);
        obj.SetActive(false);
        pipeAndSpherePool.Add(obj);
      }
    }
  }
	
  /*
    @param tag - The name of the type of object we are trying to get. (Ex: Pipe, Sphere)

    @return - If an uninstantiated GameObject shares the same tag, return said GameObject. Otherwise would return null.
  */
  public GameObject GetPooledObject(string tag) {

    for (int i = 0; i < pipeAndSpherePool.Count; i++) {
        if (!pipeAndSpherePool[i].activeInHierarchy && pipeAndSpherePool[i].tag == tag) {
            return pipeAndSpherePool[i];
        }
    }
    foreach (ObjectPoolItem item in objectsToBePooled) {
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

	// Update is called once per frame
	void Update () {
	
	}
}
