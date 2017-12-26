# LabeledDoubleConverter.ConvertBack Method 
 _**\[This is preliminary documentation and is subject to change.\]**_

Revierte la conversión realizada por este objeto.

**Namespace:**&nbsp;<a href="209509be-498c-78bd-c9c1-8c3bc31f7d1f">System.Windows.Converters</a><br />**Assembly:**&nbsp;MCART (in MCART.dll) Version: 0.7.0.0

## Syntax

**C#**<br />
``` C#
public Object ConvertBack(
	Object value,
	Type targetType,
	Object parameter,
	CultureInfo culture
)
```

**VB**<br />
``` VB
Public Function ConvertBack ( 
	value As Object,
	targetType As Type,
	parameter As Object,
	culture As CultureInfo
) As Object
```


#### Parameters
&nbsp;<dl><dt>value</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">System.Object</a><br />Objeto a convertir.</dd><dt>targetType</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/42892f65" target="_blank">System.Type</a><br />Tipo del destino.</dd><dt>parameter</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">System.Object</a><br />Función opcional de transformación de valor.</dd><dt>culture</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/kx54z3k7" target="_blank">System.Globalization.CultureInfo</a><br /><a href="http://msdn2.microsoft.com/es-es/library/kx54z3k7" target="_blank">CultureInfo</a> a utilizar para la conversión.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/es-es/library/e5kfa45b" target="_blank">Object</a><br />Un <a href="http://msdn2.microsoft.com/es-es/library/643eft0t" target="_blank">Double</a> cuyo valor es el promedio del grosor establecido en el <a href="http://msdn2.microsoft.com/es-es/library/ms603275" target="_blank">Thickness</a> especificado.

#### Implements
<a href="http://msdn2.microsoft.com/es-es/library/ms590768" target="_blank">IValueConverter.ConvertBack(Object, Type, Object, CultureInfo)</a><br />

## See Also


#### Reference
<a href="9976f03d-75c2-f765-83a1-45f86bbea8a1">LabeledDoubleConverter Class</a><br /><a href="209509be-498c-78bd-c9c1-8c3bc31f7d1f">System.Windows.Converters Namespace</a><br />