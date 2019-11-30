using System.Collections.Generic;

namespace Ic.Syntax{
    public sealed class Lexer
    {
        private string Text { get; }
        private int Position;
        private List<string> _diagnostics = new List<string>();
        public Lexer(string text)
        {
            Text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;
       

        private char Current => Peek(0);

        private char Lookahead => Peek(1);

        private char Peek(int offset)
        {
            var index =Position + offset;

            if (index >=Text.Length)
                return '\0';

            return Text[index];
        }

        private void Next()
        {
            Position++;
        }

        public SyntaxToken Lex()
        {
            if (Position >= Text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, Position, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = Position;

                while (char.IsDigit(Current))
                    Next();

                var length = Position - start;
                var text = Text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                    _diagnostics.Add($"The number {Text} isn't valid Int32.");

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = Position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = Position - start;
                var text = Text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                var start = Position;

                while (char.IsLetter(Current))
                    Next();

                var length = Position - start;
                var text = Text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch (Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, Position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, Position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, Position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, Position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, Position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, Position++, ")", null);
                case '&':
                    if (Lookahead == '&')
                        return new SyntaxToken(SyntaxKind.AmpersandAmpersandToken, Position += 2, "&&", null);
                    break;
                case '|':
                    if (Lookahead == '|')
                        return new SyntaxToken(SyntaxKind.PipePipeToken, Position += 2, "||", null);
                    break;
                case '=':
                    if (Lookahead == '=')
                        return new SyntaxToken(SyntaxKind.EqualsEqualsToken, Position += 2, "==", null);
                    break;
                case '!':
                    if (Lookahead == '=')
                        return new SyntaxToken(SyntaxKind.BangEqualsToken, Position += 2, "!=", null);
                    else
                        return new SyntaxToken(SyntaxKind.BangToken, Position++, "!", null);
            }

            _diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new SyntaxToken(SyntaxKind.BadToken, Position++, Text.Substring(Position - 1, 1), null);
        }

    }
}