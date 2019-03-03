using bayonet.Core.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Text;

namespace bayonet.Api.Tests
{
    public static class Generators
    {
        public static Faker<Item>[] FakeItems(int count = -1)
        {
            Faker faker = new Faker();
            count = (count == -1) ? faker.Random.Int(1, 10) : count;
            Faker<Item>[] fakeItems = new Faker<Item>[count];
            for(int i = 0; i < fakeItems.Length; i++)
            {
                fakeItems[i] = FakeItem();
            }
            return fakeItems;
        }

        public static Faker<Item> FakeItem()
        {
            return new Faker<Item>()
                .RuleFor(t => t.Id, f => f.Lorem.Word())
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
    }
}
