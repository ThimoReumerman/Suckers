using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {
    [SerializeField] GameObject enemy;
    Renderer rend;
    Vector3 min;
    Vector3 max;
    void Start() {
        rend = GetComponent<Renderer>();
        min = -rend.bounds.extents;
        max = rend.bounds.extents;

        print(min + "max" + max);
    }

    void Update() {
        if(Input.GetButton("Fire1")) {
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        Vector3 spawn;
        spawn.x = Random.Range(min.x, max.x);
        spawn.y = transform.position.y;
        spawn.z = Random.Range(min.x, max.x);

        Instantiate(enemy, spawn, enemy.transform.rotation);
    }
    
}
