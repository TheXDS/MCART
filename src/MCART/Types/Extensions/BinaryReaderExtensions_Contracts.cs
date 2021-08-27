using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using TheXDS.MCART.Misc;

namespace TheXDS.MCART.Types.Extensions
{
    public static partial class BinaryReaderExtensions
    {
        [Conditional("EnforceContracts")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerNonUserCode]
        private static void ReadStruct_Contract(this BinaryReader reader)
        {
            Internals.NullCheck(reader, nameof(reader));
        }
    }
}