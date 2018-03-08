using System;
using System.Security;
using TheXDS.MCART;
using Xunit;

namespace CoreTest.Modules
{
    /// <summary>
    ///     Contiene pruebas para la clase estática <see cref="Common" />.
    /// </summary>
    public class CommonTest
    {
        /// <summary>
        ///     Prueba del método <see cref="Common.AreAllEmpty" />
        /// </summary>
        [Fact]
        public void AreAllEmptyTest()
        {
            Assert.True(Common.AreAllEmpty(null, " ", string.Empty));
            Assert.False(Common.AreAllEmpty(null, "Test", string.Empty));
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.Listed(System.Collections.Generic.IEnumerable{string})" />
        /// </summary>
        [Fact]
        public void CollectionListedTest()
        {
            var outp = new[]
            {
                "This", "is", "a", "test"
            }.Listed();
            Assert.Equal(
                $"This{Environment.NewLine}is{Environment.NewLine}a{Environment.NewLine}test{Environment.NewLine}",
                outp);
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.Condense(System.Collections.Generic.IEnumerable{string}, string)" />
        /// </summary>
        [Fact]
        public void CondenseTest()
        {
            Assert.Equal("A B C", new[] { "A", "B", "C" }.Condense());
            Assert.Equal("A, B, C", new[] { "A", "B", "C" }.Condense(", "));
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.ContainsAny(string, char[])" />.
        /// </summary>
        [Fact]
        public void ContainsAnyTest()
        {
            Assert.True("Test".ContainsAny(out var idx, 'q', 't', 'a'));
            Assert.Equal(1, idx);
            Assert.True("Test".ContainsAny(out var idx2, "t", "a"));
            Assert.Equal(0, idx2);
            Assert.False("Test".ContainsAny(out var idx3, 'a', 'd'));
            Assert.Equal(-1, idx3);
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.CouldItBe(string, string)"/>
        /// </summary>
        [Fact]
        public void CouldItBeTest()
        {
            Assert.Equal(1.0, "César Morgan".CouldItBe("César Andrés Morgan"));
            Assert.Equal(0.0, "Gerardo Belot".CouldItBe("César Andrés Morgan"));
            Assert.InRange("Jarol Darío Rivera".CouldItBe("Harold Rivera Aguilar",0.6f), 0.55, 0.56);
            Assert.Equal(0.5, "Edith Alvarez".CouldItBe("Edith Mena"));
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.CountChars(string, char[])" />
        /// </summary>
        [Fact]
        public void CountCharsTest()
        {
            Assert.Equal(5, "This is a test".CountChars('i', ' '));
            Assert.Equal(5, "This is a test".CountChars("i "));
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.IsAnyEmpty(out int, string[])" />
        /// </summary>
        [Fact]
        public void IsAnyEmptyTest()
        {
            Assert.True(Common.IsAnyEmpty("Test", string.Empty, ""));
            Assert.False(Common.IsAnyEmpty("T", "e", "s", "t"));
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.IsBetween{T}(T, T, T)" />.
        /// </summary>
        [Fact]
        public void IsBetweenTest()
        {
            Assert.True(0.5.IsBetween(0.0, 1.0));
            Assert.True(0.IsBetween(0, 1));
            Assert.True(1.0f.IsBetween(0.0f, 1.0f));
            Assert.False(((byte)2).IsBetween((byte)0, (byte)1));
            Assert.False(((sbyte)-50).IsBetween((sbyte)0, (sbyte)1));
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.IsEmpty(string)" />
        /// </summary>
        [Fact]
        public void IsEmptyTest()
        {
            Assert.True(string.Empty.IsEmpty());
            Assert.False("Test".IsEmpty());
            Assert.True((null as string).IsEmpty());
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.Left(string, int)" />
        /// </summary>
        [Fact]
        public void LeftTest()
        {
            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = "Test".Left(5); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = "Test".Left(-1); });

            //Prueba de valores devueltos...
            Assert.Equal("Te", "Test".Left(2));
        }

        [Fact]
        public void LikenessTest()
        {
            Assert.InRange("Cesar Morgan".Likeness("César Morgan"), 0.9f, 1f);
        }

        [Fact]
        public void ReadBytesTest()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            var r = s.ReadBytes();
            Assert.True(64 == r[0] && 0 == r[1]);
        }

        [Fact]
        public void ReadInt16Test()
        {
            var s = new SecureString();
            s.AppendChar('@');
            s.MakeReadOnly();
            Assert.Equal((short)64, s.ReadInt16()[0]);
        }

        [Fact]
        public void ReadTest()
        {
            var s = new SecureString();
            s.AppendChar('T');
            s.AppendChar('e');
            s.AppendChar('s');
            s.AppendChar('t');
            s.MakeReadOnly();
            Assert.Equal("Test", s.Read());
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.Right(string, int)" />
        /// </summary>
        [Fact]
        public void RightTest()
        {
            //Pruebas de sanidad de argumentos...
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = "Test".Right(5); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = "Test".Right(-1); });

            //Prueba de valores devueltos...
            Assert.Equal("st", "Test".Right(2));
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.Swap{T}" />.
        /// </summary>
        [Fact]
        public void SwapTest()
        {
            int a = 1, b = 2;
            Common.Swap(ref a, ref b);
            Assert.Equal(2, a);
            Assert.Equal(1, b);
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.ToHex(byte[])" />
        /// </summary>
        [Fact]
        public void ToHexTest1()
        {
            Assert.Equal("F0", ((byte)240).ToHex());
        }

        /// <summary>
        ///     Prueba del método <see cref="Common.ToHex(byte)" />
        /// </summary>
        [Fact]
        public void ToHexTest2()
        {
            Assert.Equal("0A0B0C", new byte[] { 10, 11, 12 }.ToHex());
        }
    }
}