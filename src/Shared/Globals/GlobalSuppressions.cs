// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Muchos bloques try/catch suprimen intencionalmente todas las excepciones producidas, o no requieren conocer con precisión el tipo de la excepción producida.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2217:Do not mark enums with FlagsAttribute", Justification = "El atributo de bandera permite cierta utilidad adicional para funciones que soporten estos metadatos.")]
