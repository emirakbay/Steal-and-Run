using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Gender { Man, Woman };
public class People : MonoBehaviour
{
    [SerializeField] private Gender Gender;
    private bool _isChasing = false;
    private bool _isNervous = false;
    private bool _hasAnimStopped = false;

    public bool IsChasing { get => _isChasing; set => _isChasing = value; }
    public bool IsNervous { get => _isNervous; set => _isNervous = value; }
}

