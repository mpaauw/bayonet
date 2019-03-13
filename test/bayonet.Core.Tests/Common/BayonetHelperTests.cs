using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace bayonet.Core.Tests.Common
{
    public class BayonetHelperTests
    {
        private readonly BayonetHelperFixture fixture;
        private readonly Faker faker;

        public BayonetHelperTests()
        {
            this.fixture = new BayonetHelperFixture();
            this.faker = new Faker();
        }

        [Fact]
        public void Formatting_Camel_Case_StoryType_Should_ToLower_String()
        {
            string typeString = "Camel";
            var result = this.fixture
                .WithValidTypeString(typeString)
                .ExecuteStoryMethodUnderTestString();
            Assert.Equal(typeString.ToLower(), result);
        }

        [Fact]
        public void Formatting_Upper_Case_StoryType_Should_ToLower_String()
        {
            string typeString = "UPPER";
            var result = this.fixture
                .WithValidTypeString(typeString)
                .ExecuteStoryMethodUnderTestString();
            Assert.Equal(typeString.ToLower(), result);
        }

        [Fact]
        public void Formatting_Lower_Case_StoryType_Should_Have_No_Change()
        {
            string typeString = "lower";
            var result = this.fixture
                .WithValidTypeString(typeString)
                .ExecuteStoryMethodUnderTestString();
            Assert.Equal(typeString.ToLower(), result);
        }

        [Fact]
        public void Invalid_Story_Type_Should_Return_False()
        {
            var result = this.fixture
                .WithInvalidTypeString()
                .ExecuteStoryMethodUnderTestBool();
            Assert.False(result);
        }

        [Fact]
        public void Valid_Story_Type_Should_Return_True()
        {
            var result = this.fixture
                .WithValidTypeString()
                .ExecuteStoryMethodUnderTestBool();
            Assert.True(result);
        }

        [Fact]
        public void Invalid_Id_Should_Return_False()
        {
            var result = this.fixture
                .WithInvalidId()
                .ExecuteIdMethodUnderTestBool();
            Assert.False(result);
        }

        [Fact]
        public void Valid_Id_Should_Return_True()
        {
            var result = this.fixture
                .WithValidId()
                .ExecuteIdMethodUnderTestBool();
            Assert.True(result);
        }

        [Fact]
        public void Invalid_Count_Should_Return_False()
        {
            var result = this.fixture
                .WithInvalidCount()
                .ExecuteCountMethodUnderTestBool();
            Assert.False(result);
        }

        [Fact]
        public void Valid_Count_Should_Return_True()
        {
            var result = this.fixture
                .WithValidCount()
                .ExecuteCountMethodUnderTestBool();
            Assert.True(result);
        }
    }
}
