# InterfaceNotImplementedException Constructor (String, Exception, Type)
 _**\[This is preliminary documentation and is subject to change.\]**_

Inicializa una nueva instancia de este objeto especificando el mensaje, la <a href="http://msdn2.microsoft.com/es-es/library/c18k6c59" target="_blank">Exception</a> y el tipo que generó esta excepción.

**Namespace:**&nbsp;<a href="36e6166c-cb29-ee06-1b8a-ebc61fae7b0a">MCART.Exceptions</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public InterfaceNotImplementedException(
	string message,
	Exception inner,
	Type T = null
)
```

**VB**<br />
``` VB
Public Sub New ( 
	message As String,
	inner As Exception,
	Optional T As Type = Nothing
)
```


#### Parameters
&nbsp;<dl><dt>message</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/s1wwdcbf" target="_blank">System.String</a><br />Un <a href="http://msdn2.microsoft.com/es-es/library/s1wwdcbf" target="_blank">String</a> que describe a la excepción.</dd><dt>inner</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/c18k6c59" target="_blank">System.Exception</a><br /><a href="http://msdn2.microsoft.com/es-es/library/c18k6c59" target="_blank">Exception</a> que es la causa de esta excepción.</dd><dt>T (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/42892f65" target="_blank">System.Type</a><br />Tipo que generó la excepción</dd></dl>

## See Also


#### Reference
<a href="1363a077-1b87-621e-2121-ffa23147e661">InterfaceNotImplementedException Class</a><br /><a href="c70e9e23-d680-1bd1-e17f-dfba0c44f62f">InterfaceNotImplementedException Overload</a><br /><a href="36e6166c-cb29-ee06-1b8a-ebc61fae7b0a">MCART.Exceptions Namespace</a><br />