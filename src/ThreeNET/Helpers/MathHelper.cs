using System.Numerics;

namespace ThreeNET.Helpers;

public static class MathHelper
{
    public static bool Equal(float a, float b, float epsilon = 0.01f)
    {
        return MathF.Abs(a - b) < epsilon;
    }
}