# Misc.DownloadHttp Method (Uri, Stream)
 _**\[This is preliminary documentation and is subject to change.\]**_

Descarga un archivo por medio de http y lo almacena en el <a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">Stream</a> provisto.

**Namespace:**&nbsp;<a href="c6445fcc-8709-dc3e-4d6b-f87f79cbd982">MCART.Networking</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public static void DownloadHttp(
	Uri uri,
	Stream stream
)
```

**VB**<br />
``` VB
Public Shared Sub DownloadHttp ( 
	uri As Uri,
	stream As Stream
)
```


#### Parameters
&nbsp;<dl><dt>uri</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/txt7706a" target="_blank">System.Uri</a><br />Url del archivo. Debe ser una ruta http válida.</dd><dt>stream</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">System.IO.Stream</a><br /><a href="http://msdn2.microsoft.com/es-es/library/8f86tw9e" target="_blank">Stream</a> en el cual se almacenará el archivo.</dd></dl>

## See Also


#### Reference
<a href="01881faa-5da8-f3c1-6dac-3498a8eed917">Misc Class</a><br /><a href="0bebcb60-4918-814c-0fc2-32267b923f6d">DownloadHttp Overload</a><br /><a href="c6445fcc-8709-dc3e-4d6b-f87f79cbd982">MCART.Networking Namespace</a><br />