using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rail : MonoBehaviour {

    public static Rail Instance;
    public GameObject _testNodePrefab;

    private List<Vector3> _nodes;
    private int _nodeCount;

    // Use this for initialization
    void Start() {
        Instance = this;
        _nodeCount = transform.childCount;
        _nodes = new List<Vector3>();
    }
    

    public Vector3 ProjectPositionOnRail(Vector3 pos)
    {
        int closestNodeIndex = GetClosestNodeIndex(pos);

        if (closestNodeIndex == 0)
        {
            return ProjectOnSegment(_nodes[0], _nodes[1], pos);
        } else if (closestNodeIndex == _nodeCount - 1)
        {
            return ProjectOnSegment(_nodes[_nodeCount - 1], _nodes[_nodeCount - 2], pos);
        } else
        {
            Vector3 leftSeg = ProjectOnSegment(_nodes[closestNodeIndex - 1], _nodes[closestNodeIndex], pos);
            Vector3 rightSeg = ProjectOnSegment(_nodes[closestNodeIndex + 1], _nodes[closestNodeIndex], pos);

            if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude)
            {
                return leftSeg;
            } else
            {
                return rightSeg;
            }
        }

    }

    private int GetClosestNodeIndex(Vector3 pos)
    {
        int closestNodeIndex = -1;
        float shortestDistance = 0f;

        for (int i = 0; i < _nodeCount; i++)
        {
            float sqrDistance = (_nodes[i] - pos).sqrMagnitude;
            if (shortestDistance == 0f || sqrDistance < shortestDistance)
            {
                shortestDistance = sqrDistance;
                closestNodeIndex = i;
            }
        }

        return closestNodeIndex;
    }

    private Vector3 ProjectOnSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 segDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(segDirection, v1ToPos);
        if (distanceFromV1 < 0f)
        {
            return v1;
        } else if (distanceFromV1 * distanceFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        } else
        {
            Vector3 fromV1 = segDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }

    public void AddNode(Vector3 node)
    {
        _nodes.Add(node);
        if (_testNodePrefab)
        {
            GameObject nodeObj = Instantiate(_testNodePrefab);
            nodeObj.transform.position = node;
        }
        _nodeCount++;
    }

    public void RemoveNode(Vector3 node)
    {
        if (_nodes.Remove(node))
            _nodeCount--;
    }
}
