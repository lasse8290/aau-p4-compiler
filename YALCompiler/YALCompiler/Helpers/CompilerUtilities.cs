using YALCompiler.DataTypes;
using YALCompiler.Exceptions;

namespace YALCompiler.Helpers;

public static class CompilerUtilities
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

    public static Symbol? FindSymbol(string identifier, ASTNode node)
    {
        Symbol? symbol = null;
        while (symbol is null && node is not null)
        {
            if (node.SymbolTable is not null && node.SymbolTable.ContainsKey(identifier))
            {
                symbol = node.SymbolTable[identifier];
                break;
            }

            node = node.Parent;
        }

        return symbol;
    }
    
    public static Function? FindFunction(string identifier, ASTNode node)
    {
        Function? function = null;
        while (function is null && node is not null)
        {
            if (node.FunctionTable is not null && node.FunctionTable.ContainsKey(identifier))
            {
                function = node.FunctionTable[identifier];
                break;
            }

            node = node.Parent;
        }

        return function;
    }
}