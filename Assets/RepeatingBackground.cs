using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RepeatingBackground : MonoBehaviour
{
    // Start is called before the first frame update
    float leftScreenEdgeX;
    public float speed;
    private float spriteWidth;
    public Transform[] segments;
    private Queue<Transform> segmentQueue;

    void Start()
    {
        leftScreenEdgeX = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        var size = segments[0].GetComponent<SpriteRenderer>().size;
        spriteWidth = size.x *= segments[0].transform.lossyScale.x;
        segmentQueue = new Queue<Transform>();
        foreach (var segment in segments)
        {
            segmentQueue.Enqueue(segment);
        }
    }

    void Update()
    {
        foreach (var segment in segments)
        {
            segment.Translate(Vector2.left * Time.deltaTime * speed);
        }
        var firstSegment = segmentQueue.Peek();
        if (leftScreenEdgeX - firstSegment.position.x > spriteWidth / 2)
        {
            segmentQueue.Dequeue();
            firstSegment.Translate(Vector2.right * spriteWidth * segments.Length);
            var newPos = firstSegment.transform.position;
            newPos.x = segmentQueue.Last().position.x + spriteWidth;
            firstSegment.transform.position = newPos;
            segmentQueue.Enqueue(firstSegment);
        }
    }
}
