# TaskReporter Class
 _**\[This is preliminary documentation and is subject to change.\]**_

Clase base para objetos que puedan utilizarse para reportar el progreso de una operación o tarea.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;MCART.Types.TaskReporter.TaskReporter<br />&nbsp;&nbsp;&nbsp;&nbsp;<a href="33ab697e-a7c6-ba80-19b2-ef4705632f90">MCART.Types.TaskReporter.ConsoleTaskReporter</a><br />&nbsp;&nbsp;&nbsp;&nbsp;<a href="3110d67a-24e6-f37f-f20a-c43d9518a569">MCART.Types.TaskReporter.DummyTaskReporter</a><br />
**Namespace:**&nbsp;<a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public abstract class TaskReporter : ITaskReporter
```

**VB**<br />
``` VB
Public MustInherit Class TaskReporter
	Implements ITaskReporter
```

The TaskReporter type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="4e317878-1482-ff40-fbc9-ce50638197d7">TaskReporter</a></td><td>
Inicializa una nueva instancia de la clase TaskReporter.</td></tr></table>&nbsp;
<a href="#taskreporter-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="5150ee99-224b-5586-84a9-156efb671649">CancelPending</a></td><td>
Obtiene un valor que indica si existe una solicitud para deterner la tarea actualmente en ejecución.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="617445a4-a69a-7abb-369f-8ff1f1208d35">CurrentProgress</a></td><td>
Obtiene un valor que indica el progreso actual reportardo por la tarea.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="a324065e-fa48-7965-6f25-49f7a7e138fc">OnDuty</a></td><td>
Obtiene un valor que indica si se está ejecutando una tarea actualmente.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="68fd8463-efa0-7939-d53a-e3c42db75c6f">TimedOut</a></td><td>
Obtiene un valor que indica si la operación debe detenerse debido a que se ha agotado el tiempo de espera de la misma.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="af00f1a9-9fd4-44f5-c3ed-4e77f7429538">TimeLeft</a></td><td>
Obtiene la cantidad de tiempo disponible para finalizar la tarea.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="b65a2c99-a1b8-78cf-4d5c-815b2bfa80a9">Timeout</a></td><td>
Obtiene la cantidad de tiempo asignado para ejecutar la tarea.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="623b73a1-9976-d979-56ea-72b4ff136ad6">TStart</a></td><td>
Obtiene el instante en el que dio inicio la tarea en ejecución.</td></tr></table>&nbsp;
<a href="#taskreporter-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="210dc562-3f82-88bb-6448-82c11ac5c980">Begin()</a></td><td>
Genera el evento <a href="0c308070-92da-dcc7-e2fd-2913c32e7f23">Begun</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="f35b5f27-427e-0bc3-1f62-0dc53be5a601">Begin(TimeSpan, Boolean)</a></td><td>
Indica que una tarea se ha iniciado. Genera el evento <a href="0c308070-92da-dcc7-e2fd-2913c32e7f23">Begun</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="306293e8-2c42-564c-73e7-5814ddcf2f07">BeginNonStop()</a></td><td>
Genera el evento <a href="0c308070-92da-dcc7-e2fd-2913c32e7f23">Begun</a> para una tarea que no puede detenerse.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="834aa87e-94d9-e256-f598-2db150fc36f3">BeginNonStop(TimeSpan, Boolean)</a></td><td>
Indica que una tarea que no se puede detener ha iniciado. Genera el evento <a href="0c308070-92da-dcc7-e2fd-2913c32e7f23">Begun</a>.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="a1d4051d-7859-9b34-bbd4-c6a436c803f7">End</a></td><td>
Genera el evento <a href="04f09c04-b1e9-8520-79ef-bb0f98272ef9">Ended</a></td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="a5b0998d-9475-63e3-c4a5-dff46dfb17a0">EndWithError</a></td><td>
Genera el evento <a href="23526f7b-7026-1852-6cad-f91ed78bc96a">Error</a></td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/bsc2ak47" target="_blank">Equals</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/4k87zsw7" target="_blank">Finalize</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="a0c2b75b-1865-3f83-a8bb-a8ff9f55392a">For(Int32, ForAction, String, Boolean, Action, Action)</a></td><td>
Ejecuta un ciclo determinado por el delegado *forAct*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="f7b2379b-5431-3540-629d-4174f81889a3">For(Int32, Int32, ForAction, String, Boolean, Action, Action)</a></td><td>
Ejecuta un ciclo determinado por el delegado *forAct*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="5e5e35e2-826b-5a62-a121-3012e35f4c53">For(Int32, Int32, Int32, ForAction, String, Boolean, Action, Action)</a></td><td>
Ejecuta un ciclo determinado por el delegado *forAct*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="4c77d54b-e550-00c0-d3d3-f0b41fec9386">For(Int32, Int32, Int32, ForAction, String, Boolean, Action, Action, ITaskReporter)</a></td><td>
Ejecuta un ciclo determinado por el delegado *forAct*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="9ae1ddba-e172-a6e1-10d2-d541152191a2">ForEach(T)(IEnumerable(T), ForEachAction(T), String, Boolean, Action, Action)</a></td><td>
Ejecuta un ciclo `For Each` determinado por el delegado *forEachAct*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")![Static member](media/static.gif "Static member")</td><td><a href="27f5aff6-dbbf-c569-5f88-094ecc69b5cd">ForEach(T)(IEnumerable(T), ForEachAction(T), String, Boolean, Action, Action, ITaskReporter)</a></td><td>
Ejecuta un ciclo `For Each` determinado por el delegado *forEachAct*.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/zdee4b3y" target="_blank">GetHashCode</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/dfwy45w9" target="_blank">GetType</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="da5f74be-c96e-494e-59a4-dcaedba86bf3">RaiseBegun</a></td><td>
Genera el evento <a href="0c308070-92da-dcc7-e2fd-2913c32e7f23">Begun</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="647fa337-f88f-fe52-810f-7fbacd0afd97">RaiseCancelRequested</a></td><td>
Genera el evento <a href="32f89ac9-d1ab-e39b-1209-838a4c7bba7d">CancelRequested</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="24993208-05bf-d358-8002-7620190da21d">RaiseEnded</a></td><td>
Genera el evento <a href="04f09c04-b1e9-8520-79ef-bb0f98272ef9">Ended</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="9d96d1f9-03d3-beaa-6a94-518fab3bcd70">RaiseError</a></td><td>
Genera el evento <a href="23526f7b-7026-1852-6cad-f91ed78bc96a">Error</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="f9dd4f66-63d6-ee09-af16-85235f39dfd8">RaiseReporting</a></td><td>
Genera el evento <a href="e870628f-1461-b87d-8212-ba57342472c8">Reporting</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="b56fabf5-6ddf-7379-e6e9-9c291c7b3754">RaiseStopped</a></td><td>
Genera el evento <a href="36c5202b-56a4-851b-6afc-492e5dfc5aa7">Stopped</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="7a9ee362-655c-e3d9-80a4-7aa62e083c3a">RaiseTimeout</a></td><td>
Genera el evento <a href="f16407b7-dfa4-7bff-d6b2-38a6da42d45c">TaskTimeout</a> desde una clase derivada.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="2a83e997-acdd-d2ec-a75e-f00469e97359">Report(String)</a></td><td>
Genera el evento <a href="e870628f-1461-b87d-8212-ba57342472c8">Reporting</a> desde una tarea.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="b65dd9a5-eab3-f686-8ca7-275619935090">Report(ProgressEventArgs)</a></td><td>
Genera el evento <a href="e870628f-1461-b87d-8212-ba57342472c8">Reporting</a> desde una tarea.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="3186a9d6-39b3-030b-1fc5-3ae1df266ed4">Report(Nullable(Single), String)</a></td><td>
Genera el evento <a href="e870628f-1461-b87d-8212-ba57342472c8">Reporting</a> desde una tarea.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="20544992-8780-692f-abfc-0f1072ccb255">ResetTimeout</a></td><td>
Reinicia el contador de tiempo de espera durante una tarea.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="0459d3ac-061c-3cad-190d-cfc8d727981e">SetCancelPending</a></td><td>
Establece el valor de la propiedad de sólo lectura <a href="5150ee99-224b-5586-84a9-156efb671649">CancelPending</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="0927c33b-1d86-3423-7a69-5fdc27ffbbe3">SetCurrentProgress</a></td><td>
Establece el valor de la propiedad de sólo lectura <a href="617445a4-a69a-7abb-369f-8ff1f1208d35">CurrentProgress</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="3d4a1181-cc43-923b-9699-c32f5fce7a93">SetOnDuty</a></td><td>
Establece el valor de la propiedad de sólo lectura <a href="a324065e-fa48-7965-6f25-49f7a7e138fc">OnDuty</a> desde una clase derivada.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="29a2e6f8-6405-4a7d-78f5-24c0b7a3241a">SetTimeStart</a></td><td>
Establece el valor de la propiedad de sólo lectura <a href="623b73a1-9976-d979-56ea-72b4ff136ad6">TStart</a> desde una clase derivada.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="385f2d31-aaa7-54fa-4efa-4c7cef75de61">Stop(String)</a></td><td>
Genera el evento <a href="36c5202b-56a4-851b-6afc-492e5dfc5aa7">Stopped</a> al interrumpir una tarea.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="407817c2-6566-7f4f-12e9-32b12aacf07d">Stop(ProgressEventArgs)</a></td><td>
Genera el evento <a href="36c5202b-56a4-851b-6afc-492e5dfc5aa7">Stopped</a> al interrumpir una tarea.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="ed1b5088-d89b-5d6c-1383-40f483aa6bd6">Stop(Nullable(Single), String)</a></td><td>
Genera el evento <a href="36c5202b-56a4-851b-6afc-492e5dfc5aa7">Stopped</a> al interrumpir una tarea.</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/es-es/library/7bxwbwt2" target="_blank">ToString</a></td><td> (Inherited from <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a>.)</td></tr></table>&nbsp;
<a href="#taskreporter-class">Back to Top</a>

## Events
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="0c308070-92da-dcc7-e2fd-2913c32e7f23">Begun</a></td><td>
Se genera cuando se ha iniciado una tarea.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="32f89ac9-d1ab-e39b-1209-838a4c7bba7d">CancelRequested</a></td><td>
Se genera cuando este TaskReporter ha solicifado la detención de la tarea en ejecucion.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="04f09c04-b1e9-8520-79ef-bb0f98272ef9">Ended</a></td><td>
Se genera cuando una tarea ha finalizado.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="23526f7b-7026-1852-6cad-f91ed78bc96a">Error</a></td><td>
Se genera cuando ocurre una excepción durante la ejecución de la tarea.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="e870628f-1461-b87d-8212-ba57342472c8">Reporting</a></td><td>
Se genera cuando una tarea desea reportar su estado.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="36c5202b-56a4-851b-6afc-492e5dfc5aa7">Stopped</a></td><td>
Se genera cuando una tarea ha sido cancelada.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="f16407b7-dfa4-7bff-d6b2-38a6da42d45c">TaskTimeout</a></td><td>
Se genera cuando se ha agotado el tiempo de espera establecido para ejecutar la tarea.</td></tr></table>&nbsp;
<a href="#taskreporter-class">Back to Top</a>

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
<a href="#taskreporter-class">Back to Top</a>

## See Also


#### Reference
<a href="256f3901-18cb-eeca-835c-7de778822db3">MCART.Types.TaskReporter Namespace</a><br />