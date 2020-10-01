using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    public float Speed
    {
        get
        {
            return _speed;
        }
    }

    private GameObject _target;
    public GameObject Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Magnetize();
    }

    // Magnetize gameObject to target with speed = _speed
    public void Magnetize()
    {
        if(_target == null)
        {
            return;
        }

        if (Vector3.Distance(gameObject.transform.position, _target.transform.position) > 0)
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _target.transform.position, _speed * Time.deltaTime);
        else
            this.enabled = false;
    }
}