using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer[] mesh = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in mesh)
        {
            meshRenderer.gameObject.AddComponent<MeshCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
