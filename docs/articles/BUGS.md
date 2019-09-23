# Información de errores conocidos
En la siguiente sección se describen los errores conocidos (y en algunos casos,
un workaround para los mismos) que existen dentro de MCART. Cuando el proyecto
finalmente vea un release, este archivo será eliminado, y se utilizará el
sistema de control de problemas existente en el servicio Git en donde el
proyecto se encuentre alojado; excepto si el servicio no incluye tal
característica.

La decisión de crear este archivo obedece a cuatro factores:
1) Actualmente, no existen contribuyentes a parte de su creador
2) Mientras el proyecto está en fase pre-alpha, no vale la pena congestionar el
   repositorio con tickets de problemas.
3) Los problemas descritos aquí podrían llegar a permanecer a largo plazo
   durante el desarrollo de MCART.
4) Hay una enorme cantidad de *quirks*, lo cual generaría muchos tickets.

Por lo general, únicamente los programadores de este proyecto aportarán
información de problemas conocidos a través de este archivo, ya que la
plataforma de tickets tiene como propósito brindarle esa posibilidad a
cualquier persona que no esté involucrada en este proyecto a un nivel tan
profundo.

Los elementos contenidos en este arhivo seguirán el siguiente formato estándar
en Markdown:

``` md
### (Título del fallo conocido)
Desde: MCART (versión)
Tipo: (Tipo de fallo)
######
#### Problema:
(Descripción corta del problema)
#### Detalles técnicos (en caso de existir):
(Descripción técnica del problema)
#### Workaround (en caso de existir):
(Descripción del workaround)
#### Comentarios(en caso de existir):
(Comentarios sobre el problema)
```

# Errores conocidos

### ```RingGraph``` no funciona correctamente.
Desde: MCART 0.8 series  
Tipo: Fallo de fiabilidad/estabilidad

#### Problema:
El control de gráfico de anillos, ```RingGraph```, no funciona más allá de lo
que la aplicación de demostración permite observar.

#### Detalles técnicos:
Hay enormes problemas con las rutinas de dibujado y de escala de los
sub-elementos. El refresco parece no funcionar tampoco, y no es posible agregar
más de 3 o 4 puntos de datos a la gráfica sin que WPF enloquezca y cause un
*crash*.

#### Comentarios:
Todo el subsistema de gráficos está pasando por un proceso de análisis de
viabilidad, y es posible que sea removido totalmente de MCART en un futuro.
Todos estos elementos posiblemente encuentren un nuevo hogar en un proyecto
totalmente independiente, que se centre en este tipo de controles para WPF.

Si este fuera el caso, cabrá la posibilidad que regresen los proyectos de MCART
para GTK# y Win32 para la serie 0.12 del Runtime, especialmente ahora que para
la serie 0.9 se pretende mudar todo el código de cuadros de diálogo y de UI en
general al esquema MVVM. 