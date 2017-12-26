# ITaskReporter.BeginNonStop Method (TimeSpan, Boolean)
 _**\[This is preliminary documentation and is subject to change.\]**_

Indica que una tarea que no se puede interrumpir ha dado inicio.

**Namespace:**&nbsp;<a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
void BeginNonStop(
	TimeSpan timeout,
	bool genTOutEx = false
)
```

**VB**<br />
``` VB
Sub BeginNonStop ( 
	timeout As TimeSpan,
	Optional genTOutEx As Boolean = false
)
```


#### Parameters
&nbsp;<dl><dt>timeout</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/269ew577" target="_blank">System.TimeSpan</a><br />Tiempo concedido a la tarea.</dd><dt>genTOutEx (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/a28wyd50" target="_blank">System.Boolean</a><br />Indica si se generará una excepción al terminarse el tiempo de espera.</dd></dl>

## See Also


#### Reference
<a href="33635590-5f82-4893-14af-1a5de20591b5">ITaskReporter Interface</a><br /><a href="0b1c5c2e-f5f4-3eae-6272-8fa251d5f9ab">BeginNonStop Overload</a><br /><a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter Namespace</a><br />