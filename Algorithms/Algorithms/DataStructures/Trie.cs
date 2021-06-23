using System;
using System.Collections.Generic;

namespace Algorithms.DataStructures
{
    public class Trie
    {
        private sealed class Node
        {
            private readonly char _character;
            public int Count { get; set; }
            public bool IsWordEnding { get; set; }
            private readonly Dictionary<char, Node> _children = new Dictionary<char, Node>();

            public Node(char character)
            {
                this._character = character;
            }

            public void AddChild(char character, Node node)
            {
                this._children.Add(character, node);
            }

            public Node GetChild(char character)
            {
                return this._children.GetValueOrDefault(character);
            }

            public void DeleteChild(char character)
            {
                this._children.Remove(character);
            }
            private bool Equals(Node other)
            {
                return _character == other._character && Equals(_children, other._children) && Count == other.Count && IsWordEnding == other.IsWordEnding;
            }

            public override bool Equals(object obj)
            {
                return ReferenceEquals(this, obj) || obj is Node other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(_character, _children);
            }
        }

        private const char RootChar = '\0';
        private readonly Node _root = new Node(RootChar);

        public void ValidateKey(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentException("key can not be null or empty", key);
            }
        }

        public void ValidateNum(int num)
        {
            if (num <= 0)
            {
                throw new ArgumentException("num must be > 0", num.ToString());
            }
        }

        public bool Insert(string key, int num = 1)
        {
            ValidateKey(key);
            ValidateNum(num);

            var node = _root;
            bool isNodeCreated = false;
            bool isPrefix = false;

            foreach (var c in key)
            {
                var nextNode = node.GetChild(c);
                if (nextNode == null)
                {
                    nextNode = new Node(c);
                    node.AddChild(c, nextNode);
                    isNodeCreated = true;
                }
                else if(nextNode.IsWordEnding)
                {
                    isPrefix = true;
                }

                nextNode.Count += num;
                node = nextNode;
            }

            if (!_root.Equals(node))
            {
                node.IsWordEnding = true;
            }
            return isPrefix || !isNodeCreated;
        }

        public bool Delete(string key, int num = 1)
        {
            if (!Contains(key))
            {
                throw new ArgumentException("key not present in Trie", key);
            }

            ValidateNum(num);

            var node = _root;
            foreach (var c in key)
            {
                var nextNode = node.GetChild(c);
                nextNode.Count -= num;

                if (nextNode.Count <= 0)
                {
                    node.DeleteChild(c);
                    return true;
                }

                node = nextNode;
            }

            return true;
        }

        public int Count(string key)
        {
            ValidateKey(key);

            var node = _root;
            foreach (var c in key)
            {
                if (node == null)
                {
                    return 0;
                }

                node = node.GetChild(c);
            }

            if (node != null)
            {
                return node.Count;
            }
            return 0;
        }

        public bool Contains(string key)
        {
            return Count(key) > 0;
        }

    }
}