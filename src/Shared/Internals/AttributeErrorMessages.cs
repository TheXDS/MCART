/*
PrivateInternals.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Released under the MIT License (MIT)
Copyright © 2011 - 2025 César Andrés Morgan

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace TheXDS.MCART.Misc;

internal static class AttributeErrorMessages
{
    public const string CallStackAccess = "The method dynamically accesses the call stack, so it is not compatible with trimming.";
    public const string ClassHeavilyUsesReflection = "The class extensively uses reflection, so it is not compatible with trimming.";
    public const string ClassCallsDynamicCode = "The class makes calls to dynamic code, so it is not compatible with trimming.";
    public const string MethodCreatesDelegates = "The method dynamically creates delegates, so it is not compatible with trimming.";
    public const string ClassScansForTypes = "Members of the class obtain a collection of types without direct references, so the class is not compatible with trimming.";
    public const string MethodScansForTypes = "The method obtains a collection of types without direct references, so it is not compatible with trimming.";
    public const string MethodGetsTypeMembersByName = "The method gets members of a type by their name, so it is not compatible with trimming.";
    public const string MethodAnalyzesTypeMembers = "The method accesses and analyzes type members dynamically, so it is not compatible with trimming.";
    public const string MethodCallsDynamicCode = "The method makes calls to dynamic code, so it is not compatible with trimming.";
    public const string MethodCreatesNewTypes = "The method dynamically creates new types, so it is not compatible with trimming.";
    public const string MethodLoadsAssemblyResources = "The method dynamically loads resources from the assembly, so it is not compatible with trimming.";
    public const string Net6Deprecation = "This class uses deprecated methods in .Net 6.";
    public const string UseLicenseUriAttributeInstead = "Use LicenseUriAttribute instead.";
}
