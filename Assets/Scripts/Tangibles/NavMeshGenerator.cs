using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshGenerator : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
