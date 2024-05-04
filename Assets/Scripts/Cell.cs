using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool cellOccupied;

    private Image _image;

    private void Start()
    {

        _image = GetComponent<Image>();
        _image.color = new Color32(176, 131, 67, 100);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.CompareTag("Element") && col.gameObject.GetComponent<ElementMovement>().GetStateTouched)
        //{
        //    _image.color = new Color32(141, 97, 33, 100);
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Element"))
        {
            _image.color = new Color32(176, 131, 67, 100);
        }
    }
}