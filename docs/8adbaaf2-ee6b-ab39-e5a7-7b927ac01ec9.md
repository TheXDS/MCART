# ListUpdatingEventArgs(*T*) Class
 _**\[This is preliminary documentation and is subject to change.\]**_

Contiene información para el evento <a href="ebf74abd-f458-6598-5b77-2309a7beeb16">ListUpdating</a>.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;<a href="http://msdn2.microsoft.com/es-es/library/118wxtk3" target="_blank">System.EventArgs</a><br />&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://msdn2.microsoft.com/es-es/library/9ws52wzb" target="_blank">System.ComponentModel.CancelEventArgs</a><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MCART.Types.Extensions.ListUpdatingEventArgs(T)<br />
**Namespace:**&nbsp;<a href="a8e71047-44e0-7000-43f0-67a6f5b9758c">MCART.Types.Extensions</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public class ListUpdatingEventArgs<T> : CancelEventArgs

```

**VB**<br />
``` VB
Public Class ListUpdatingEventArgs(Of T)
	Inherits CancelEventArgs
```


#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Tipo de elementos de la lista.</dd></dl>&nbsp;
The ListUpdatingEventArgs(T) type exposes the following members.


## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/es-es/library/e1bcat2e" target="_blank">Cancel</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/9ws52wzb" target="_blank">CancelEventArgs</a>.)</td></tr></table>&nbsp;
<a href="#listupdatingeventargs(*t*)-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/bsc2ak47" target="_blank">Equals</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/4k87zsw7" target="_blank">Finalize</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/zdee4b3y" target="_blank">GetHashCode</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/dfwy45w9" target="_blank">GetType</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/7bxwbwt2" target="_blank">ToString</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr></table>&nbsp;
<a href="#listupdatingeventargs(*t*)-class">Back to Top</a>

## Fields
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public field](media/pubfield.gif "Public field")</td><td><a href="b7cbcde4-2376-5b22-a126-d8665f2f0265">Items</a></td><td>
Elementos afectados por la actualización.</td></tr><tr><td>![Public field](media/pubfield.gif "Public field")</td><td><a href="1f71d8ed-af81-120d-067e-95e19e24a167">UpdateType</a></td><td>
TIpo de actualización a realizar en el <a href="e472f890-0d94-e75b-9f29-f49cc04a830f">List(T)</a> que generó el evento.</td></tr></table>&nbsp;
<a href="#listupdatingeventargs(*t*)-class">Back to Top</a>

## Extension Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="266d0619-24e8-4bb1-eeac-82fa7c767fb6">GetAttr(T)()</a></td><td>Overloaded.  
Devuelve el atributo asociado a la declaración del objeto especificado.
 (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="266d0619-24e8-4bb1-eeac-82fa7c767fb6">GetAttr(T)()</a></td><td>Overloaded.   (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="23d8a4fc-d7b8-c950-fd60-5488d38ae883">HasAttr(T)()</a></td><td>Overloaded.  
Determina si un miembro posee un atributo definido.
 (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="23d8a4fc-d7b8-c950-fd60-5488d38ae883">HasAttr(T)()</a></td><td>Overloaded.   (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="203375c6-370f-f64c-5432-7536a7b7ebcc">HasAttr(T)(T)</a></td><td>Overloaded.  
Determina si un miembro posee un atributo definido.
 (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="203375c6-370f-f64c-5432-7536a7b7ebcc">HasAttr(T)(T)</a></td><td>Overloaded.   (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="196f8475-b677-a34d-59bf-35344814f977">Is(Object)</a></td><td>Overloaded.  
Determina si *obj1* es la misma instancia en *obj2*.
 (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="196f8475-b677-a34d-59bf-35344814f977">Is(Object)</a></td><td>Overloaded.   (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="df46cf0b-190b-ec6a-69df-c78f6a5797bf">IsEither(Object[])</a></td><td>Overloaded.  
Determina si un objeto es cualquiera de los indicados.
 (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="df46cf0b-190b-ec6a-69df-c78f6a5797bf">IsEither(Object[])</a></td><td>Overloaded.   (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="eefea649-60a0-7eb1-917a-075b273494b9">IsNeither(Object[])</a></td><td>Overloaded.  
Determina si un objeto no es ninguno de los indicados.
 (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="eefea649-60a0-7eb1-917a-075b273494b9">IsNeither(Object[])</a></td><td>Overloaded.   (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="544e32e7-8440-b023-8a1b-4e3542ae24f5">IsNot(Object)</a></td><td>Overloaded.  
Determina si *obj1* es una instancia diferente a *obj2*.
 (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr><tr><td>![Public Extension Method](media/pubextension.gif "Public Extension Method")</td><td><a href="544e32e7-8440-b023-8a1b-4e3542ae24f5">IsNot(Object)</a></td><td>Overloaded.   (Defined by <a href="bed01b44-1ba8-b02e-7f19-0855e84b8dbd">Objects</a>.)</td></tr></table>&nbsp;
<a href="#listupdatingeventargs(*t*)-class">Back to Top</a>

## See Also


#### Reference
<a href="a8e71047-44e0-7000-43f0-67a6f5b9758c">MCART.Types.Extensions Namespace</a><br />