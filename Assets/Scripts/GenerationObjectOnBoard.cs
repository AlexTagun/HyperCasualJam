using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable] public struct ObjectGeneration
{
    public GameObject _objectForGenerate;
    public float _generationProbability;
}
public class GenerationObjectOnBoard : MonoBehaviour
{
    [SerializeField] private List<ObjectGeneration> _generationsObjects = new List<ObjectGeneration>();
    [SerializeField] private int _minAmountObjectsOnBoard;
    [SerializeField] private int _maxAmountObjectsOnBoard;
    [SerializeField] private Transform transformParent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerationRandomObjectAtRandomPosition (GameObject board)
    {
        var boardCollider = board.GetComponent<BoxCollider2D>();
        //var positionRange = new Vector2();
        var ramdomAmountObjects = Random.Range(_minAmountObjectsOnBoard, _maxAmountObjectsOnBoard + 1);
        for (int i = 0; i < ramdomAmountObjects; i++)
        {
            var randomPosition = new Vector2(Random.Range(board.transform.position.x - board.transform.localScale.x * boardCollider.size.x/2, board.transform.position.x + board.transform.localScale.x * boardCollider.size.x/2), board.transform.position.y);

            var objectSpawn = GetRandomTypeObject();
            GameObject newObject= Instantiate(objectSpawn, transformParent);
            newObject.transform.position = randomPosition;

        }
    }

    private GameObject GetRandomTypeObject()
    {
        var randomNumber = Random.value;
        var currentRangeMin = 0f;

        foreach (var objectGeneration in _generationsObjects)
        {
            float currentRangeMax = currentRangeMin + objectGeneration._generationProbability;
            if (randomNumber >= currentRangeMin && randomNumber <= currentRangeMax)
            {
                return objectGeneration._objectForGenerate;
            }
                currentRangeMin = currentRangeMax;
        }
        return null;
    }
}
