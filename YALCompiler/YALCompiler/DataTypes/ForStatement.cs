namespace YALCompiler.DataTypes;

public class ForStatement : ASTNode
{
    public List<ASTNode> InitialStatements { get; set; }
    public List<Expression> TestStatements { get; set; }
    public List<Expression> UpdateStatements { get; set; }
}