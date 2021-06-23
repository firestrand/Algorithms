using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.DataStructures.Tests
{
    [TestClass]
    public class TrieTests
    {
        private Trie _trie;

        [TestInitialize]
        public void Setup()
        {
            _trie = new Trie();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Insert_WhenKeyNull_ThenException()
        {
            _trie.Insert(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Insert_WhenNumZero_ThenException()
        {
            _trie.Insert("key", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_WhenKeyNull_ThenException()
        {
            _trie.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_WhenKeyNotPresent_ThenException()
        {
            _trie.Delete("foo");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_WhenNumZero_ThenException()
        {
            _trie.Insert("key");
            _trie.Delete("key", -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Contains_WhenKeyNull_ThenException()
        {
            _trie.Contains(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Count_WhenKeyNull_ThenException()
        {
            _trie.Count(null);
        }

        [TestMethod]
        public void Insert_WithKey_ThenContainsKey()
        {
            var key = "foo";
            _trie.Insert(key);
            Assert.IsTrue(_trie.Contains(key));
        }

        [TestMethod]
        public void InsertAndDelete_WhenSameKey_ThenContainsFalse()
        {
            var key = "foo";
            _trie.Insert(key);
            _trie.Delete(key);
            Assert.IsFalse(_trie.Contains(key));
        }

        [TestMethod]
        public void InsertAndDelete_WhenPrefixRemovedValueRemains_ThenContainsTrue()
        {
            var key = "foo";
            _trie.Insert(key);
            _trie.Insert("foot");
            _trie.Delete(key);
            Assert.IsTrue(_trie.Contains(key));
        }

        [TestMethod]
        public void Insert_WhenSameKeyThreeTimes_ThenCountThree()
        {
            var key = "foo";
            _trie.Insert(key);
            _trie.Insert(key);
            _trie.Insert(key);
            Assert.AreEqual(3, _trie.Count(key));
        }

        [TestMethod]
        public void Insert_WhenSamePrefixThreeTimes_ThenCountThree()
        {
            _trie.Insert("fooz");
            _trie.Insert("foobat");
            _trie.Insert("foomon");
            Assert.AreEqual(3, _trie.Count("foo"));
        }

        [TestMethod]
        public void Count_WhenLastCharMismatch_ThenCountZero()
        {
            _trie.Insert("foo");
            Assert.AreEqual(0, _trie.Count("fot"));
        }

        
    }
}