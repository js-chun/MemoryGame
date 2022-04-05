using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private GameManager gameSettings;

    public int rows;
    public int cols;
    public int match;

    private float _usedScale = 1f;
    private float _gapPerc = 0.1f;
    private float _cardOrigDim = 2.1f;

    public GameObject cardPrefab;
    public List<int> orderedCards;

    private float _useHeight;
    private float _useWidth;
    private float _heightMax;
    private float _widthMin;

    void Start()
    {
        gameSettings = FindObjectOfType<GameManager>();
        cols = gameSettings.colNum;
        rows = gameSettings.rowNum;
        match = gameSettings.matchNum;
        gameSettings.gameStart();

        Camera camera = Camera.main;
        float _halfHeight = camera.orthographicSize;
        float _halfWidth = camera.aspect * _halfHeight;
        _useHeight = 0.8f * 2 * _halfHeight;
        _useWidth = 0.95f * 2 * _halfWidth;
        _heightMax = _halfHeight - 0.12f * _halfHeight;
        float _widthMax = 0.95f * _halfWidth;
        _widthMin = -_widthMax;

        spawnCards();
    }

    private void Update()
    {
        repeatState();
    }
    //Spawns cards in a set area spaced out and scaled according to rows and columns of cards
    private void spawnCards()
    {

        //float _xdist = _topRight.position.x - _topLeft.position.x;
        //float _ydist = _topLeft.position.y - _bottomLeft.position.y;

        float _xdist = _useWidth;
        float _ydist = _useHeight;

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

        //float xPos = _topLeft.position.x;
        //float yPos = _topLeft.position.y;

        float xPos = _widthMin;
        float yPos = _heightMax;

        randomCards();
        int cardNum = 0;

        float _xInitialSkip;
        float _xUsualSkip;
        float _yInitialSkip;
        float _yUsualSkip;

        if (_usedScale == _xscale)
        {
            _xInitialSkip = (_gapPerc * _xdist) / (cols + 1) + (_xCardDim / 2);
            _xUsualSkip = (_gapPerc * _xdist) / (cols + 1) + _xCardDim;

            _yInitialSkip = (_ydist - (_xCardDim * rows)) / (rows + 1) + (_xCardDim / 2);
            _yUsualSkip = (_ydist - (_xCardDim * rows)) / (rows + 1) + _xCardDim;
        }
        else
        {
            _xInitialSkip = (_xdist - (_yCardDim * cols)) / (cols + 1) + (_yCardDim / 2);
            _xUsualSkip = (_xdist - (_yCardDim * cols)) / (cols + 1) + _yCardDim;

            _yInitialSkip = (_gapPerc * _ydist) / (rows + 1) + (_yCardDim / 2);
            _yUsualSkip = (_gapPerc * _ydist) / (rows + 1) + _yCardDim;
        }


        for (int i = 0; i < cols; i++)
        {
            float _xskip;
            float _yskip;
            if (i == 0)
            {
                _xskip = _xInitialSkip;
                //xPos = _topLeft.position.x;
                xPos = _widthMin;
            }
            else
            {
                _xskip = _xUsualSkip;
            }
            xPos = xPos + _xskip;

            for (int j = 0; j < rows; j++)
            {
                if (j == 0)
                {
                    _yskip = _yInitialSkip;
                    //yPos = _topLeft.position.y;
                    yPos = _heightMax;
                }
                else
                {
                    if (_yUsualSkip > _xUsualSkip)
                    {
                        _yUsualSkip = _xUsualSkip;
                    }
                    _yskip = _yUsualSkip;
                }
                yPos = yPos - _yskip;

                GameObject newCard = Instantiate(cardPrefab, new Vector2(xPos, yPos), Quaternion.identity, transform);
                newCard.transform.localScale = new Vector2(_usedScale, _usedScale);

                newCard.GetComponent<Card>().cardValue = orderedCards[cardNum];
                cardNum = cardNum + 1;
            }
        }
    }

    //Randomizes a list of cards using values. 2 or 3 of same values each based on # of matches
    private void randomCards()
    {
        int totalCards = rows * cols;
        if (match > 0)
        {
            int setCards = totalCards / match;
            orderedCards.Clear();

            List<int> valuesUsed = new List<int>();

            for (int i = 0; i < setCards; i++)
            {
                valuesUsed.Add(0);
            }


            while (orderedCards.Count < totalCards)
            {
                int randomCard = Random.Range(1, setCards + 1);
                int numUsed = valuesUsed[randomCard - 1];

                if (orderedCards.Contains(randomCard))
                {

                    if (numUsed < match)
                    {
                        orderedCards.Add(randomCard);
                        valuesUsed[randomCard - 1] = numUsed + 1;
                    }
                }
                else
                {
                    orderedCards.Add(randomCard);
                    valuesUsed[randomCard - 1] = numUsed + 1;
                }
            }
        }
    }

    private void repeatState()
    {
        if(gameSettings.gameMode != 0)
        {
            if (gameSettings.numCards <= 0)
            {
                spawnCards();
            }
        }
    }

}