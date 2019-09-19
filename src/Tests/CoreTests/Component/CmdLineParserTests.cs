/*
CmdLineParserTests.cs

This file is part of Morgan's CLR Advanced Runtime (MCART)

Author(s):
     César Andrés Morgan <xds_xps_ivx@hotmail.com>

Copyright © 2011 - 2019 César Andrés Morgan

Morgan's CLR Advanced Runtime (MCART) is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as published
by the Free Software Foundation, either version 3 of the License, or (at your
option) any later version.

Morgan's CLR Advanced Runtime (MCART) is distributed in the hope that it will
be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <http://www.gnu.org/licenses/>.
*/

#pragma warning disable CS1591

using System.Linq;
using TheXDS.MCART.Component;
using Xunit;

namespace TheXDS.MCART.Tests.Component
{
    public class CmdLineParserTests
    {
        private class Recursive : Argument
        {
            public override string LongName => "recursive";
            public override char? ShortName => 'r';
        }
        private class Force : Argument
        {
            public override ValueKind Kind => ValueKind.Optional;
            public override string LongName => "force";
            public override char? ShortName => 'f';
            public override string Default => "yes";
        }
        private class Verbose : Argument
        {
            public override string LongName => "Verbose";
            public override char? ShortName => 'v';
        }
        private class FileSystem : Argument
        {
            public override ValueKind Kind => ValueKind.Required;
            public override string LongName => "FileSystem";
        }

        [InlineData("-rf")]
        [InlineData("-r --Force")]
        [InlineData("--recursive --Force")]
        [InlineData("--recursive -f")]
        [InlineData("-r -f")]
        [InlineData("/r /Force")]
        [InlineData("/r --Force")]
        [InlineData("-r /Force")]
        [InlineData("/recursive /Force")]
        [InlineData("/r /f")]
        [InlineData("/r -f")]
        [Theory]
        public void ReadFlagsTest(string cmdLine)
        {
            var parser = new CmdLineParser(cmdLine);

            Assert.True(parser.IsPresent<Recursive>());
            Assert.True(parser.IsPresent<Force>());
            Assert.False(parser.IsPresent<Verbose>());
        }

        [Fact]
        public void InvalidFlagTest()
        {
            var parser = new CmdLineParser("-t");
            Assert.True(parser.Invalid.Any());
        }

        [InlineData('=')]
        [InlineData(':')]
        [Theory]
        public void ReadValueTest(char separator)
        {
            var parser = new CmdLineParser($"--Filesystem{separator}FAT --force");
            Assert.True(parser.IsPresent<FileSystem>());
            Assert.Equal("FAT", parser.Value<FileSystem>());
            Assert.Equal("yes", parser.Value<Force>());

        }

        [Fact]
        public void InvalidArgsTest()
        {
            var parser = new CmdLineParser("--Filesystem --verbose=yes");
            Assert.Equal(2, parser.Invalid.Count());
        }

        [Fact]
        public void MissingArgsTest()
        {
            var parser = new CmdLineParser("--Filesystem=FAT");
            Assert.Empty(parser.Missing);
            parser = new CmdLineParser("-r -f");
            Assert.NotEmpty(parser.Missing);
        }
    }
}
