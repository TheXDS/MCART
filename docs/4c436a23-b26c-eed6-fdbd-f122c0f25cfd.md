# IEasyChecksum.ComputeAsync Method (Stream)
 _**\[This is preliminary documentation and is subject to change.\]**_

Ejecuta la transformación del algoritmo de forma asíncrona y devuelve el Checksum / Hash de los datos como una colección de <a href="http://msdn2.microsoft.com/es-es/library/yyb1w04y" target="_blank">Byte</a>.

**Namespace:**&nbsp;<a href="60810d21-7cbc-628a-0d69-05538adbf155">MCART.Security.Checksum</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
Task<byte[]> ComputeAsync(
	Stream X
)
```

**VB**<br />
``` VB
Function ComputeAsync ( 
	X As Stream
) As Task(Of Byte())
```


#### Parameters
&nbsp;<dl><dt>X</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">System.IO.Stream</a><br /><a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">Stream</a> de entrada.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/es-es/library/dd321424" target="_blank">Task</a>(<a href="http://msdn2.microsoft.com/es-es/library/yyb1w04y" target="_blank">Byte</a>[])<br />Un <a href="http://msdn2.microsoft.com/es-es/library/dd321424" target="_blank">Task(TResult)</a> correspondiente al resultado de ejecutar el algoritmo con la información provista.

## See Also


#### Reference
<a href="91f7d2c9-3f1a-2c86-2521-c04ece8a3e0b">IEasyChecksum Interface</a><br /><a href="59499851-0182-eaa0-7bed-d92b91db3445">ComputeAsync Overload</a><br /><a href="60810d21-7cbc-628a-0d69-05538adbf155">MCART.Security.Checksum Namespace</a><br />