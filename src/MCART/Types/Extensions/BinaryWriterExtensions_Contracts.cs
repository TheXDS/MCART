using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Types.Extensions
{
    public static partial class BinaryWriterExtensions
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void DynamicWrite_Contract(this BinaryWriter bw, object value)
        {
            Internals.NullCheck(bw, nameof(bw));
            Internals.NullCheck(value, nameof(value));
        }
    }
}