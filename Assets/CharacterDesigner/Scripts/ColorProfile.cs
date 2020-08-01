using UnityEngine;

namespace CharacterDesigner.Scripts
{
    [CreateAssetMenu(fileName = "ColorProfile", menuName = "Repnox/ColorProfile", order = 1)]
    public class ColorProfile : ScriptableObject
    {

        public Color minColor;

        public Color maxColor;

        public Color Calculate(float t, float hue=0, float sat=0, float value=0)
        {
            Color.RGBToHSV(Color.Lerp(minColor, maxColor, t),
                out float h, out float s, out float v);
            return Color.HSVToRGB(Mathf.Repeat(h+hue, 1), Mathf.Clamp(s+sat, 0, 1), Mathf.Clamp(v+value, 0, 1));
        }

    }
}
