using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Compiler
{
    public static class CompilerHelper
    {
        #region [GetUsing]
        internal static UsingDirectiveSyntax GetUsing(string ns)
        {
            return SyntaxFactory.UsingDirective
            (
                SyntaxFactory.IdentifierName(ns)
            );
        }

        internal static UsingDirectiveSyntax GetUsing(string ns, string ns2)
        {
            return SyntaxFactory.UsingDirective
            (
                SyntaxFactory.QualifiedName
                (
                    SyntaxFactory.IdentifierName(ns),
                    SyntaxFactory.IdentifierName(ns2)
                )
            );
        }

        internal static UsingDirectiveSyntax GetUsing(string ns, string ns2, string ns3)
        {
            return SyntaxFactory.UsingDirective
            (
                SyntaxFactory.QualifiedName
                (
                    SyntaxFactory.QualifiedName
                    (
                        SyntaxFactory.IdentifierName(ns),
                        SyntaxFactory.IdentifierName(ns2)
                    ),
                    SyntaxFactory.IdentifierName(ns3)
                )
            );
        }
        #endregion
    }
}
