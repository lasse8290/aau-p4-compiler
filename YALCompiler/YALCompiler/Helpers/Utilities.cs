using YALCompiler.Exceptions;

namespace YALCompiler.Helpers;

public static class Utilities
{
    public static int GetArraySizeFromDefiner(string definer)
    {
        if (int.TryParse(definer.Substring(1, definer.Length - 2), out var size))
        {
            return size;
        }
        else
        {
            throw new ArraySizeNotRecognizedException(definer);
        }
    }
}