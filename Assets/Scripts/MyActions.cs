using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using TMPro;
using UnityEngine;

public class MyActions : MonoBehaviour
{
    private int _textNumber = 0;
    private bool _move;
    private bool _rotate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;
        if (_move)
        {
            gameObject.transform.position += (Vector3.forward * deltaTime);
        }

        if (_rotate)
        {
            gameObject.transform.Rotate(0,0,10f * deltaTime);
        }
    }

    public void Move()
    {
        Debug.Log("Switch Moving the Cube");
        _move = !_move;
    }

    public void Rotate()
    {
        Debug.Log("Switch Rotating the Cube");
        _rotate = !_rotate;
    }

    public void Scale()
    {
        Debug.Log("Scale the Cube");
        const float scale = 0.9f;
        var localScale = gameObject.transform.localScale;
        var x = localScale.x * scale;
        var y = localScale.y * scale;
        var z = localScale.z * scale;
        gameObject.transform.localScale = new Vector3(x,y,z);
    }

    public void ScaleOneDirection()
    {
        Debug.Log("Scale One Direction");
        const float scale = 1.1f;
        var localScale = gameObject.transform.localScale;
        var x = localScale.x;
        var y = localScale.y * scale;
        var z = localScale.z;
        gameObject.transform.localScale = new Vector3(x,y,z);
    }

    public void IncreaseTextNumber()
    {
        _textNumber += 1;
        gameObject.GetComponent<TextMeshPro>().text = $"{_textNumber}";
    }
}
