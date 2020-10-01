using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public float Speed
    {
        get
        {
            return _speed;
        }
    }

    private GameObject target;
    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Magnet();
    }

    public void Magnet()
    {
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) > 0)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, _speed * Time.deltaTime);
        else
            this.enabled = false;
    }
}
