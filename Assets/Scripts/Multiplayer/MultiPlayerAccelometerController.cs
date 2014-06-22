using UnityEngine;
using System.Collections;

public class MultiPlayerAccelometerController : AccelometerController
{   
    public override void Update()
    {
        if (networkView.isMine)
        {
            base.Update();
        }
    }
}
