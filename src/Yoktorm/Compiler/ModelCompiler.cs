using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yoktorm.Compiler
{
    public class ModelCompiler : IModelCompiler
    {
        private CompilerConfiguration configuration;

        public ModelCompiler(CompilerConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private SyntaxTree AddTestMethod()
        {
            return SyntaxFactory.CompilationUnit()
                .WithUsings
                (
                    SyntaxFactory.SingletonList<UsingDirectiveSyntax>
                    (
                        SyntaxFactory.UsingDirective
                        (
                            SyntaxFactory.IdentifierName("System")
                        )
                    )
                )
                .WithMembers
                (
                    SyntaxFactory.SingletonList<MemberDeclarationSyntax>
                    (
                        SyntaxFactory.NamespaceDeclaration
                        (
                            SyntaxFactory.IdentifierName("_YTest")
                        )
                        .WithMembers
                        (
                            SyntaxFactory.SingletonList<MemberDeclarationSyntax>
                            (
                                SyntaxFactory.ClassDeclaration("YTest")
                                .WithModifiers
                                (
                                    SyntaxFactory.TokenList
                                    (
                                        SyntaxFactory.Token(SyntaxKind.PublicKeyword)
                                    )
                                )
                                .WithMembers
                                (
                                    SyntaxFactory.SingletonList<MemberDeclarationSyntax>
                                    (
                                        SyntaxFactory.MethodDeclaration
                                        (
                                            SyntaxFactory.PredefinedType
                                            (
                                                SyntaxFactory.Token(SyntaxKind.StringKeyword)
                                            ),
                                            SyntaxFactory.Identifier("Test")
                                        )
                                        .WithModifiers
                                        (
                                            SyntaxFactory.TokenList
                                            (
                                                SyntaxFactory.Token(SyntaxKind.PublicKeyword)
                                            )
                                        )
                                        .WithBody
                                        (
                                            SyntaxFactory.Block
                                            (
                                                SyntaxFactory.SingletonList<StatementSyntax>
                                                (
                                                    SyntaxFactory.ReturnStatement
                                                    (
                                                        SyntaxFactory.LiteralExpression
                                                        (
                                                            SyntaxKind.StringLiteralExpression,
                                                            SyntaxFactory.Literal("Yoktorm!")
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
                .NormalizeWhitespace().SyntaxTree;
        }

        /// <summary>
        /// Creates the instance creator
        /// </summary>
        /// <param name="structure">Structure instnace</param>
        /// <returns>Syntax tree which contains the instance creator</returns>
        private SyntaxTree CreateInstanceCreator(Db.DatabaseStructure structure)
        {
            var ifStatements = new List<StatementSyntax>();

            // Add tables
            foreach (var table in structure.Tables)
            {
                ifStatements.Add
                    (
                        SyntaxFactory.IfStatement
                        (
                            SyntaxFactory.BinaryExpression
                            (
                                SyntaxKind.EqualsExpression,
                                SyntaxFactory.IdentifierName("tableName"),
                                SyntaxFactory.LiteralExpression
                                (
                                    SyntaxKind.StringLiteralExpression,
                                    SyntaxFactory.Literal(table.Name)
                                )
                            ),
                            SyntaxFactory.Block
                            (
                                SyntaxFactory.SingletonList<StatementSyntax>
                                (
                                    SyntaxFactory.ReturnStatement
                                    (
                                        SyntaxFactory.ObjectCreationExpression
                                        (
                                            SyntaxFactory.QualifiedName
                                            (
                                                SyntaxFactory.QualifiedName
                                                (
                                                    SyntaxFactory.IdentifierName(configuration.Namespace),
                                                    SyntaxFactory.IdentifierName("Dynamic")
                                                ),
                                                SyntaxFactory.IdentifierName(table.Name)
                                            )
                                        )
                                        .WithArgumentList
                                        (
                                            SyntaxFactory.ArgumentList()
                                        )
                                    )
                                )
                            )
                        )
                    );
            }

            // Must be the last statement
            ifStatements.Add
                (
                    SyntaxFactory.ReturnStatement(SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression))
                );

            return

                SyntaxFactory.CompilationUnit()
                .WithUsings
                (
                    SyntaxFactory.SingletonList<UsingDirectiveSyntax>
                    (
                        CompilerHelper.GetUsing("System")
                    )
                )
                .WithMembers
                (
                    SyntaxFactory.SingletonList<MemberDeclarationSyntax>
                    (
                        SyntaxFactory.NamespaceDeclaration
                        (
                            SyntaxFactory.QualifiedName
                            (
                                SyntaxFactory.IdentifierName(configuration.Namespace),
                                SyntaxFactory.IdentifierName("Dynamic")
                            )
                        )
                        .WithMembers
                        (
                            SyntaxFactory.SingletonList<MemberDeclarationSyntax>
                            (
                                SyntaxFactory.ClassDeclaration("InstanceCreator")
                                .WithModifiers
                                (
                                    SyntaxFactory.TokenList
                                    (
                                        new[]
                                        {
                                            SyntaxFactory.Token(SyntaxKind.PublicKeyword)
                                        }
                                    )
                                )
                                .WithBaseList
                                (
                                    SyntaxFactory.BaseList
                                    (
                                        SyntaxFactory.SingletonSeparatedList<BaseTypeSyntax>
                                        (
                                            SyntaxFactory.SimpleBaseType
                                            (
                                                SyntaxFactory.QualifiedName
                                                (
                                                    SyntaxFactory.QualifiedName
                                                    (
                                                        SyntaxFactory.IdentifierName("Yoktorm"),
                                                        SyntaxFactory.IdentifierName("Model")
                                                    ),
                                                    SyntaxFactory.IdentifierName("IInstanceCreator")
                                                )
                                            )
                                        )
                                    )
                                )
                                .WithMembers
                                (
                                    SyntaxFactory.SingletonList<MemberDeclarationSyntax>
                                    (
                                        SyntaxFactory.MethodDeclaration
                                        (
                                            SyntaxFactory.PredefinedType
                                            (
                                                SyntaxFactory.Token(SyntaxKind.ObjectKeyword)
                                            ),
                                            SyntaxFactory.Identifier("CreateInstance")
                                        )
                                        .WithModifiers
                                        (
                                            SyntaxFactory.TokenList
                                            (
                                                new[]
                                                {
                                                    SyntaxFactory.Token(SyntaxKind.PublicKeyword)
                                                }
                                            )
                                        )
                                        .WithParameterList
                                        (
                                            SyntaxFactory.ParameterList
                                            (
                                                SyntaxFactory.SingletonSeparatedList<ParameterSyntax>
                                                (
                                                    SyntaxFactory.Parameter
                                                    (
                                                        SyntaxFactory.Identifier("tableName")
                                                    )
                                                    .WithType
                                                    (
                                                        SyntaxFactory.PredefinedType
                                                        (
                                                            SyntaxFactory.Token(SyntaxKind.StringKeyword)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                        .WithBody
                                        (
                                            SyntaxFactory.Block
                                            (
                                                ifStatements
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                )
                .NormalizeWhitespace().SyntaxTree;
        }

        private SyntaxTree AddModel(Db.TableStructure table)
        {
            return null;
        }

        public byte[] Compile(Db.DatabaseStructure structure)
        {
            var syntaxTrees = new List<SyntaxTree>();
            syntaxTrees.Add(AddTestMethod());

            // Add tables
            foreach (var table in structure.Tables)
                syntaxTrees.Add(AddModel(table));

            syntaxTrees.Add(CreateInstanceCreator(structure));

            // Assert references
            if (configuration.References == null)
                configuration.References = new List<Assembly>();

            // Load references
            List<MetadataReference> references = configuration.References.Select(item => MetadataReference.CreateFromFile(item.Location)).ToList<MetadataReference>();

            // Add everything from the current app-domain
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    if (!configuration.References.Contains(asm) && !string.IsNullOrWhiteSpace(asm.Location))
                    {
                        references.Add(MetadataReference.CreateFromFile(asm.Location));
                    }
                }
                catch
                {
                    Console.WriteLine("Skip: assembly");
                }
            }

            // Set compiler options
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: configuration.AssemblyName,
                syntaxTrees: syntaxTrees.ToArray(),
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                // Emit/compile code and embedd
                var result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);


                    foreach (Diagnostic diagnostic in failures)
                    {
                        throw new Exception($"{diagnostic.GetMessage()} :: {diagnostic.Id}");
                    }
                }
                else
                {
                    // Reset stream
                    ms.Seek(0, SeekOrigin.Begin);
                    return ms.ToArray();
                }
            }

            return null;
        }
    }
}
