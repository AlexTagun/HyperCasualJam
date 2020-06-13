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
    [SerializeField] private float _minDistanceToNextObject;
    [SerializeField] private float _maxDistanceToNextObject;
    [SerializeField] private float _minYPosition;
    [SerializeField] private float _maxYPosition;
    [SerializeField] private Transform transformParent;


    public GameObject GenerationRandomObjectAtRandomInterval(Transform lastObject)
    {
        var randomPosition = new Vector2(Random.Range(lastObject.position.x + _minDistanceToNextObject, lastObject.position.x + _maxDistanceToNextObject),
            Random.Range(gameObject.transform.position.y + _minYPosition,gameObject.transform.position.y + _maxYPosition));
        var objectSpawn = GetRandomTypeObject();
        GameObject newObject = Instantiate(objectSpawn, transformParent);
        newObject.transform.position = randomPosition;
        return newObject;
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
