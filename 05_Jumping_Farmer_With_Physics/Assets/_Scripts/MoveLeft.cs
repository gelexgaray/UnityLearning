using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed = 7f;
    private PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerController.GameOver)
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
