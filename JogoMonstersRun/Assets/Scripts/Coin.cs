using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int _rotationSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime, 0, 0);
    }
}
