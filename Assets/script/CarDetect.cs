using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDetect : MonoBehaviour {
    [SerializeField]
    CarBase car;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        car.detectEnter(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        car.detectExit(collision.gameObject);

    }
}
