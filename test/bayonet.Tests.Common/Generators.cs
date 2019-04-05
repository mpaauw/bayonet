using bayonet.Core.Models;
using Bogus;
using System.Collections.Generic;

namespace bayonet.Tests.Common
{
    public static class Generators
    {
        public static IList<Item> FakeItems(int count = -1)
        {
            Faker faker = new Faker();
            IList<Item> fakeItems = new List<Item>();
            count = (count == -1) ? faker.Random.Int(1, 10) : count;
            for(int i = 0; i < count; i++)
            {
                fakeItems.Add(FakeItem());
            }
            return fakeItems;
        }

        public static Faker<Item> FakeItem(string id = null)
        {
            return new Faker<Item>()
                .RuleFor(t => t.Id, f => (id is null) ? f.Lorem.Word() : id)
                .RuleFor(t => t.Deleted, f => f.Random.Bool())
                .RuleFor(t => t.Type, ItemType.Story)
                .RuleFor(t => t.By, f => f.Lorem.Word())
                .RuleFor(t => t.Time, f => f.Random.Int())
                .RuleFor(t => t.Text, f => f.Hacker.Phrase())
                .RuleFor(t => t.Dead, f => f.Random.Bool())
                .RuleFor(t => t.Parent, f => f.Random.Int())
                .RuleFor(t => t.Poll, f => f.Random.Int())
                .RuleFor(t => t.Kids, f => f.Lorem.Words())
                .RuleFor(t => t.Url, f => f.Internet.Url())
                .RuleFor(t => t.Score, f => f.Random.Int())
                .RuleFor(t => t.Parts, f => f.Lorem.Words())
                .RuleFor(t => t.Descendants, f => f.Random.Int());
        }

        public static Faker<Updates> FakeUpdates()
        {
            return new Faker<Updates>()
                .RuleFor(t => t.Items, f => f.Lorem.Words())
                .RuleFor(t => t.Profiles, f => f.Lorem.Words());
        }

        public static Faker<User> FakeUser(string id = null)
        {
            return new Faker<User>()
                .RuleFor(t => t.Id, f => (id is null) ? f.Lorem.Word() : id)
                .RuleFor(t => t.Delay, f => f.Random.Int())
                .RuleFor(t => t.Created, f => f.Random.Int())
                .RuleFor(t => t.Karma, f => f.Random.Int())
                .RuleFor(t => t.About, f => f.Hacker.Phrase())
                .RuleFor(t => t.Submitted, f => f.Lorem.Words());
        }
    }
}
