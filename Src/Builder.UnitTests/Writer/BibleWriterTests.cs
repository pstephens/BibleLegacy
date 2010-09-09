#region Copyright Notice

/* Copyright 2009-2010 Peter Stephens

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

#endregion

using System;
using System.IO;
using Builder.UnitTests.HandMocks;
using Builder.Writer;
using NUnit.Framework;

namespace Builder.UnitTests.Writer
{
    [TestFixture]
    public class BibleWriterTests
    {
        private static IBibleWriter CreateWriterUnderTest()
        {
            return new BibleWriter();
        }

        [Test]
        public void Write_with_null_output_should_throw()
        {
            var writer = CreateWriterUnderTest();
            var bible = new BibleStub();

            Assert.Throws<ArgumentNullException>(() => writer.Write(null, bible, null));
        }

        [Test]
        public void Write_with_null_bible_should_throw()
        {
            var writer = CreateWriterUnderTest();
            var stream = new MemoryStream();

            Assert.Throws<ArgumentNullException>(() => writer.Write(stream, null, null));
        }

        [Test]
        public void Write_with_no_tables_should_write_simple_header()
        {
            var writer = CreateWriterUnderTest();
            var stream = new MemoryStream();
            var bible = new BibleStub();

            writer.Write(stream, bible, null);

            stream.Seek(0, SeekOrigin.Begin);
            var reader = new BinaryReader(stream);

            Assert.Ignore("TODO");
        }
    }
}