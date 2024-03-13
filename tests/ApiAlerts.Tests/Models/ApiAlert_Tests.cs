using ApiAlerts.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiAlerts.Tests.Models
{
    public class ApiAlert_Tests
    {
        private const string FiveCharacters = "AAAAA";
        private const string TenCharacters = FiveCharacters + FiveCharacters;
        private const string FiftyCharacters = TenCharacters + TenCharacters + TenCharacters + TenCharacters + TenCharacters;
        private const string HundredCharacters = FiftyCharacters + FiftyCharacters;
        private const string FiveHundredCharacters = HundredCharacters + HundredCharacters + HundredCharacters + HundredCharacters + HundredCharacters;
        private const string ThousandCharacters = FiveHundredCharacters + FiveHundredCharacters;

        private ApiAlert SubjectUnderTest { get; set; }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(FiveHundredCharacters + FiveCharacters)]
        [InlineData(FiveHundredCharacters + TenCharacters)]
        [InlineData(FiveHundredCharacters + FiftyCharacters)]
        public void ValidateMessage_WhenMessageIsInvalid(string message)
        {
            SubjectUnderTest = new ApiAlert(message);
            Assert.False(SubjectUnderTest.ValidateMessage());
        }

        [Theory]
        [InlineData("A")]
        [InlineData(FiveCharacters)]
        [InlineData(TenCharacters)]
        [InlineData(FiftyCharacters)]
        [InlineData(HundredCharacters)]
        [InlineData(FiveHundredCharacters)]
        public void ValidateMessage_WhenMessageIsValid(string message)
        {
            SubjectUnderTest = new ApiAlert(message);
            Assert.True(SubjectUnderTest.ValidateMessage());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(ThousandCharacters + FiveCharacters)]
        [InlineData(ThousandCharacters + TenCharacters)]
        [InlineData(ThousandCharacters + FiftyCharacters)]
        public void ValidateLink_WhenLinkIsInvalid(string link)
        {
            SubjectUnderTest = new ApiAlert(FiveHundredCharacters, link);
            Assert.False(SubjectUnderTest.ValidateLink());
        }

        [Theory]
        [InlineData("A")]
        [InlineData(FiveCharacters)]
        [InlineData(TenCharacters)]
        [InlineData(FiftyCharacters)]
        [InlineData(HundredCharacters)]
        [InlineData(FiveHundredCharacters)]
        [InlineData(ThousandCharacters)]
        public void ValidateLink_WhenLinkIsValid(string link)
        {
            SubjectUnderTest = new ApiAlert(FiveHundredCharacters, link);
            Assert.True(SubjectUnderTest.ValidateLink());
        }

        [Fact]
        public void ValidateTags_WhenTagsIsNull()
        {
            SubjectUnderTest = new ApiAlert(FiveHundredCharacters, null, FiveHundredCharacters);
            Assert.False(SubjectUnderTest.ValidateTags());
        }

        [Fact]
        public void ValidateTags_WhenTagsIsEmpty()
        {
            SubjectUnderTest = new ApiAlert(FiveHundredCharacters, new List<string>(), FiveHundredCharacters);
            Assert.False(SubjectUnderTest.ValidateTags());
        }

        [Fact]
        public void ValidateTags_WhenTagsIsAboveMaximum()
        {
            var tags = new List<string>() { TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters, TenCharacters };

            SubjectUnderTest = new ApiAlert(FiveHundredCharacters, tags, FiveHundredCharacters);
            Assert.False(SubjectUnderTest.ValidateTags());
        }

        [Theory]
        [InlineData(HundredCharacters)]
        [InlineData(FiveHundredCharacters)]
        [InlineData(ThousandCharacters)]
        public void ValidateTags_WhenTagsIsInValidAmountButInvalid(string tag)
        {
            var tags = new List<string>() { tag };
            SubjectUnderTest = new ApiAlert(FiveHundredCharacters, tags, FiveHundredCharacters);
            Assert.False(SubjectUnderTest.ValidateTags());
        }

        [Theory]
        [InlineData(FiveCharacters)]
        [InlineData(TenCharacters)]
        [InlineData(FiftyCharacters)]
        public void ValidateTags_WhenTagsIsValidAmountButInvalid(string tag)
        {
            var tags = new List<string>() { tag };
            SubjectUnderTest = new ApiAlert(FiveHundredCharacters, tags, FiveHundredCharacters);
            Assert.True(SubjectUnderTest.ValidateTags());
        }
    }
}
