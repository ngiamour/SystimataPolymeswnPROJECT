using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WinManager : MonoBehaviour
{
    public float totalTime = 90;
    private PlayerMove _playerMove;
//    [SerializeField] private GameObject gameWonObject;
    
    public PlayableDirector cutsceneDirector;
    public bool gameWon = false;
    [SerializeField] private GameObject audioToStop;
    public bool stopSpawning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTime > 0)
        {
            totalTime -= Time.deltaTime;
        }

        if (totalTime <= 0 && !cutsceneDirector.playableGraph.IsValid())
        {
            GameWonFoo();
        }

        if (totalTime <= 11)
        {
            stopSpawning = true;
        }
    }

    private void GameWonFoo()
    {
        audioToStop.SetActive(false);
        gameWon = true;
        cutsceneDirector.Play();
        _playerMove.enabled = false;
        
        //  gameWonObject.SetActive(true); //Play Cutscene

    }
}
