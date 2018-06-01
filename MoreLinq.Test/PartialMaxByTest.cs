namespace MoreLinq.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PartialMaxByTest
    {
        [Test]
        public void IsLazy()
        {
            new BreakingSequence<int>().PartialMaxBy(Extremity.First, 42, BreakingFunc.Of<int, int>());
        }

        [Test]
        public void WithNegativeLimit()
        {
            AssertThrowsArgument.OutOfRangeException("limit", () =>
                new BreakingSequence<int>().PartialMaxBy(Extremity.First, -1, BreakingFunc.Of<int, int>()));
        }

        [Test]
        public void WithUndefinedExtremity()
        {
            AssertThrowsArgument.OutOfRangeException("extremity", () =>
                new BreakingSequence<int>().PartialMaxBy((Extremity) 42, 42, BreakingFunc.Of<int, int>()));
        }

        [TestCase(Extremity.First, 0, ExpectedResult = new string[0]             )]
        [TestCase(Extremity.Last , 0, ExpectedResult = new string[0]             )]
        [TestCase(Extremity.First, 1, ExpectedResult = new[] { "hello"          })]
        [TestCase(Extremity.First, 2, ExpectedResult = new[] { "hello", "world" })]
        [TestCase(Extremity.First, 3, ExpectedResult = new[] { "hello", "world" })]
        [TestCase(Extremity.Last , 1, ExpectedResult = new[] { "world"          })]
        [TestCase(Extremity.Last , 2, ExpectedResult = new[] { "hello", "world" })]
        [TestCase(Extremity.Last , 3, ExpectedResult = new[] { "hello", "world" })]
        public string[] ReturnsMaxima(Extremity extremity, int limit)
        {
            using (var strings = SampleData.Strings.AsTestingSequence())
            {
                return strings.PartialMaxBy(extremity, limit, s => s.Length)
                              .ToArray();
            }
        }

        [TestCase(Extremity.First, 10)]
        [TestCase(Extremity.Last , 10)]
        public void WithNullComparer(Extremity extremity, int limit)
        {
            using (var strings = SampleData.Strings.AsTestingSequence())
            {
                var result = strings.PartialMaxBy(extremity, limit, x => x.Length, null);
                Assert.That(result, Is.EqualTo(SampleData.Strings.PartialMaxBy(extremity, limit, x => x.Length)));
            }
        }

        [TestCase(Extremity.First, 10)]
        [TestCase(Extremity.Last , 10)]
        public void WithEmptySequence(Extremity extremity, int limit)
        {
            Assert.That(new string[0].PartialMaxBy(extremity, limit, x => x.Length), Is.Empty);
        }

        [TestCase(Extremity.First, 0, 0, ExpectedResult = new string[0])]
        [TestCase(Extremity.Last , 0, 0, ExpectedResult = new string[0])]
        [TestCase(Extremity.First, 3, 1, ExpectedResult = new[] { "aa" })]
        [TestCase(Extremity.Last , 3, 1, ExpectedResult = new[] { "aa" })]
        [TestCase(Extremity.First, 1, 0, ExpectedResult = new[] { "ax"                         })]
        [TestCase(Extremity.First, 2, 0, ExpectedResult = new[] { "ax", "aa"                   })]
        [TestCase(Extremity.First, 3, 0, ExpectedResult = new[] { "ax", "aa", "ab"             })]
        [TestCase(Extremity.First, 4, 0, ExpectedResult = new[] { "ax", "aa", "ab", "ay"       })]
        [TestCase(Extremity.First, 5, 0, ExpectedResult = new[] { "ax", "aa", "ab", "ay", "az" })]
        [TestCase(Extremity.First, 6, 0, ExpectedResult = new[] { "ax", "aa", "ab", "ay", "az" })]
        [TestCase(Extremity.Last , 1, 0, ExpectedResult = new[] { "az"                         })]
        [TestCase(Extremity.Last , 2, 0, ExpectedResult = new[] { "ay", "az"                   })]
        [TestCase(Extremity.Last , 3, 0, ExpectedResult = new[] { "ab", "ay", "az"             })]
        [TestCase(Extremity.Last , 4, 0, ExpectedResult = new[] { "aa", "ab", "ay", "az"       })]
        [TestCase(Extremity.Last , 5, 0, ExpectedResult = new[] { "ax", "aa", "ab", "ay", "az" })]
        [TestCase(Extremity.Last , 6, 0, ExpectedResult = new[] { "ax", "aa", "ab", "ay", "az" })]
        public string[] WithComparerReturnsMaxima(Extremity extremity, int limit, int index)
        {
            using (var strings = SampleData.Strings.AsTestingSequence())
            {
                return strings.PartialMaxBy(extremity, limit, s => s[index], SampleData.ReverseCharComparer)
                              .ToArray();
            }
        }
    }
}
