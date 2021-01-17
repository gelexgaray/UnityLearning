using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Constants
    private const string FLAP = "Flap";
    #endregion

    private Animator Animator { get => _animator; }
    private Animator _animator;

    public bool Flap
    {
        get => _isFlapping;
        set
        {
            _isFlapping = value;
            Animator.SetBool(FLAP, value);
        }
    }
    private bool _isFlapping = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Flap = Input.GetKey(KeyCode.Space);
    }
}
