namespace YALParser.Helpers;

public abstract class YALType: IEquatable<YALType>
{
    public bool Equals(YALType other)
    {
        switch (other)
        {
            case SingleType st:
                if (this is SingleType st2 && st.Type == st2.Type)
                {
                    return true;
                }
                else if (this is TupleType tt)
                {
                    return tt.Types.Count == 1 && tt.Types[0] == st.Type;
                }

                break;
            case TupleType tt:
                if (this is SingleType st1)
                {
                    return tt.Types.Count == 1 && tt.Types[0] == st1.Type;
                }
                else if (this is TupleType tt2)
                {
                    return tt.Types.TrueForAll(t => tt2.Types.Any(t2 => t == t2));
                }
                break;
        }

        return false;

    }

    public static bool operator ==(YALType first, YALType second)
    {
        return first.Equals(second);
    }
    public static bool operator !=(YALType first, YALType second)
    {
        return !first.Equals(second);
    }
}