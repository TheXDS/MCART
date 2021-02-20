using System;
using System.Runtime.InteropServices;

namespace TheXDS.MCART.Modules
{
    /// <summary>
    /// Contiene llamadas de propósito general a la API de Microsoft Windows.
    /// </summary>
    public static class WinApi
    {
        /// <summary>
        /// Libera un objeto COM.
        /// </summary>
        /// <param name="obj">Objeto COM a liberar.</param>
        public static void ReleaseComObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
