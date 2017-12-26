# IEnumerableExtensions.Pop(*T*) Method 
 _**\[This is preliminary documentation and is subject to change.\]**_

Devuelve el último elemento en la lista, quitándolo.

**Namespace:**&nbsp;<a href="a8e71047-44e0-7000-43f0-67a6f5b9758c">MCART.Types.Extensions</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public static T Pop<T>(
	this IEnumerable<T> a
)

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function Pop(Of T) ( 
	a As IEnumerable(Of T)
) As T
```


#### Parameters
&nbsp;<dl><dt>a</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/9eekhta0" target="_blank">System.Collections.Generic.IEnumerable</a>(*T*)<br />Lista de la cual obtener el elemento.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Tipo de elementos de la lista.</dd></dl>

#### Return Value
Type: *T*<br />El último elemento en la lista.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="http://msdn2.microsoft.com/es-es/library/9eekhta0" target="_blank">IEnumerable</a>(*T*). When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="b12b3254-391f-e729-a551-2fdb7baa0685">IEnumerableExtensions Class</a><br /><a href="a8e71047-44e0-7000-43f0-67a6f5b9758c">MCART.Types.Extensions Namespace</a><br />