#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2008 Jonathan Skeet. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace MoreLinq.Test
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class ForEachTest
    {
        [Test]
        public void ForEachWithSequence()
        {
            var results = new List<int>();
            using var source = TestingSequence.Of(1, 2, 3);
            source.ForEach(results.Add);
            results.AssertSequenceEqual(1, 2, 3);
        }

        [Test]
        public void ForEachIndexedWithSequence()
        {
            var valueResults = new List<int>();
            var indexResults = new List<int>();
            using var source = TestingSequence.Of(9, 7, 8);
            source.ForEach((x, index) => { valueResults.Add(x); indexResults.Add(index); });
            valueResults.AssertSequenceEqual(9, 7, 8);
            indexResults.AssertSequenceEqual(0, 1, 2);
        }
    }
}
