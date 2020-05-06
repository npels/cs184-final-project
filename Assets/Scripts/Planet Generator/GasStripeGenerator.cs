using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasStripeGenerator 
{
    ColorSettings colorSettings;

    public void UpdateElevation(MinMax elevationMinMax)
    {
        colorSettings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateSettings(ColorSettings settings)
    {
        this.colorSettings = colorSettings;
    }
}
