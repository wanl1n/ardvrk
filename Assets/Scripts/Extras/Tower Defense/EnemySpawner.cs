using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        if (!Player.Instance.gameOver)
        {
            yield return new WaitForSeconds(Random.Range(6, 8));

            GameObject obj = Instantiate(this.enemy);
            obj.transform.localPosition = this.transform.position;
            obj.transform.localScale = this.transform.localScale;

            StartCoroutine(SpawnEnemy());
        }
    }
}
