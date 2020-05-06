using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasColorGenerator 
{
    GasColorSettings settings;
    Texture2D texture;
    const int textureResolution = 50;

    public void UpdateSettings(GasColorSettings settings)
    {
        this.settings = settings;
        if (texture == null)// || texture.height != settings.stripeColorSettings.stripes.Length)
        {
            texture = new Texture2D(textureResolution, 1);//settings.stripeColorSettings.stripes.Length);
        }
        
    }

    public float StripePercentFromPoint(Vector3 pointOnUnitSphere)
    {
        return 0;
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[texture.width * texture.height];
        for (int i = 0; i < textureResolution; i++)
        {
            colors[i] = settings.planetColor.Evaluate(i / (textureResolution - 1f));
        }
        /*int colorIndex = 0;
        
        foreach(var stripe in settings.stripeColorSettings.stripes)
        {
            for (int i = 0; i < textureResolution; i++)
            {
                Color gradientCol = stripe.planetColor.Evaluate(i / (textureResolution - 1f));
                Color tintCol = stripe.tint;
                colors[colorIndex] = gradientCol * (1 - stripe.tintPercent) * tintCol * stripe.tintPercent;
                colorIndex++;
            }
        }*/
        
        texture.SetPixels(colors);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);
    }
}
