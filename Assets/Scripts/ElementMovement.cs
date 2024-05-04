using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementMovement : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Transform _oldParent;
    [SerializeField] Transform _newParent;

    public int level;


    private void Start()
    {
        transform.DOScale(3, 2f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cell")
        {
            if (col.transform.childCount == 0)
            {
                _newParent = col.transform;
            } else if (col.transform.childCount == 1)
            {
                if (col.transform.GetChild(0).tag == "Element")
                {
                    if (col.transform.GetChild(0).GetComponent<ElementMovement>().level == level)
                    {
                        // Join
                    } else
                    {
                        // Change
                    }
                }
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.tag == "Cell")
        {
            if (other.transform == _newParent) 
            {
                _newParent = null;
            }
         }
    }

    private void Update()
    {
            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.right);
            //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.right, Color.red);


            //if (hit.collider != null)
            //{
            //    Debug.Log(hit.collider.name);
            //}
    }

    void FixedUpdate()
    {
       
    }

    void Ray()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 1000f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1000f);

        if (hit && hit.collider.CompareTag("Cell"))
        {
            if (hit.transform.childCount == 0)
            {
                transform.parent = hit.transform.parent;
                //��� ������, ����������� ��� �������� ��������

                if (hit.collider.transform.childCount == 0)
                {
                    transform.parent = hit.transform;

                    transform.DOLocalMove(Vector2.zero, .2f);
                }
            }
        }
    }


    void ChangeElements(Transform currentElement, Transform targetElement)
    {
        
    }

    void JoinElements(Transform currentElement, Transform targetElement)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _oldParent = transform.parent;
        transform.parent = GameSettings.instance.canvas.transform;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition += eventData.delta / GameSettings.instance.canvas.scaleFactor;
    }
}