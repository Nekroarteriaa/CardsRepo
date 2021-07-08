using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CollectionCardsSortButton : MonoBehaviour , IElementClicked<>
{
    [SerializeField]
    Sprite tipeSortButtonSprite;
    [SerializeField]
    TextMeshProUGUI buttonText;
    Button selfButton;

    public Action onElementCliked;

    private void Awake()
    {
        selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
