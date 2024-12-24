# Technical Development Information
This file details some technical considerations when working on MCART. For an efficient workflow, please read this entire document.

### Solution Directory Structure
The solution contains an organized directory structure for the type of project, according to its support scope. For example, for libraries supporting a specific UI platform, there is the `Platforms` folder, where the `WPF` project, *MCART for WPF*, currently exists.

The following table lists the currently defined directories:

Directory          | Directory Contents
---                 | ---
./Art               | Art resource files. Contains icons and images on MCART.
./Build             | Compiled and packaged versions of distributable libraries. It will be generated when compiling MCART.
./docs              | Standard documentation, and DocFX project to generate API documentation.
./src/Extras        | Special extensions with very specific functionality and/or the possibility of external references.
./src/Lib           | Generic auxiliary libraries of MCART for creating assemblies for different platforms.
./src/Lib/MCART     | Main project.
./src/Platform      | UI platform projects.
./src/Shared        | Shared code projects.
./src/Targets       | Build configuration files.
./src/Tests         | Unit and integration tests.
./vs-snippets       | Code snippets to install in Visual Studio.

### Version Numbers
MCART follows the NuGet version number standards, using a version string *`Major.Minor.Revision.Build`*, appending a suffix for pre-release versions.

### Global Build Constants
The file `./src/Targets/GlobalDirectives.targets` contains a set of build constants defined for the entire solution. This is to avoid issues that may arise from individually configuring each project and to efficiently share these constants.

The following table includes the existing constants and the effect they produce when activated:

Constant              | Effect
---                   | ---
AntiFreeze            | Enables functions that allow it to eventually stop a potentially infinite enumeration when it has been misused.
BufferedIO            | Some input/output functions include an optional buffered implementation. Activating this option enables buffered reads and writes.
CheckDanger           | Forces functions that allow it to limit the use of dangerous classes or functions (marked with the `DangerousAttribute`).
CLSCompliance         | Requires the use of implementations that comply with CLS (Common Language Standard). It is highly recommended to enable this constant.
DynamicLoading        | Enables the loading of classes and/or class members through `System.Reflection` of elements contained within MCART.
EnforceContracts      | Includes contract checking functions (argument sanity) in methods that support it. It is highly recommended to enable this constant, with the option to disable it for better performance if the user implementation ensures argument sanity.
ExtrasBuiltIn         | Includes in the MCART assemblies basic examples and standard implementations of interfaces or abstract classes for which they can be provided.
FloatDoubleSpecial    | `float` and `double` are numeric types that can contain special values, such as NaN or infinity. Activating this constant enables special methods that can work with these values.
ForceNoCls            | Some projects can't be made to properly conform to CLS because of their extensive use of non-CLS compliant code. You should enable this flag only at the project level, and only in cases where an important dependency is not and can't be made CLS compliant.
McartAsPlugin         | Enables the loading of plugins defined within MCART.
NativeNumbers         | Allows the use of culture-aware implementations for some functions that work with numeric symbols, using local numeric digits.
PreferExceptions      | When certain functions need to handle invalid information, activating this constant causes exceptions to be thrown instead of continuing with alternative code (activating this flag can be a headache, but results in safer code).
RatherDRY             | Indicates that, despite reducing code optimization, the DRY (Don't Repeat Yourself) principle should be strictly respected when implementing overloads whose body is exactly the same except for the argument types.
SaferPasswords        | Allows some security functions to require, check, or generate more secure passwords, at the expense of compatibility with available input methods or with the system.
StrictMCARTVersioning | When compatibility checks with MCART are performed, activating this constant causes those checks to be stricter. It is recommended to keep this constant enabled.
