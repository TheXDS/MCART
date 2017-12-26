# RuleSets.NullEvalRule Method 
 _**\[This is preliminary documentation and is subject to change.\]**_

Crea una regla nula especial para ayudar a balancear los puntajes de evaluación.

**Namespace:**&nbsp;<a href="dbbe708a-6e0a-d3f8-20a0-94d530d6d526">MCART.Security.Password</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public static PwEvalRule NullEvalRule(
	PonderationLevel ponderation = PonderationLevel.AdverseNormal
)
```

**VB**<br />
``` VB
Public Shared Function NullEvalRule ( 
	Optional ponderation As PonderationLevel = PonderationLevel.AdverseNormal
) As PwEvalRule
```


#### Parameters
&nbsp;<dl><dt>ponderation (Optional)</dt><dd>Type: <a href="ce318a63-6b3d-2810-363f-809772c2773d">MCART.Security.Password.PonderationLevel</a><br />Ponderation.</dd></dl>

#### Return Value
Type: <a href="948e40e2-3627-ef3a-b8d7-9dab91b199f0">PwEvalRule</a><br />Regla de evaluación que devuelve valores constantes.

## See Also


#### Reference
<a href="12d592b5-8142-9905-8192-00037f77a515">RuleSets Class</a><br /><a href="dbbe708a-6e0a-d3f8-20a0-94d530d6d526">MCART.Security.Password Namespace</a><br />