using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField] Transform target;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 position = target.transform.position - offset;
            this.transform.position = Vector3.Lerp(this.transform.position, position, 1.5f);
        }
       
    }
}
