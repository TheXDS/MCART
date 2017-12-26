# CancelRequestedEventHandler Delegate
 _**\[This is preliminary documentation and is subject to change.\]**_

Indica a la tarea que se ha solicitado que se detenga

**Namespace:**&nbsp;<a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public delegate void CancelRequestedEventHandler(
	Object sender,
	CancelEventArgs e
)
```

**VB**<br />
``` VB
Public Delegate Sub CancelRequestedEventHandler ( 
	sender As Object,
	e As CancelEventArgs
)
```


#### Parameters
&nbsp;<dl><dt>sender</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">System.Object</a><br />Objeto que ha iniciado el evento</dd><dt>e</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/9ws52wzb" target="_blank">System.ComponentModel.CancelEventArgs</a><br />Parámetros del evento</dd></dl>

## Remarks
Este evento debe ser implementado por la clase que contiene a la tarea

## See Also


#### Reference
<a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter Namespace</a><br />