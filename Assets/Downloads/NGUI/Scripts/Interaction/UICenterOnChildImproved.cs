using UnityEngine;

/// <summary>
/// Ever wanted to be able to auto-center on an object within a draggable panel?
/// Attach this script to the container that has the objects to center on as its children.
/// </summary>
[AddComponentMenu("NGUI/Interaction/Center On Child")]
public class UICenterOnChildImproved : MonoBehaviour
{
    #region Fields Arranged Nicely :)
    
    public UIDraggablePanel draggablePanel;
    
    public Transform parentForChildren;
    
   // public System.EventHandler onCenteredOnChild;
    
    public GameObject centeredObject;
    
    public float scaleFactor = 1.2F;
    
    public bool scaleSelected = false;
    
    UIPanel panel;
    
    #endregion Fields Arranged Nicely :)
    
    #region Properties
    
    public UIPanel Panel
    {
        get
        {
            if (!panel)
            {
                panel = draggablePanel.GetComponent<UIPanel>();
            }
            return panel;
        }
    }
    
    #endregion Properties
    
    #region Methods
    
    public void CenterOn(Transform closest)
    {
        if (closest != null)
        {

            Vector4 clip = Panel.clipRange;
            Transform dt = Panel.cachedTransform;
            Vector3 center = dt.localPosition;
            center.x += clip.x;
            center.y += clip.y;
            center = dt.parent.TransformPoint(center);
        
            centeredObject = closest.gameObject;
            if (scaleSelected)
            {
            
                TweenScale.Begin(centeredObject, 0.3F, Vector3.one * scaleFactor);
            }
        
            // onCenteredOnChild.Invoke(this, null);
        
            // Figure out the difference between the chosen child and the panel's center in local coordinates
            Vector3 cp = dt.InverseTransformPoint(closest.position);
            Vector3 cc = dt.InverseTransformPoint(center);
            Vector3 offset = cp - cc;
        
            // Offset shouldn't occur if blocked by a zeroed-out scale
            if (draggablePanel.scale.x == 0f)
                offset.x = 0f;
            if (draggablePanel.scale.y == 0f)
                offset.y = 0f;
            if (draggablePanel.scale.z == 0f)
                offset.z = 0f;
        
            // Spring the panel to this calculated position
            SpringPanel.Begin(draggablePanel.gameObject, dt.localPosition - offset, 8f);
        }
    }
    /// <summary>
    /// Recenter the draggable list on the center-most child.
    /// </summary>
    public void Recenter()
    {
        Vector4 clip = Panel.clipRange;
        Transform dt = Panel.cachedTransform;
        Vector3 center = dt.localPosition;
        center.x += clip.x;
        center.y += clip.y;
        center = dt.parent.TransformPoint(center);
        
        // Offset this value by the momentum
        Vector3 offsetCenter = center - draggablePanel.currentMomentum * (draggablePanel.momentumAmount * 0.1f);
        draggablePanel.currentMomentum = Vector3.zero;
        
        float min = float.MaxValue;
        Transform closest = null;
        
        // Determine the closest child
        for (int i = 0, imax = parentForChildren.childCount; i < imax; ++i)
        {
            Transform t = parentForChildren.GetChild(i);
            float sqrDist = Vector3.SqrMagnitude(t.position - offsetCenter);
            
            if (sqrDist < min)
            {
                min = sqrDist;
                closest = t;
            }
            if (scaleSelected)
            {
                TweenScale.Begin(t.gameObject, 0.3F, Vector3.one);
            }
        }
        
        CenterOn(closest);
    }
    
    void Awake()
    {
        draggablePanel.onDragFinished = OnDragFinished;
    }
    void OnDragFinished()
    {
        if (enabled) Recenter();
    }
    /// <summary>
    /// Game object that the draggable panel is currently centered on.
    /// </summary>
    void OnEnable()
    {
        Recenter();
    }
    
    #endregion Methods
}