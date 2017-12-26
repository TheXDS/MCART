# TaskReporterPlugin.BeginNonStop Method (TimeSpan, Boolean)
 _**\[This is preliminary documentation and is subject to change.\]**_

Indica que una tarea que no se puede detener ha iniciado. Genera el evento <a href="3069d260-7f76-26ba-4e39-cda7f1c431ef">Begun</a>.

**Namespace:**&nbsp;<a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public void BeginNonStop(
	TimeSpan timeout,
	bool genTOutEx = false
)
```

**VB**<br />
``` VB
Public Sub BeginNonStop ( 
	timeout As TimeSpan,
	Optional genTOutEx As Boolean = false
)
```


#### Parameters
&nbsp;<dl><dt>timeout</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/269ew577" target="_blank">System.TimeSpan</a><br />\[Missing <param name="timeout"/> documentation for "M:MCART.Types.TaskReporter.TaskReporterPlugin.BeginNonStop(System.TimeSpan,System.Boolean)"\]</dd><dt>genTOutEx (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/a28wyd50" target="_blank">System.Boolean</a><br />\[Missing <param name="genTOutEx"/> documentation for "M:MCART.Types.TaskReporter.TaskReporterPlugin.BeginNonStop(System.TimeSpan,System.Boolean)"\]</dd></dl>

#### Implements
<a href="50cdf91c-3b8b-c21f-1f59-eefbbcfafb72">ITaskReporter.BeginNonStop(TimeSpan, Boolean)</a><br />

## See Also


#### Reference
<a href="2cca1eb3-f49c-080a-88d8-66137c07787e">TaskReporterPlugin Class</a><br /><a href="64b152ac-3fe7-3417-88eb-f5120c7954ec">BeginNonStop Overload</a><br /><a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter Namespace</a><br />