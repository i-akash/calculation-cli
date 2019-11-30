

namespace Ic.Syntax{
    public enum SyntaxKind
    {
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        EndOfFileToken,
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        AmpersandAmpersandToken,
        PipePipeToken,
        EqualsEqualsToken,
        BangEqualsToken,
        BangToken,
        BadToken,
        TrueKeyword,
        FalseKeyword,
        IdentifierToken,
        ParenthesizedExpression
    }
}