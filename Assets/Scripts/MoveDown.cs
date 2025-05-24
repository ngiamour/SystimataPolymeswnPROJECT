using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;

    private PlayerMove _playerMove;
    private WinManager _winManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
        _winManager = FindObjectOfType<WinManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerMove.gameOver && _winManager.gameWon==false)
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
    }
}
