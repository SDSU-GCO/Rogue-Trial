using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Assertions;
using ByteSheep.Events;
using NaughtyAttributes;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class HeartContainer : MonoBehaviour {

    //Inspector Items
    [ReadOnly][SerializeField] private HeartContainerSprites _ImagePool; //Images to display. (Health in ascending order)
    [ReadOnly][SerializeField] private int _CurrentHitpoints = 0;
    [ReadOnly][SerializeField] private int _MaxHitpoints;

    //Private Items
    public HeartContainerSprites ImagePool { get => _ImagePool; set => _ImagePool = value; }

    //Reference Items
    private Image ImageRef;
    private RectTransform RectTransformRef;

    //Public elements
    public int MaxHitpoints { 
        get => _MaxHitpoints = ImagePool.Sprites.Count - ( ImagePool.ContainsEmptySprite ? 1 : 0 ) ;
    }
    public int CurrentHitpoints { 
        get => _CurrentHitpoints; 
        private set => _CurrentHitpoints = Mathf.Clamp(value, 0, MaxHitpoints);
    }

    //Unity Functions
    private void Awake() {
        ImageRef = GetComponent<Image>();
        RectTransformRef = GetComponent<RectTransform>();
    }

    private void Start() {
        _MaxHitpoints = MaxHitpoints;
        _CurrentHitpoints = 0;
        SetHealth(CurrentHitpoints);
    }

    //Functions
    private Sprite GetSprite( int Health ) => ImagePool.Sprites[Health];
    public void SetHealth( int Health ) {
        CurrentHitpoints = Health;
        if(ImageRef != null) ImageRef.sprite = GetSprite( CurrentHitpoints );
    }

}