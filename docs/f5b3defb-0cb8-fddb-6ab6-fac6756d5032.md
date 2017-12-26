# TaskReporterPlugin.Stop Method (ProgressEventArgs)
 _**\[This is preliminary documentation and is subject to change.\]**_

Genera el evento <a href="9979558a-42a8-6537-7934-5cd20c75fcaa">Stopped</a> al interrumpir una tarea.

**Namespace:**&nbsp;<a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public abstract void Stop(
	ProgressEventArgs e
)
```

**VB**<br />
``` VB
Public MustOverride Sub Stop ( 
	e As ProgressEventArgs
)
```


#### Parameters
&nbsp;<dl><dt>e</dt><dd>Type: <a href="ca737456-2d6f-7f13-63a9-5b5d228c5048">MCART.Types.TaskReporter.ProgressEventArgs</a><br />Argumentos del evento.</dd></dl>

#### Implements
<a href="19ebcd19-d0ef-cf26-691e-14c8ffc0c38e">ITaskReporter.Stop(ProgressEventArgs)</a><br />

## See Also


#### Reference
<a href="2cca1eb3-f49c-080a-88d8-66137c07787e">TaskReporterPlugin Class</a><br /><a href="65e6ee9b-bb84-996c-b069-03753ee93b15">Stop Overload</a><br /><a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter Namespace</a><br />