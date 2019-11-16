using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
public class DiscreteHpBar : MonoBehaviour
{

    public GameObject TrackObject;
    [SerializeField] public HeartContainerSprites ImagePool;
    [Range(1, 1000)] public int Spacing = 1;
    public float Scaling = 1;

    public int MaxHealth => HeartContainers.Count * SegmentCount;
    public int SegmentCount => ( ImagePool.ContainsEmptySprite ? ImagePool.Sprites.Count - 1 : ImagePool.Sprites.Count );

    private int PrevSpacing = 1;
    private float PrevScaling = 1;
    private Vector3 PrevPosition = Vector3.zero;
    private List<GameObject> HeartContainers = new List<GameObject>();
    private RectTransform RectTransformRef;
    private Health HealthObjectRef;

    private Vector3 ReversePositon() => PrevPosition -= new Vector3( Spacing, 0, 0 );
    private Vector3 NextPosition() => PrevPosition += new Vector3( Spacing, 0, 0 );
    private void UpdateSpacing() { 
        PrevPosition = Vector3.zero;
        PrevPosition -= new Vector3( Spacing, 0, 0 );
        foreach(var Hc in HeartContainers) {
            Hc.transform.localPosition = NextPosition();
            Hc.transform.localScale = Vector3.one * Scaling;
        }
    }
    private void AddLastHp() {
        Debug.Log("Add");
        HeartContainers.Add( CreateHeartContainer() );
        UpdateSpacing();
        SetHp(HealthObjectRef.CurrentHealth);
    }
    private void RemoveLastHp() {
        Debug.Log("Remove");
        if( HeartContainers.Count > 0 ) {
            GameObject RemoveObj = HeartContainers.Last();
            RemoveObj.SetActive( false );
            DestroyImmediate( RemoveObj );
            HeartContainers.Remove( RemoveObj );
            ReversePositon();
        }
        SetHp(HealthObjectRef.CurrentHealth);
        UpdateSpacing();
    }
    private void SetHp(int Health) {

        foreach(var Hco in HeartContainers) {
            HeartContainer Hc = Hco.GetComponent<HeartContainer>();
            if( Health >= Hc.MaxHitpoints ) {
                Hc.SetHealth( Hc.MaxHitpoints );
                Health -= Hc.MaxHitpoints;
            } else if( Health < Hc.MaxHitpoints ) {
                Hc.SetHealth( Health );
                Health = 0;
            }
        }

    }
    private void SetMaxHp(int inMaxHealth) {

        int PrevSegmentCount = HeartContainers.Count;
        int TargetSegmentCount = Mathf.CeilToInt( (float) inMaxHealth / SegmentCount );
        int Delta = TargetSegmentCount - PrevSegmentCount;

        Debug.Log($"{PrevSegmentCount} -> {TargetSegmentCount}");

        for( int i = 0; i < Mathf.Abs(Delta); i++ ){
            if(Delta > 0) AddLastHp();
            if(Delta < 0) RemoveLastHp();
        }

    }
    private GameObject CreateHeartContainer() {

        GameObject HeartContainer = new GameObject("HeartContainer");

        HeartContainer.AddComponent<Image>();
        HeartContainer.AddComponent<HeartContainer>().ImagePool = ImagePool;
        RectTransform RectRef = HeartContainer.GetComponent<RectTransform>();
        RectRef.SetParent( transform );
        RectRef.anchorMin = new Vector2( 0f, 0.5f );
        RectRef.anchorMax = new Vector2( 0f, 0.5f );
        RectRef.pivot = new Vector2( 0f, 1f );
        RectRef.transform.localPosition = NextPosition();
        RectRef.transform.localScale = Vector3.one * Scaling;
        HeartContainer.SetActive( true );

        return HeartContainer;

    }

    private void Awake() {
        RectTransformRef = GetComponent<RectTransform>();
        HealthObjectRef = TrackObject.GetComponent<Health>();

        PrevPosition -= new Vector3( Spacing, 0, 0 );
    }

    private void Start() {

        HealthObjectRef.HealEvent.AddListener( i => SetHp( HealthObjectRef.CurrentHealth ));
        HealthObjectRef.InflictEvent.AddListener( i => SetHp( HealthObjectRef.CurrentHealth ));
        HealthObjectRef.IncreaseEvent.AddListener( SetMaxHp );
        HealthObjectRef.DecreaseEvent.AddListener( SetMaxHp );

        Debug.Log($"{HealthObjectRef.CurrentHealth}/{HealthObjectRef.MaxHealth}");
        SetMaxHp( HealthObjectRef.MaxHealth );

    }

    void Update()
    {

        SetHp( HealthObjectRef.CurrentHealth );

        if( Scaling != PrevScaling ) { 
            foreach(var Hc in HeartContainers) {
                Hc.transform.localScale = Vector3.one * Scaling;
            }
        }

        if( Spacing != PrevSpacing ){
            PrevSpacing = Spacing;
            UpdateSpacing();
        }

        //if( this.MaxHealth < HealthObjectRef.MaxHealth || this.MaxHealth > (HealthObjectRef.MaxHealth + SegmentCount) ) {

        //int Delta = Mathf.CeilToInt( (float) HealthObjectRef.MaxHealth / SegmentCount);
        //    PrevHpCount = HpCount;

        //    for( int i = 0; i < Mathf.Abs( Delta ); i++ ) {
        //        if( Delta < 0 ) AddLastHp();
        //        else if( Delta > 0) RemoveLastHp();
        //    }

        //}

    }
}
