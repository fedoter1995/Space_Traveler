using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStructures.MovingPlatforms;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _positions = new List<Vector3>();
    [SerializeField, Range(0.1f, 5)]
    private float _moveSpeed = 0.1f;
    [SerializeField, Range(1, 10)]
    private float _moveDelay = 1f;
    [SerializeField]
    private PlatformMovePattern _movePattern;

    private int currentPositionIndex = 0;
    private Coroutine moveEnumerator = null;
    private Coroutine triggerEnumerator = null;

    public int PosCount => _positions.Count;

    private void Awake()
    {
        if (!_movePattern)
            throw new System.Exception("No movement pattern added");
        if (_positions.Count <= 1)
            Debug.Log("Insufficient number of positions for platform movement");
        if( _positions.Count > 0)
            transform.position = _positions[0];

        _movePattern.InitMovePattern(_positions.Count);
    }
    public void MoveTo(int posIndex)
    {
        if(_positions.Count - 1 < posIndex || moveEnumerator != null)       
            return;

        StopAllCoroutines();
        currentPositionIndex = posIndex;
        moveEnumerator = StartCoroutine(MoveRoutine());        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        collision.gameObject.transform.SetParent(transform);
        if(triggerEnumerator == null)
            triggerEnumerator = StartCoroutine(OnTrigger());
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
    private IEnumerator OnTrigger()
    {
        yield return new WaitForSeconds(_moveDelay);
        MoveTo(_movePattern.GetMoveIndex(currentPositionIndex));
        triggerEnumerator = null;
    }

    private IEnumerator MoveRoutine() 
    {

        var heading = _positions[currentPositionIndex] - transform.position;
        while (transform.position != _positions[currentPositionIndex])
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(heading.normalized * _moveSpeed);
        }
        moveEnumerator = null;
    }
    public void OnSaveButtonClick()
    {
        _positions.Add(transform.position);
    }
    public void OnDeleteButtonClick()
    {
        if (_positions.Count > 0)
            _positions.Remove(_positions[_positions.Count - 1]);
        else
            Debug.Log($"There are no elements in the {_positions}");
    }
}
