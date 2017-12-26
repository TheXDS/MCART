# SearchBox.AttachView Method 
 _**\[This is preliminary documentation and is subject to change.\]**_

Conecta un <a href="http://msdn2.microsoft.com/es-es/library/ms613466" target="_blank">BindingListCollectionView</a> para ser controlado automáticamente por este control.

**Namespace:**&nbsp;<a href="1c9d7a8e-81d4-838a-f87d-7379b253b6ce">MCART.Controls</a><br />**Assembly:**&nbsp;MCART (in MCART.dll) Version: 0.7.0.0

## Syntax

**C#**<br />
``` C#
public void AttachView(
	BindingListCollectionView cv,
	string[] searchFields = null
)
```

**VB**<br />
``` VB
Public Sub AttachView ( 
	cv As BindingListCollectionView,
	Optional searchFields As String() = Nothing
)
```


#### Parameters
&nbsp;<dl><dt>cv</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/ms613466" target="_blank">System.Windows.Data.BindingListCollectionView</a><br /><a href="http://msdn2.microsoft.com/es-es/library/ms613466" target="_blank">BindingListCollectionView</a> a controlar.</dd><dt>searchFields (Optional)</dt><dd>Type: <a href="http://msdn2.microsoft.com/es-es/library/s1wwdcbf" target="_blank">System.String</a>[]<br />Si <a href="2bb07c09-855b-f661-6568-873979e2e547">HasSearch</a> es `true`, establece los campos necesarios para realizar búsquedas.</dd></dl>

## See Also


#### Reference
<a href="21638864-43a2-3a2d-f2c7-4a82a05540ba">SearchBox Class</a><br /><a href="1c9d7a8e-81d4-838a-f87d-7379b253b6ce">MCART.Controls Namespace</a><br />