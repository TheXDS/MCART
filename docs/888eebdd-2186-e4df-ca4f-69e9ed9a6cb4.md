# ChecksumPlugin.Compute Method (Stream)
 _**\[This is preliminary documentation and is subject to change.\]**_

Calcula una suma de verificación sobre un <a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">Stream</a>.

**Namespace:**&nbsp;<a href="60810d21-7cbc-628a-0d69-05538adbf155">MCART.Security.Checksum</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public virtual byte[] Compute(
	Stream X
)
```

**VB**<br />
``` VB
Public Overridable Function Compute ( 
	X As Stream
) As Byte()
```


#### Parameters
&nbsp;<dl><dt>X</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">System.IO.Stream</a><br /><a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">Stream</a> con la información a computar.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/es-es/library/yyb1w04y" target="_blank">Byte</a>[]<br />Un arreglo de bytes con la suma de verificación.

#### Implements
<a href="ab6aa3ea-c542-2839-1937-2b47de1b4d7d">IEasyChecksum.Compute(Stream)</a><br />

## See Also


#### Reference
<a href="d782770c-07c3-9534-00a9-6334d827cd7f">ChecksumPlugin Class</a><br /><a href="5b11736c-1081-e5a5-b886-86f325d740db">Compute Overload</a><br /><a href="60810d21-7cbc-628a-0d69-05538adbf155">MCART.Security.Checksum Namespace</a><br />