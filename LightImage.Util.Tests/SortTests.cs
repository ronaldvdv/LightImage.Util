using DynamicData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace LightImage.Util.Tests
{
    [TestClass]
    public class SortTests
    {
        [TestMethod]
        public void TestRemove()
        {
            var source = new SourceList<Item>();
            var item = new Item("b");
            source.AddRange(new[] { new Item("a"), item, new Item("c") });
            source.Connect().SortBy(x => x.Name).Bind(out var sorted).Subscribe();
            source.Remove(item);
            Assert.AreEqual(2, sorted.Count);
        }

        [TestMethod]
        public void TestRemoveWithOr()
        {
            var source = new SourceList<Item>();
            var item = new Item("b");
            source.AddRange(new[] { new Item("a"), item, new Item("c") });
            var fixedList = new SourceList<Item>();
            var fixedItem = new Item("f");
            fixedList.Add(fixedItem);
            fixedList.Connect().Or(source.Connect().SortBy(x => x.Name)).Bind(out var sorted).Subscribe();
            source.Remove(item);
            Assert.AreEqual(3, sorted.Count);
            Assert.IsTrue(sorted.Contains(fixedItem));
            Assert.IsFalse(sorted.Contains(item));
        }

        private class Item : INotifyPropertyChanged
        {
            private string _name;

            public Item(string name)
            {
                _name = name;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public string Name
            {
                get => _name;
                set
                {
                    if (_name != value)
                    {
                        _name = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                    }
                }
            }
        }
    }
}