using bayonet.Core.Common;
using bayonet.Core.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace bayonet.Core.Tests.Common
{
    public class BayonetHelperFixture
    {
        private string typeString;
        private string id;
        private int count;

        private readonly Faker faker;

        public BayonetHelperFixture()
        {
            this.faker = new Faker();
        }

        public BayonetHelperFixture WithValidTypeString(string typeString = null)
        {
            var types = Enum.GetNames(typeof(StoryType));
            this.typeString = (String.IsNullOrEmpty(typeString) || String.IsNullOrWhiteSpace(typeString)) ? types[this.faker.Random.Int(0, types.Length - 1)] : typeString;
            return this;
        }

        public BayonetHelperFixture WithInvalidTypeString()
        {
            this.typeString = this.faker.Lorem.Word();
            return this;
        }

        public BayonetHelperFixture WithValidId()
        {
            this.id = this.faker.Lorem.Word();
            return this;
        }

        public BayonetHelperFixture WithInvalidId()
        {
            string[] badData = new string[] { "", " ", null };
            this.id = badData[this.faker.Random.Int(0, 2)];
            return this;
        }

        public BayonetHelperFixture WithValidCount()
        {
            this.count = this.faker.Random.Int(1, 10);
            return this;
        }

        public BayonetHelperFixture WithInvalidCount()
        {
            int[] badData = new int[] { 0, -1 };
            this.count = badData[this.faker.Random.Int(0, 1)];
            return this;
        }

        public string ExecuteStoryMethodUnderTestString()
        {
            return BayonetHelper.FormatStoryType(this.typeString);
        }

        public bool ExecuteStoryMethodUnderTestBool()
        {
            return BayonetHelper.ValidateStoryType(this.typeString);
        }

        public bool ExecuteIdMethodUnderTestBool()
        {
            return BayonetHelper.ValidateId(this.id);
        }

        public bool ExecuteCountMethodUnderTestBool()
        {
            return BayonetHelper.ValidateCount(this.count);
        }
    }
}
