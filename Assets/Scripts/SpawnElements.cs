using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElements : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elements = new List<GameObject>();

    [SerializeField] private float _seconds = 2f;
    GameObject _grid;


    void Start()
    {
        _grid = GameSettings.instance.grid;
    }

    
    void Update()
    {
        _seconds -= Time.deltaTime;

        if (_seconds <= 0)
        {
            _seconds = 2f;
            
            SpawnElement();
        }
    }

    void SpawnElement()
    {
        int rnd = Random.Range(0, _elements.Count);
        Transform clearCell = CheckClearCell();

        if (clearCell == null)
        {
            return;
        }

        var newElement = Instantiate(_elements[rnd].gameObject, Vector2.zero, Quaternion.identity, clearCell);
        //newElement.transform.localPosition = new Vector2(0,Mathf.Abs(clearCell.GetComponent<RectTransform>().anchoredPosition.y) / 2);
        newElement.transform.localPosition = new Vector2(0, 500);

        newElement.transform.DOLocalMove(new Vector2(0, 0), .5f);
        newElement.GetComponent<SpriteRenderer>().DOFade(0, 0);
        newElement.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);

        clearCell.GetComponent<Cell>().cellOccupied = true;

    }

    Transform CheckClearCell()
    {
        Transform newClearTransform = null;
        
        for (int i = 0; i < _grid.transform.childCount; i++)
        {
            if (!_grid.transform.GetChild(i).GetComponent<Cell>().cellOccupied) // here mb error
            {
                _grid.transform.GetChild(i).GetComponent<Cell>().cellOccupied = true;
                newClearTransform = _grid.transform.GetChild(i);
                break;
            }
        }

        return newClearTransform;
    }
}