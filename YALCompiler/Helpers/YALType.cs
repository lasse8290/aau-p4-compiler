﻿namespace YALCompiler.Helpers;

public abstract class YALType: IEquatable<YALType>
{
    public bool Equals(YALType other)
    {
        switch (other)
        {
            case (null):
                return false;
            case SingleType st:
                if (this is SingleType st2 && st.Type == st2.Type && st.IsArray == st2.IsArray)
                {
                    return true;
                }
                else if (this is TupleType tt)
                {
                    return tt.Types.Count == 1 && tt.Types[0].Type == st.Type && tt.Types[0].IsArray == st.IsArray;
                }

                break;
            case TupleType tt:
                if (this is SingleType st1)
                {
                    return tt.Types.Count == 1 && tt.Types[0].Type == st1.Type && tt.Types[0].IsArray == st1.IsArray;
                }
                else if (this is TupleType tt2)
                {
                    if (tt.Types.Count != tt2.Types.Count)
                        return false;
                    
                    for (int i = 0; i < tt.Types.Count; i++)
                    {
                        if (tt.Types[i] != tt2.Types[i])
                            return false;
                    }

                    return true;
                }
                break;
        }

        return false;

    }

    public static bool operator ==(YALType first, YALType second)
    {
        if (first is null || second is null)
            return false;
        
        return first.Equals(second);
    }
    public static bool operator !=(YALType first, YALType second)
    {
        if (first is null || second is null)
            return true;
        
        return !first.Equals(second);
    }
}