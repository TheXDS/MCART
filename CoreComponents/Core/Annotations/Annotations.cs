﻿/* MIT License

Copyright (c) 2016 JetBrains http://www.jetbrains.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */

using System;
using static System.AttributeTargets;

#pragma warning disable 1591
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming

namespace TheXDS.MCART.Annotations
{
    /// <inheritdoc />
    /// <summary>
    ///     Indica que el valor del elemento marcado podría ser 
    ///     <see langword="null"/> algunas veces, por lo que es necesario
    ///     comprobar si el valor es <see langword="null"/> antes de
    ///     utilizarlo.
    /// </summary>
    /// <example>
    ///     Este ejemplo muestra el uso del atributo
    ///     <see cref="CanBeNullAttribute"/> soportado por ReSharper en C# 7.3
    ///     y anteriores, así como todas las versiones de VB y opcionalmente C#
    ///     8.0 y posteriores cuando no se habilite el soporte de tipos de
    ///     referencia nulabes a nivel de lenguaje.
    ///     <code language="cs" source="..\..\Documentation\Examples\Annotations\Annotations.cs"
    ///         region="CanBeNullAttributeExample" />
    ///     <code language="vb" source="..\..\Documentation\Examples\Annotations\Annotations.vb"
    ///         region="CanBeNullAttributeExample" />
    /// </example>
    /// <remarks>
    ///     Este atributo puede colocarse en métodos, parámetros, propiedades,
    ///     delegados, campos, eventos, clases, interfaces y parámetros genéricos.
    /// </remarks>
    [AttributeUsage(
        Method | Parameter | Property | AttributeTargets.Delegate |
        Field | Event | Class | Interface | GenericParameter)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica que el valor del elemento marcado nunca puede ser
    ///     <see langword="null"/>.
    /// </summary>
    /// <example>
    ///     Este ejemplo muestra el uso del atributo
    ///     <see cref="NotNullAttribute"/> soportado por ReSharper en C# 7.3
    ///     y anteriores, así como todas las versiones de VB y opcionalmente C#
    ///     8.0 y posteriores cuando no se habilite el soporte de tipos de
    ///     referencia nulabes a nivel de lenguaje.
    ///     <code language="cs" source="..\..\Documentation\Examples\Annotations\Annotations.cs"
    ///         region="NotNullAttributeExample" />
    ///     <code language="vb" source="..\..\Documentation\Examples\Annotations\Annotations.vb"
    ///         region="NotNullAttributeExample" />
    /// </example>
    /// <remarks>
    ///     Este atributo puede colocarse en métodos, parámetros, propiedades,
    ///     delegados, campos, eventos, clases, interfaces y parámetros genéricos.
    /// </remarks>
    [AttributeUsage(
        Method | Parameter | Property | AttributeTargets.Delegate |
        Field | Event | Class | Interface | GenericParameter)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Can be applied to symbols of types derived from IEnumerable as well as to symbols of Task
    ///     and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    ///     or of the Lazy.Value property can never be null.
    /// </summary>
    /// <remarks>
    ///     Este atributo puede colocarse en métodos, parámetros, propiedades, delegados y campos.
    /// </remarks>
    [AttributeUsage(
        Method | Parameter | Property |
        AttributeTargets.Delegate | Field)]
    public sealed class ItemNotNullAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Can be applied to symbols of types derived from IEnumerable as well as to symbols of Task
    ///     and Lazy classes to indicate that the value of a collection item, of the Task.Result property
    ///     or of the Lazy.Value property can be null.
    /// </summary>
    /// <remarks>
    ///     Este atributo puede colocarse en métodos, parámetros, propiedades, delegados y campos.
    /// </remarks>
    [AttributeUsage(
        Method | Parameter | Property |
        AttributeTargets.Delegate | Field)]
    public sealed class ItemCanBeNullAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indica que el método marcado construye cadenas por medio del patrón
    ///     de formateo junto con argumentos opcionales.
    ///     El nombre del parámetro que contiene el formato de cadena deberá
    ///     ser pasado en el constructor. La cadena de formato debe estar en
    ///     un formato como el de
    ///     <see cref="M:System.String.Format(System.IFormatProvider,System.String,System.Object[])"/>.
    /// </summary>
    /// <example>
    ///     Este ejemplo muestra el uso del atributo
    ///     <see cref="StringFormatMethodAttribute"/> soportado por ReSharper.
    ///     <code language="cs" source="..\..\Documentation\Examples\Annotations\Annotations.cs"
    ///         region="StringFormatMethodAttributeExample" />
    ///     <code language="vb" source="..\..\Documentation\Examples\Annotations\Annotations.vb"
    ///         region="StringFormatMethodAttributeExample" />
    /// </example>
    /// <remarks>
    ///     Este atributo puede colocarse en constructores, métodos, propiedades y delegados.
    /// </remarks>
    [AttributeUsage(
        Constructor | Method |
        Property | AttributeTargets.Delegate)]
    public sealed class StringFormatMethodAttribute : Attribute
    {
        [NotNull] public string FormatParameterName { get; }

        /// <summary>
        ///     Indica que el método marcado construye cadenas por medio del patrón
        ///     de formateo junto con argumentos opcionales.
        /// </summary>
        /// <param name="formatParameterName">
        ///     Especifica cual parámetro del método anotado deberá ser tratado
        ///     como una cadena de formato.
        /// </param>        
        public StringFormatMethodAttribute([NotNull] string formatParameterName)
        {
            FormatParameterName = formatParameterName;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     For a parameter that is expected to be one of the limited set of values.
    ///     Specify fields of which type should be used as values for this parameter.
    /// </summary>
    [AttributeUsage(
        Parameter | Property | Field,
        AllowMultiple = true)]
    public sealed class ValueProviderAttribute : Attribute
    {
        [NotNull] public string Name { get; }

        public ValueProviderAttribute([NotNull] string name)
        {
            Name = name;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that the function argument should be string literal and match one
    ///     of the parameters of the caller function. For example, ReSharper annotates
    ///     the parameter of <see cref="T:System.ArgumentNullException" />.
    /// </summary>
    /// <example>
    ///     <code>
    /// void Foo(string param) {
    ///   if (param == null)
    ///     throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(Parameter)]
    public sealed class InvokerParameterNameAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that the method is contained in a type that implements
    ///     <c>System.ComponentModel.INotifyPropertyChanged</c> interface and this method
    ///     is used to notify that some property value changed.
    /// </summary>
    /// <remarks>
    ///     The method should be non-static and conform to one of the supported signatures:
    ///     <list>
    ///         <item>
    ///             <c>NotifyChanged(string)</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged(params string[])</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged{T}(Expression{Func{T}})</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged{T,U}(Expression{Func{T,U}})</c>
    ///         </item>
    ///         <item>
    ///             <c>SetProperty{T}(ref T, T, string)</c>
    ///         </item>
    ///     </list>
    /// </remarks>
    /// <example>
    ///     <code>
    ///  public class Foo : INotifyPropertyChanged {
    ///    public event PropertyChangedEventHandler PropertyChanged;
    ///    [NotifyPropertyChangedInvocator]
    ///    protected virtual void NotifyChanged(string propertyName) { ... }
    ///    string _name;
    ///    public string Name {
    ///      get { return _name; }
    ///      set { _name = value; NotifyChanged("LastName"); /* Warning */ }
    ///    }
    ///  }
    ///  </code>
    ///     Examples of generated notifications:
    ///     <list>
    ///         <item>
    ///             <c>NotifyChanged("Property")</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged(() =&gt; Property)</c>
    ///         </item>
    ///         <item>
    ///             <c>NotifyChanged((VM x) =&gt; x.Property)</c>
    ///         </item>
    ///         <item>
    ///             <c>SetProperty(ref myField, value, "Property")</c>
    ///         </item>
    ///     </list>
    /// </example>
    [AttributeUsage(Method)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
    {
        [CanBeNull] public string ParameterName { get; }

        public NotifyPropertyChangedInvocatorAttribute()
        {
        }

        public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
        {
            ParameterName = parameterName;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Describes dependency between method input and output.
    /// </summary>
    /// <syntax>
    ///     <p>Function Definition Table syntax:</p>
    ///     <list>
    ///         <item>FDT      ::= FDTRow [;FDTRow]*</item>
    ///         <item>FDTRow   ::= Input =&gt; Output | Output &lt;= Input</item>
    ///         <item>Input    ::= ParameterName: Value [, Input]*</item>
    ///         <item>Output   ::= [ParameterName: Value]* {halt|stop|void|nothing|Value}</item>
    ///         <item>Value    ::= true | false | null | notnull | canbenull</item>
    ///     </list>
    ///     If method has single input parameter, it's name could be omitted.<br />
    ///     Using <c>halt</c> (or <c>void</c>/<c>nothing</c>, which is the same) for method output
    ///     means that the methods doesn't return normally (throws or terminates the process).<br />
    ///     Value <c>canbenull</c> is only applicable for output parameters.<br />
    ///     You can use multiple <c>[ContractAnnotation]</c> for each FDT row, or use single attribute
    ///     with rows separated by semicolon. There is no notion of order rows, all rows are checked
    ///     for applicability and applied per each program state tracked by R# analysis.<br />
    /// </syntax>
    /// <examples>
    ///     <list>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("=&gt; halt")]
    /// public void TerminationMethod()
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("halt &lt;= condition: false")]
    /// public void Assert(bool condition, string text) // regular assertion method
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("s:null =&gt; true")]
    /// public bool IsNullOrEmpty(string s) // string.IsNullOrEmpty()
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// // A method that returns null if the parameter is null,
    /// // and not null if the parameter is not null
    /// [ContractAnnotation("null =&gt; null; notnull =&gt; notnull")]
    /// public object Transform(object data) 
    /// </code>
    ///         </item>
    ///         <item>
    ///             <code>
    /// [ContractAnnotation("=&gt; true, result: notnull; =&gt; false, result: null")]
    /// public bool TryParse(string s, out Person result)
    /// </code>
    ///         </item>
    ///     </list>
    /// </examples>
    [AttributeUsage(Method, AllowMultiple = true)]
    public sealed class ContractAnnotationAttribute : Attribute
    {
        [NotNull] public string Contract { get; }

        public bool ForceFullStates { get; }

        public ContractAnnotationAttribute([NotNull] string contract)
            : this(contract, false)
        {
        }

        public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
        {
            Contract = contract;
            ForceFullStates = forceFullStates;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that marked element should be localized or not.
    /// </summary>
    /// <example>
    ///     <code>
    /// [LocalizationRequiredAttribute(true)]
    /// class Foo {
    ///   string str = "my string"; // Warning: Localizable string
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(All)]
    public sealed class LocalizationRequiredAttribute : Attribute
    {
        public bool Required { get; }

        public LocalizationRequiredAttribute() : this(true)
        {
        }

        public LocalizationRequiredAttribute(bool required)
        {
            Required = required;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that the value of the marked type (or its derivatives)
    ///     cannot be compared using '==' or '!=' operators and <c>Equals()</c>
    ///     should be used instead. However, using '==' or '!=' for comparison
    ///     with <see langword="null"/> is always permitted.
    /// </summary>
    /// <example>
    ///     <code>
    /// [CannotApplyEqualityOperator]
    /// class NoEquality { }
    /// class UsesNoEquality {
    ///   void Test() {
    ///     var ca1 = new NoEquality();
    ///     var ca2 = new NoEquality();
    ///     if (ca1 != null) { // OK
    ///       bool condition = ca1 == ca2; // Warning
    ///     }
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(Interface | Class | Struct)]
    public sealed class CannotApplyEqualityOperatorAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     When applied to a target attribute, specifies a requirement for any type marked
    ///     with the target attribute to implement or inherit specific type or types.
    /// </summary>
    /// <example>
    ///     <code>
    /// [BaseTypeRequired(typeof(IComponent)] // Specify requirement
    /// class ComponentAttribute : Attribute { }
    /// [Component] // ComponentAttribute requires implementing IComponent interface
    /// class MyComponent : IComponent { }
    /// </code>
    /// </example>
    [AttributeUsage(Class, AllowMultiple = true)]
    [BaseTypeRequired(typeof(Attribute))]
    public sealed class BaseTypeRequiredAttribute : Attribute
    {
        [NotNull] public Type BaseType { get; }

        public BaseTypeRequiredAttribute([NotNull] Type baseType)
        {
            BaseType = baseType;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that the marked symbol is used implicitly (e.g. via reflection, in external library),
    ///     so this symbol will not be marked as unused (as well as by other usage inspections).
    /// </summary>
    [AttributeUsage(All)]
    public sealed class UsedImplicitlyAttribute : Attribute
    {
        public ImplicitUseTargetFlags TargetFlags { get; }

        public ImplicitUseKindFlags UseKindFlags { get; }

        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Should be used on attributes and causes ReSharper to not mark symbols marked with such attributes
    ///     as unused (as well as by other usage inspections)
    /// </summary>
    [AttributeUsage(Class | GenericParameter)]
    public sealed class MeansImplicitUseAttribute : Attribute
    {
        [UsedImplicitly] public ImplicitUseTargetFlags TargetFlags { get; private set; }

        [UsedImplicitly] public ImplicitUseKindFlags UseKindFlags { get; private set; }

        public MeansImplicitUseAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
        {
        }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default)
        {
        }

        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }
    }

    [Flags]
    public enum ImplicitUseKindFlags
    {
        Default = Access | Assign | InstantiatedWithFixedConstructorSignature,

        /// <summary>Only entity marked with attribute considered used.</summary>
        Access = 1,

        /// <summary>Indicates implicit assignment to a member.</summary>
        Assign = 2,

        /// <summary>
        ///     Indicates implicit instantiation of a type with fixed constructor signature.
        ///     That means any unused constructor parameters won't be reported as such.
        /// </summary>
        InstantiatedWithFixedConstructorSignature = 4,

        /// <summary>Indicates implicit instantiation of a type.</summary>
        InstantiatedNoFixedConstructorSignature = 8
    }

    /// <summary>
    ///     Specify what is considered used implicitly when marked
    ///     with <see cref="MeansImplicitUseAttribute" /> or <see cref="UsedImplicitlyAttribute" />.
    /// </summary>
    [Flags]
    public enum ImplicitUseTargetFlags
    {
        Default = Itself,
        Itself = 1,

        /// <summary>Members of entity marked with attribute are considered used.</summary>
        Members = 2,

        /// <summary>Entity marked with attribute and all its members considered used.</summary>
        WithMembers = Itself | Members
    }

    /// <inheritdoc />
    /// <summary>
    ///     This attribute is intended to mark publicly available API
    ///     which should not be removed and so is treated as used.
    /// </summary>
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public sealed class PublicAPIAttribute : Attribute
    {
        [CanBeNull] public string Comment { get; }

        public PublicAPIAttribute()
        {
        }

        public PublicAPIAttribute([NotNull] string comment)
        {
            Comment = comment;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Tells code analysis engine if the parameter is completely handled when the invoked method is on stack.
    ///     If the parameter is a delegate, indicates that delegate is executed while the method is executed.
    ///     If the parameter is an enumerable, indicates that it is enumerated while the method is executed.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class InstantHandleAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that a method does not make any observable state changes.
    ///     The same as <c>System.Diagnostics.Contracts.PureAttribute</c>.
    /// </summary>
    /// <example>
    ///     <code>
    /// [Pure] int Multiply(int x, int y) =&gt; x * y;
    /// void M() {
    ///   Multiply(123, 42); // Waring: Return value of pure method is not used
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(Method)]
    public sealed class PureAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that the return value of method invocation must be used.
    /// </summary>
    [AttributeUsage(Method)]
    public sealed class MustUseReturnValueAttribute : Attribute
    {
        [CanBeNull] public string Justification { get; }

        public MustUseReturnValueAttribute()
        {
        }

        public MustUseReturnValueAttribute([NotNull] string justification)
        {
            Justification = justification;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates the type member or parameter of some type, that should be used instead of all other ways
    ///     to get the value that type. This annotation is useful when you have some "context" value evaluated
    ///     and stored somewhere, meaning that all other ways to get this value must be consolidated with existing one.
    /// </summary>
    /// <example>
    ///     <code>
    /// class Foo {
    ///   [ProvidesContext] IBarService _barService = ...;
    ///   void ProcessNode(INode node) {
    ///     DoSomething(node, node.GetGlobalServices().Bar);
    ///     //              ^ Warning: use value of '_barService' field
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(
        Field | Property | Parameter | Method |
        Class | Interface | Struct | GenericParameter)]
    public sealed class ProvidesContextAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that a parameter is a path to a file or a folder within a web project.
    ///     Path can be relative or absolute, starting from web root (~).
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class PathReferenceAttribute : Attribute
    {
        [CanBeNull] public string BasePath { get; }

        public PathReferenceAttribute()
        {
        }

        public PathReferenceAttribute([NotNull] [PathReference] string basePath)
        {
            BasePath = basePath;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     An extension method marked with this attribute is processed by ReSharper code completion
    ///     as a 'Source Template'. When extension method is completed over some expression, it's source code
    ///     is automatically expanded like a template at call site.
    /// </summary>
    /// <remarks>
    ///     Template method body can contain valid source code and/or special comments starting with '$'.
    ///     Text inside these comments is added as source code when the template is applied. Template parameters
    ///     can be used either as additional method parameters or as identifiers wrapped in two '$' signs.
    ///     Use the <see cref="T:TheXDS.MCART.Annotations.MacroAttribute" /> attribute to specify macros for parameters.
    /// </remarks>
    /// <example>
    ///     In this example, the 'forEach' method is a source template available over all values
    ///     of enumerable types, producing ordinary C# 'foreach' statement and placing caret inside block:
    ///     <code>
    /// [SourceTemplate]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; xs) {
    ///   foreach (var x in xs) {
    ///      //$ $END$
    ///   }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(Method)]
    public sealed class SourceTemplateAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Allows specifying a macro for a parameter of a
    ///     <see cref="T:TheXDS.MCART.Annotations.SourceTemplateAttribute">source template</see>.
    /// </summary>
    /// <remarks>
    ///     You can apply the attribute on the whole method or on any of its additional parameters. The macro expression
    ///     is defined in the <see cref="P:TheXDS.MCART.Annotations.MacroAttribute.Expression" /> property. When applied on a
    ///     method, the target
    ///     template parameter is defined in the <see cref="P:TheXDS.MCART.Annotations.MacroAttribute.Target" /> property. To
    ///     apply the macro silently
    ///     for the parameter, set the <see cref="P:TheXDS.MCART.Annotations.MacroAttribute.Editable" /> property value = -1.
    /// </remarks>
    /// <example>
    ///     Applying the attribute on a source template method:
    ///     <code>
    /// [SourceTemplate, Macro(Target = "item", Expression = "suggestVariableName()")]
    /// public static void forEach&lt;T&gt;(this IEnumerable&lt;T&gt; collection) {
    ///   foreach (var item in collection) {
    ///     //$ $END$
    ///   }
    /// }
    /// </code>
    ///     Applying the attribute on a template method parameter:
    ///     <code>
    /// [SourceTemplate]
    /// public static void something(this Entity x, [Macro(Expression = "guid()", Editable = -1)] string newguid) {
    ///   /*$ var $x$Id = "$newguid$" + x.ToString();
    ///   x.DoSomething($x$Id); */
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(Parameter | Method, AllowMultiple = true)]
    public sealed class MacroAttribute : Attribute
    {
        /// <summary>
        ///     Allows specifying which occurrence of the target parameter becomes editable when the template is deployed.
        /// </summary>
        /// <remarks>
        ///     If the target parameter is used several times in the template, only one occurrence becomes editable;
        ///     other occurrences are changed synchronously. To specify the zero-based index of the editable occurrence,
        ///     use values >= 0. To make the parameter non-editable when the template is expanded, use -1.
        /// </remarks>
        /// >
        public int Editable { get; set; }

        /// <summary>
        ///     Allows specifying a macro that will be executed for a <see cref="SourceTemplateAttribute">source template</see>
        ///     parameter when the template is expanded.
        /// </summary>
        [CanBeNull]
        public string Expression { get; set; }

        /// <summary>
        ///     Identifies the target parameter of a <see cref="SourceTemplateAttribute">source template</see> if the
        ///     <see cref="MacroAttribute" /> is applied on a template method.
        /// </summary>
        [CanBeNull]
        public string Target { get; set; }
    }

    [AttributeUsage(Assembly | Field | Property, AllowMultiple = true)]
    public sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
    {
        [NotNull] public string Format { get; }

        public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }
    }

    [AttributeUsage(Assembly | Field | Property, AllowMultiple = true)]
    public sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
    {
        [NotNull] public string Format { get; }

        public AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }
    }

    [AttributeUsage(Assembly | Field | Property, AllowMultiple = true)]
    public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
    {
        [NotNull] public string Format { get; }

        public AspMvcAreaViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }
    }

    [AttributeUsage(Assembly | Field | Property, AllowMultiple = true)]
    public sealed class AspMvcMasterLocationFormatAttribute : Attribute
    {
        [NotNull] public string Format { get; }

        public AspMvcMasterLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }
    }

    [AttributeUsage(Assembly | Field | Property, AllowMultiple = true)]
    public sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
    {
        [NotNull] public string Format { get; }

        public AspMvcPartialViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }
    }

    [AttributeUsage(Assembly | Field | Property, AllowMultiple = true)]
    public sealed class AspMvcViewLocationFormatAttribute : Attribute
    {
        [NotNull] public string Format { get; }

        public AspMvcViewLocationFormatAttribute([NotNull] string format)
        {
            Format = format;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC action. If applied to a method, the MVC action name is calculated
    ///     implicitly from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(Parameter | Method)]
    public sealed class AspMvcActionAttribute : Attribute
    {
        [CanBeNull] public string AnonymousProperty { get; }

        public AspMvcActionAttribute()
        {
        }

        public AspMvcActionAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC area.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AspMvcAreaAttribute : Attribute
    {
        [CanBeNull] public string AnonymousProperty { get; }

        public AspMvcAreaAttribute()
        {
        }

        public AspMvcAreaAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is
    ///     an MVC controller. If applied to a method, the MVC controller name is calculated
    ///     implicitly from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.ChildActionExtensions.RenderAction(HtmlHelper, String, String)</c>.
    /// </summary>
    [AttributeUsage(Parameter | Method)]
    public sealed class AspMvcControllerAttribute : Attribute
    {
        [CanBeNull] public string AnonymousProperty { get; }

        public AspMvcControllerAttribute()
        {
        }

        public AspMvcControllerAttribute([NotNull] string anonymousProperty)
        {
            AnonymousProperty = anonymousProperty;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC Master. Use this attribute
    ///     for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, String)</c>.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AspMvcMasterAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC model type. Use this attribute
    ///     for custom wrappers similar to <c>System.Web.Mvc.Controller.View(String, Object)</c>.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AspMvcModelTypeAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter is an MVC
    ///     partial view. If applied to a method, the MVC partial view name is calculated implicitly
    ///     from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(Parameter | Method)]
    public sealed class AspMvcPartialViewAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. Allows disabling inspections for MVC views within a class or a method.
    /// </summary>
    [AttributeUsage(Class | Method)]
    public sealed class AspMvcSuppressViewErrorAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC display template.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.DisplayExtensions.DisplayForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AspMvcDisplayTemplateAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC editor template.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Html.EditorExtensions.EditorForModel(HtmlHelper, String)</c>.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AspMvcEditorTemplateAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. Indicates that a parameter is an MVC template.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.ComponentModel.DataAnnotations.UIHintAttribute(System.String)</c>.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AspMvcTemplateAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC view component. If applied to a method, the MVC view name is calculated implicitly
    ///     from the context. Use this attribute for custom wrappers similar to
    ///     <c>System.Web.Mvc.Controller.View(Object)</c>.
    /// </summary>
    [AttributeUsage(Parameter | Method)]
    public sealed class AspMvcViewAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC view component name.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AspMvcViewComponentAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    ///     is an MVC view component view. If applied to a method, the MVC view component view name is default.
    /// </summary>
    [AttributeUsage(Parameter | Method)]
    public sealed class AspMvcViewComponentViewAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     ASP.NET MVC attribute. When applied to a parameter of an attribute,
    ///     indicates that this parameter is an MVC action name.
    /// </summary>
    /// <example>
    ///     <code>
    /// [ActionName("Foo")]
    /// public ActionResult Credential(string returnUrl) {
    ///   ViewBag.ReturnUrl = Url.Action("Foo"); // OK
    ///   return RedirectToAction("Bar"); // Error: Cannot resolve action
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(Parameter | Property)]
    public sealed class AspMvcActionSelectorAttribute : Attribute
    {
    }

    [AttributeUsage(Parameter | Property | Field)]
    public sealed class HtmlElementAttributesAttribute : Attribute
    {
        [CanBeNull] public string Name { get; }

        public HtmlElementAttributesAttribute()
        {
        }

        public HtmlElementAttributesAttribute([NotNull] string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(Parameter | Field | Property)]
    public sealed class HtmlAttributeValueAttribute : Attribute
    {
        [NotNull] public string Name { get; }

        public HtmlAttributeValueAttribute([NotNull] string name)
        {
            Name = name;
        }
    }

    /// <inheritdoc />
    /// <summary>
    ///     Razor attribute. Indicates that a parameter or a method is a Razor section.
    ///     Use this attribute for custom wrappers similar to
    ///     <c>System.Web.WebPages.WebPageBase.RenderSection(String)</c>.
    /// </summary>
    [AttributeUsage(Parameter | Method)]
    public sealed class RazorSectionAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates how method, constructor invocation or property access
    ///     over collection type affects content of the collection.
    /// </summary>
    [AttributeUsage(Method | Constructor | Property)]
    public sealed class CollectionAccessAttribute : Attribute
    {
        public CollectionAccessType CollectionAccessType { get; }

        public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
        {
            CollectionAccessType = collectionAccessType;
        }
    }

    [Flags]
    public enum CollectionAccessType
    {
        /// <summary>Method does not use or modify content of the collection.</summary>
        None = 0,

        /// <summary>Method only reads content of the collection but does not modify it.</summary>
        Read = 1,

        /// <summary>Method can change content of the collection but does not add new elements.</summary>
        ModifyExistingContent = 2,

        /// <summary>Method can add new elements to the collection.</summary>
        UpdatedContent = ModifyExistingContent | 4
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that the marked method is assertion method, i.e. it halts control flow if
    ///     one of the conditions is satisfied. To set the condition, mark one of the parameters with
    ///     <see cref="T:TheXDS.MCART.Annotations.AssertionConditionAttribute" /> attribute.
    /// </summary>
    [AttributeUsage(Method)]
    public sealed class AssertionMethodAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates the condition parameter of the assertion method. The method itself should be
    ///     marked by <see cref="T:TheXDS.MCART.Annotations.AssertionMethodAttribute" /> attribute. The mandatory argument of
    ///     the attribute is the assertion type.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class AssertionConditionAttribute : Attribute
    {
        public AssertionConditionType ConditionType { get; }

        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            ConditionType = conditionType;
        }
    }

    /// <summary>
    ///     Specifies assertion type. If the assertion method argument satisfies the condition,
    ///     then the execution continues. Otherwise, execution is assumed to be halted.
    /// </summary>
    public enum AssertionConditionType
    {
        /// <summary>Marked parameter should be evaluated to true.</summary>
        IS_TRUE = 0,

        /// <summary>Marked parameter should be evaluated to false.</summary>
        IS_FALSE = 1,

        /// <summary>Marked parameter should be evaluated to null value.</summary>
        IS_NULL = 2,

        /// <summary>Marked parameter should be evaluated to not null value.</summary>
        IS_NOT_NULL = 3
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that the marked method unconditionally terminates control flow execution.
    ///     For example, it could unconditionally throw exception.
    /// </summary>
    [Obsolete("Use [ContractAnnotation('=> halt')] instead")]
    [AttributeUsage(Method)]
    public sealed class TerminatesProgramAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that method is pure LINQ method, with postponed enumeration (like Enumerable.Select,
    ///     .Where). This annotation allows inference of [InstantHandle] annotation for parameters
    ///     of delegate type by analyzing LINQ method chains.
    /// </summary>
    [AttributeUsage(Method)]
    public sealed class LinqTunnelAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that IEnumerable, passed as parameter, is not enumerated.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class NoEnumerationAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Indicates that parameter is regular expression pattern.
    /// </summary>
    [AttributeUsage(Parameter)]
    public sealed class RegexPatternAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Prevents the Member Reordering feature from tossing members of the marked class.
    /// </summary>
    /// <remarks>
    ///     The attribute must be mentioned in your member reordering patterns
    /// </remarks>
    [AttributeUsage(
        Class | Interface | Struct | AttributeTargets.Enum)]
    public sealed class NoReorderAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     XAML attribute. Indicates the type that has <c>ItemsSource</c> property and should be treated
    ///     as <c>ItemsControl</c>-derived type, to enable inner items <c>DataContext</c> type resolve.
    /// </summary>
    [AttributeUsage(Class)]
    public sealed class XamlItemsControlAttribute : Attribute
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     XAML attribute. Indicates the property of some <c>BindingBase</c>-derived type, that
    ///     is used to bind some item of <c>ItemsControl</c>-derived type. This annotation will
    ///     enable the <c>DataContext</c> type resolve for XAML bindings for such properties.
    /// </summary>
    /// <remarks>
    ///     Property should have the tree ancestor of the <c>ItemsControl</c> type or
    ///     marked with the <see cref="T:TheXDS.MCART.Annotations.XamlItemsControlAttribute" /> attribute.
    /// </remarks>
    [AttributeUsage(Property)]
    public sealed class XamlItemBindingOfItemsControlAttribute : Attribute
    {
    }

    [AttributeUsage(Class, AllowMultiple = true)]
    public sealed class AspChildControlTypeAttribute : Attribute
    {
        [NotNull] public Type ControlType { get; }

        [NotNull] public string TagName { get; }

        public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
        {
            TagName = tagName;
            ControlType = controlType;
        }
    }

    [AttributeUsage(Property | Method)]
    public sealed class AspDataFieldAttribute : Attribute
    {
    }

    [AttributeUsage(Property | Method)]
    public sealed class AspDataFieldsAttribute : Attribute
    {
    }

    [AttributeUsage(Property)]
    public sealed class AspMethodPropertyAttribute : Attribute
    {
    }

    [AttributeUsage(Class, AllowMultiple = true)]
    public sealed class AspRequiredAttributeAttribute : Attribute
    {
        [NotNull] public string Attribute { get; }

        public AspRequiredAttributeAttribute([NotNull] string attribute)
        {
            Attribute = attribute;
        }
    }

    [AttributeUsage(Property)]
    public sealed class AspTypePropertyAttribute : Attribute
    {
        public bool CreateConstructorReferences { get; }

        public AspTypePropertyAttribute(bool createConstructorReferences)
        {
            CreateConstructorReferences = createConstructorReferences;
        }
    }

    [AttributeUsage(Assembly, AllowMultiple = true)]
    public sealed class RazorImportNamespaceAttribute : Attribute
    {
        [NotNull] public string Name { get; }

        public RazorImportNamespaceAttribute([NotNull] string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(Assembly, AllowMultiple = true)]
    public sealed class RazorInjectionAttribute : Attribute
    {
        [NotNull] public string FieldName { get; }

        [NotNull] public string Type { get; }

        public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
        {
            Type = type;
            FieldName = fieldName;
        }
    }

    [AttributeUsage(Assembly, AllowMultiple = true)]
    public sealed class RazorDirectiveAttribute : Attribute
    {
        [NotNull] public string Directive { get; }

        public RazorDirectiveAttribute([NotNull] string directive)
        {
            Directive = directive;
        }
    }

    [AttributeUsage(Assembly, AllowMultiple = true)]
    public sealed class RazorPageBaseTypeAttribute : Attribute
    {
        [NotNull] public string BaseType { get; }
        [CanBeNull] public string PageName { get; }

        public RazorPageBaseTypeAttribute([NotNull] string baseType)
        {
            BaseType = baseType;
        }

        public RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
        {
            BaseType = baseType;
            PageName = pageName;
        }
    }

    [AttributeUsage(Method)]
    public sealed class RazorHelperCommonAttribute : Attribute
    {
    }

    [AttributeUsage(Property)]
    public sealed class RazorLayoutAttribute : Attribute
    {
    }

    [AttributeUsage(Method)]
    public sealed class RazorWriteLiteralMethodAttribute : Attribute
    {
    }

    [AttributeUsage(Method)]
    public sealed class RazorWriteMethodAttribute : Attribute
    {
    }

    [AttributeUsage(Parameter)]
    public sealed class RazorWriteMethodParameterAttribute : Attribute
    {
    }
}