# IPlugin Interface
 _**\[This is preliminary documentation and is subject to change.\]**_

Define una interfaz básica para crear Plugins administrados por MCART

**Namespace:**&nbsp;<a href="4abc7841-aae2-1ecc-94fa-a3d251746bda">MCART.PluginSupport</a><br />**Assemblies:**&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />&nbsp;&nbsp;MCART (in MCART.dll) Version: 0.7.0.0<br />

## Syntax

**C#**<br />
``` C#
public interface IPlugin
```

**VB**<br />
``` VB
Public Interface IPlugin
```

The IPlugin type exposes the following members.


## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="b511d1e8-3e26-cacf-a048-b7ae1d980b0a">Author</a></td><td>
Devuelve el autor del IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="5fd69fe5-d9c5-0c51-5a71-72c764ff1d65">Copyright</a></td><td>
Devuelve el Copyright del IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="0329aef8-801d-2271-5967-59da8d32bd22">Description</a></td><td>
Devuelve una descripción del IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="11732a64-3aff-acbd-e21f-464a01be3eca">HasInteractions</a></td><td>
Devuelve `true` si el <a href="a9773c1d-7ff5-ea9a-06bc-836b7335120f">Plugin</a> contiene interacciones</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="7c38212b-612f-e3d7-3530-e63c8e8a2438">Interfaces</a></td><td>
Devuelve la lista de interfaces que este IPlugin implementa.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="167b48f3-6a2f-95ef-b499-37c170bd6389">IsBeta</a></td><td>
Devuelve `true` si el plugin es Beta</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="d128e1f1-3277-12df-b0db-4022caa7356d">IsUnsafe</a></td><td>
Determina si el plugin es inseguro</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="72e27f12-a40c-a153-5230-8a75c3a5a87b">IsUnstable</a></td><td>
Determina si el plugin es inseguro</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="b91dddce-7cae-bfd4-06a8-6af4089febe2">License</a></td><td>
Devuelve la licencia del IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="d28f6f64-ad43-bd38-ad6d-b530ac5789d1">MinMCARTVersion</a></td><td>
Determina la versión mínima de MCART necesaria para este IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="75d3096a-38f4-e6b1-1078-df5fde0161e1">MyAssembly</a></td><td>
Devuelve la referencia al ensamblado que contiene a este IPlugin.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="fc0576c5-e97d-eda3-5e7b-25696c36ba5a">Name</a></td><td>
Devuelve el nombre del IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="7db3f295-b0fd-5b1d-f43f-b3a33977c10b">PluginInteractions</a></td><td>
Devuelve una colección de opciones de interacción.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="8a7b31e4-e7ee-7fb9-7122-d6786dc1e3e1">Reporter</a></td><td>
Obtiene o establece el objeto <a href="33635590-5f82-4893-14af-1a5de20591b5">ITaskReporter</a> asociado a este IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="f773289a-d2d8-384a-7137-58f5d120fbf6">Tag</a></td><td>
Contiene información adicional de este plugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="b1ed0363-7489-ffcb-cde8-77d8ac1fbb24">TargetMCARTVersion</a></td><td>
Determina la versión objetivo de MCART para este IPlugin</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="5993ccec-24cb-73a0-c225-2c9b00a57897">UIMenu</a></td><td>
Convierte el <a href="7db3f295-b0fd-5b1d-f43f-b3a33977c10b">PluginInteractions</a> en un <a href="http://msdn2.microsoft.com/es-es/library/ms611603" target="_blank">MenuItem</a>.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="bf0a89fd-44a7-5f62-7fb1-e3e8cd31c70a">Version</a></td><td>
Devuelve la versión del IPlugin</td></tr></table>&nbsp;
<a href="#iplugin-interface">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="515c14ee-7c71-5931-e4da-1a81f13c04b4">MinRTVersion</a></td><td>
Determina la versión mínima de MCART necesaria para este IPlugin</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="8c70b32c-f24e-611e-6587-0b7eda98d4c1">RequestUIChange</a></td><td>
Solicita a la aplicación que se actualize la interfaz de interacción del IPlugin</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="fb564643-022e-8bbf-2a2e-e31aae7335b9">TargetRTVersion</a></td><td>
Determina la versión objetivo de MCART necesaria para este IPlugin</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="712276a8-9e79-6edd-e379-f5732f61636d">UIPanel(T, PanelT)</a></td><td>
Convierte el <a href="7db3f295-b0fd-5b1d-f43f-b3a33977c10b">PluginInteractions</a> en un <a href="http://msdn2.microsoft.com/es-es/library/ms611631" target="_blank">Panel</a>.</td></tr></table>&nbsp;
<a href="#iplugin-interface">Back to Top</a>

## Events
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="24202360-8f75-be7e-6817-c02af8151613">PluginFinalized</a></td><td>
Se produce cuando un IPlugin ha sido desechado.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="57bc523a-fa44-cd11-9349-a7fe78d14dc9">PluginFinalizing</a></td><td>
Se produce cuando un IPlugin está por ser deshechado.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="2c633ce3-ce6e-4368-045b-c54b3c4429ca">PluginLoaded</a></td><td>
Se produce cuando un IPlugin ha sido cargado.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="b6592c6c-bf8a-5bd4-1825-4fcd91066822">PluginLoadFailed</a></td><td>
Se produce cuando la carga de un IPlugin ha fallado.</td></tr><tr><td>![Public event](media/pubevent.gif "Public event")</td><td><a href="43785423-5ff6-9a36-a667-3ff296860430">UIChangeRequested</a></td><td>
Se produce cuando un IPlugin solicita la actualización de su interfaz gráfica.</td></tr></table>&nbsp;
<a href="#iplugin-interface">Back to Top</a>

## See Also


#### Reference
<a href="4abc7841-aae2-1ecc-94fa-a3d251746bda">MCART.PluginSupport Namespace</a><br />