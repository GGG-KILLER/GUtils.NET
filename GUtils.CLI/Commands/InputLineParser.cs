﻿using System;
using System.Collections.Generic;
using System.Text;
using GUtils.CLI.Commands.Errors;

namespace GUtils.CLI.Commands
{
    internal class InputLineParser
    {
        private readonly String Input;
        private Int32 Offset;

        private InputLineParser ( String input )
        {
            this.Input = input;
            this.Offset = 0;
        }

        private String ParseQuotedString ( Char separator )
        {
            var start = this.Offset;
            this.Offset++;

            // Read the entire string
            var b = new StringBuilder ( );
            while ( this.Offset < this.Input.Length && this.Input[this.Offset] != separator )
                b.Append ( this.ParseCharacter ( ) );

            if ( this.Offset == this.Input.Length || this.Input[this.Offset] != separator )
                throw new InputLineParseException ( "Unfinished quoted string literal.", start );
            // skip '<separator>'
            this.Offset++;

            return b.ToString ( );
        }

        private Char ParseCharacter ( )
        {
            if ( this.Input[this.Offset] == '\\' )
            {
                this.Offset++;
                if ( this.Offset == this.Input.Length )
                    throw new InputLineParseException ( "Unfinished escape.", this.Offset - 1 );

                switch ( this.Input[this.Offset++] )
                {
                    case 'a':
                        return '\a';

                    case 'b':
                    {
                        if ( this.Input[this.Offset] != '0' && this.Input[this.Offset] != '1' )
                            return '\b';

                        var idx = this.Offset;
                        while ( this.Input[idx] == '0' || this.Input[idx] == '1' )
                            idx++;

                        var num = this.Input.Substring ( this.Offset, idx - this.Offset );
                        this.Offset = idx;
                        return ( Char ) Convert.ToUInt32 ( num, 2 );
                    }

                    case 'f':
                        return '\f';

                    case 'n':
                        return '\n';

                    case 'o':
                    {
                        var idx = this.Offset;
                        while ( '0' <= this.Input[idx] && this.Input[idx] <= '8' )
                            idx++;
                        if ( this.Offset == idx )
                            throw new InputLineParseException ( "Invalid octal escape.", this.Offset - 2 );

                        var num = this.Input.Substring ( this.Offset, idx - this.Offset );
                        this.Offset = idx;
                        return ( Char ) Convert.ToUInt32 ( num, 8 );
                    }

                    case 'r':
                        return '\r';

                    case 't':
                        return '\t';

                    case 'v':
                        return '\v';

                    case 'x':
                    {
                        var idx = this.Offset;
                        while ( idx < this.Input.Length
                            && ( ( '0' <= this.Input[idx] && this.Input[idx] <= '9' )
                                || ( 'a' <= this.Input[idx] && this.Input[idx] <= 'f' )
                                || ( 'A' <= this.Input[idx] && this.Input[idx] <= 'F' ) ) )
                            idx++;
                        if ( this.Offset == idx )
                            throw new InputLineParseException ( "Invalid hexadecimal escape.", this.Offset - 2 );

                        var num = this.Input.Substring ( this.Offset, idx - this.Offset );
                        this.Offset = idx;
                        return ( Char ) Convert.ToUInt32 ( num, 16 );
                    }

                    case ' ':
                        return ' ';

                    case '"':
                        return '"';

                    case '\'':
                        return '\'';

                    case '\\':
                        return '\\';

                    case Char ch when '0' <= ch && ch <= '9':
                    {
                        // We ended up consuming one of the digits
                        // on this one
                        this.Offset--;
                        var idx = this.Offset;
                        while ( '0' <= this.Input[idx] && this.Input[idx] <= '9' )
                            idx++;
                        if ( this.Offset == idx )
                            throw new InputLineParseException ( "Invalid decimal escape.", this.Offset - 2 );

                        var num = this.Input.Substring ( this.Offset, idx - this.Offset );
                        this.Offset = idx;
                        return ( Char ) Convert.ToUInt32 ( num, 10 );
                    }

                    default:
                        throw new InputLineParseException ( "Invalid character escape", this.Offset - 1 );
                }
            }
            else if ( this.Offset < this.Input.Length )
                return this.Input[this.Offset++];
            else
                throw new InputLineParseException ( "Expected char but got EOF", this.Offset );
        }

        private String ParseSpaceSeparatedSection ( )
        {
            var builder = new StringBuilder ( );
            while ( this.Offset < this.Input.Length
                && !Char.IsWhiteSpace ( this.Input[this.Offset] ) )
                builder.Append ( this.ParseCharacter ( ) );
            return builder.ToString ( );
        }

        private void ConsumeWhitespaces ( )
        {
            while ( this.Offset < this.Input.Length
                && Char.IsWhiteSpace ( this.Input[this.Offset] ) )
                this.Offset++;
        }

        private IEnumerable<String> Parse ( )
        {
            while ( this.Offset < this.Input.Length )
            {
                switch ( this.Input[this.Offset] )
                {
                    // Skip all whitespaces outside of quotes
                    case Char ch when Char.IsWhiteSpace ( ch ):
                        this.ConsumeWhitespaces ( );
                        break;

                    // Parse quoted strings
                    case Char ch when ch == '\'' || ch == '"':
                        yield return this.ParseQuotedString ( ch );
                        break;

                    // (raw)Rest """operator""" (r:)
                    case 'r':
                        // Raw rest
                        if ( this.Input[this.Offset + 1] == 'r'
                            && this.Input[this.Offset + 2] == ':' )
                        {
                            // Move from 'r' while skipping 'r'
                            // and ':'
                            this.Offset += 3;

                            yield return this.Input.Substring ( this.Offset );
                            this.Offset = this.Input.Length;
                            break;
                        }
                        else if ( this.Input[this.Offset + 1] == ':' )
                        {
                            // Move from 'r' while skipping ':'
                            this.Offset += 2;

                            var b = new StringBuilder ( );
                            while ( this.Offset < this.Input.Length )
                                b.Append ( this.ParseCharacter ( ) );
                            this.Offset = this.Input.Length;

                            // We do not return non-explicit empty
                            // strings under any circumstances
                            if ( b.Length > 0 )
                                yield return b.ToString ( );
                            break;
                        }
                        else goto default;

                    // Normal space-separated string
                    default:
                        yield return this.ParseSpaceSeparatedSection ( );
                        break;
                }
            }
        }

        public static IEnumerable<String> Parse ( String line )
        {
            return new InputLineParser ( line ).Parse ( );
        }

        /// <summary>
        /// Parse the line separating only by quotes and spaces
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static IEnumerable<String> SimpleParse ( String line )
        {
            var partAcc = new StringBuilder ( );
            var quoteChar = '\0';

            foreach ( var ch in line )
            {
                if ( ( ch == '\'' || ch == '"' ) )
                {
                    if ( quoteChar == '\0' )
                    {
                        quoteChar = ch;
                        continue;
                    }
                    else if ( quoteChar == ch )
                    {
                        quoteChar = '\0';
                        yield return partAcc.ToString ( );
                        partAcc.Clear ( );
                        continue;
                    }
                }
                else if ( ch == ' ' && quoteChar != '\0' && partAcc.Length > 0 )
                {
                    yield return partAcc.ToString ( );
                    partAcc.Clear ( );
                    continue;
                }

                partAcc.Append ( ch );
            }

            if ( partAcc.Length > 0 )
                yield return partAcc.ToString ( );
        }
    }
}