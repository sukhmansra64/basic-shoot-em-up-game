using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static public Main S;

    public GameObject[] enemies;

    public float enemySpawnPadding;
    // Start is called before the first frame update

    //sets camera as singleton, sets bounds of the camera, and randomly generates
    //enemies in random intervals between 2-3 seconds
    void Awake()
    {
        S = this;

        BoundsCheck.SetCameraBounds(this.GetComponent<Camera>());
        float time = Random.Range(2,4);
        Invoke("SpawnEnemy", time);
    }

    //function which spawns randomly selected enemies using the array
    //then insantiates prefab as a game object and calls the function again 
    public void SpawnEnemy()
    {
        int index = Random.Range(0,enemies.Length);

        GameObject go = Instantiate(enemies[index]) as GameObject;
        Vector3 pos = Vector3.zero;

        float xMin = BoundsCheck.camBounds.min.x+enemySpawnPadding;
        float xMax = BoundsCheck.camBounds.max.x-enemySpawnPadding;
        pos.x = Random.Range(xMin,xMax);
        pos.y = BoundsCheck.camBounds.max.y + enemySpawnPadding;
        go.transform.position = pos;
        float time = Random.Range(2,4);
        Invoke("SpawnEnemy", time);
    }
}
