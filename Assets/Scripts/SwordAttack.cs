using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    
    [Header("SwordAttack")] [SerializeField]
    private GameObject projPrefab;
    [SerializeField] private Transform firePos;
    private PlayerPowerSword _playerPowerSword;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private int numOfProjectiles = 1;
    [SerializeField] private float projSpeed = 10f;
    [SerializeField] private float fireRate = 0.2f;
    private PlayerMove _playerMove;

    // Start is called before the first frame update
    void Start()
    {
        _playerMove = playerAnim.gameObject.GetComponent<PlayerMove>();
        playerAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _playerPowerSword = FindObjectOfType<PlayerPowerSword>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && _playerPowerSword.hasSword && canAttack && _playerMove.gameOver==false)
        {
            StartCoroutine(FireSwordAttack());
        }
    }

    IEnumerator FireSwordAttack()
    {
        canAttack = false;
        playerAnim.SetTrigger("Slash");

     //   yield return new WaitForSeconds(0.3f);
        
        for (int i = 0; i < numOfProjectiles; i++)
        {
            _audioSource.PlayOneShot(_audioClip,0.55f);
            yield return new WaitForSeconds(0.35f);
            GameObject proj = Instantiate(projPrefab, firePos.position, Quaternion.identity);
            proj.GetComponent<Rigidbody>().velocity = transform.forward * projSpeed;
            yield return new WaitForSeconds(fireRate);
        }

        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
}
