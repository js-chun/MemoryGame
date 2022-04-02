using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameManager game;
    public int rows = 4;
    public int cols = 2;
    public float _usedScale = 1f;

    public Transform _topLeft;
    public Transform _topRight;
    public Transform _bottomLeft;
    public float _gapPerc = 0.1f;
    public float _cardOrigDim = 2.2f;

    public GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        game = FindObjectOfType<GameManager>();
        //cols = game.colNum;
        //rows = game.rowNum;
        spawnCards();
    }

    void spawnCards()
    {

        float _xdist = _topRight.position.x - _topLeft.position.x;
        float _ydist = _topLeft.position.y - _bottomLeft.position.y;

        float _xCardDim = (1 - _gapPerc) * _xdist / cols;
        float _xscale = _xCardDim / _cardOrigDim;

        float _yCardDim = (1 - _gapPerc) * _ydist / rows;
        float _yscale = _yCardDim / _cardOrigDim;

        if ((_xCardDim * rows) + (_gapPerc * _ydist) <= _ydist)
        {
            _usedScale = _xscale;
        }
        else 
        {
            _usedScale = _yscale;
        }

        float xPos = _topLeft.position.x;
        float yPos = _topLeft.position.y;

        for (int i = 0; i < cols; i++)
        {
            float _xskip;
            float _yskip;

            if (i == 0)
            {
                if (_usedScale == _xscale) { _xskip = (_gapPerc * _xdist) / (cols + 1) + (_xCardDim / 2); }
                else { _xskip = (_xdist - (_yCardDim * cols)) / (cols + 1) + (_yCardDim / 2); }
                xPos = _topLeft.position.x;
            }
            else
            {
                if (_usedScale == _xscale) { _xskip = (_gapPerc * _xdist) / (cols + 1) + _xCardDim; }
                else { _xskip = (_xdist - (_yCardDim * cols)) / (cols + 1) + _yCardDim; }
            }
            xPos = xPos + _xskip;

            for (int j = 0; j < rows; j++)
            {
                if (j == 0)
                {
                    if (_usedScale == _xscale) { _yskip = (_ydist - (_xCardDim * rows)) / (rows + 1) + (_xCardDim / 2); }
                    else { _yskip = (_gapPerc * _ydist) / (rows + 1) + (_yCardDim / 2); }
                    yPos = _topLeft.position.y;
                }
                else
                {
                    if (_usedScale == _xscale) { _yskip = (_ydist - (_xCardDim * rows)) / (rows + 1) + _xCardDim; }
                    else { _yskip = (_gapPerc * _ydist) / (rows + 1) + _yCardDim; }
                }
                yPos = yPos - _yskip;

                GameObject newCard = Instantiate(cardPrefab, new Vector2(xPos, yPos), Quaternion.identity);
                newCard.transform.localScale = new Vector2(_usedScale,_usedScale);
            }
        }
    }
}
