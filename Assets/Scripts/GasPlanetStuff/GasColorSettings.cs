using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GasColorSettings : ScriptableObject
{
    public Material planetMaterial;
    public Gradient planetColor;
    /*public StripeColorSettings stripeColorSettings;

    [System.Serializable]
    public class StripeColorSettings
    {
        public Stripe[] stripes;

        public class Stripe
        {
            public Gradient planetColor;
            public Color tint;
            [Range(0, 1)]
            public float width;
            [Range(0, 1)]
            public float tintPercent;
        }
    }*/
}
