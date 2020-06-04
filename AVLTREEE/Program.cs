using System;
using System.Collections;
using System.Collections.Generic;

namespace AVLTREEE
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree<int> intTree = new Tree<int>(20);
            intTree.Add(1);
            intTree.Add(20);
            intTree.Add(2);
            intTree.Add(19);
            intTree.Add(3);
            intTree.Add(18);
            intTree.Add(4);
            intTree.Add(17);
            intTree.Add(5);
            intTree.Add(16);
            intTree.Add(6);
            intTree.Add(15);
            intTree.Add(7);
            intTree.Add(14);
            intTree.Add(8);
            intTree.Add(13);
            try
            {
                intTree.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine($"It`s.\n\n{e.Message}:\n{e.Data}");
            }
        }

    }

    class Tree<T> where T : IComparable<T>
    {

        class Node<T>
        {

            public Node<T> Right { get; set; }
            public Node<T> Left { get; set; }
            public T Value { get; set; }

            public Node<T> Prev { get; set; }

            public int Depth { get; set; }

            public Node(T value) => Value = value;

        }

        Node<T> new_node;
        Node<T> node_reference;

        Node<T> Root;

        public int Count { get; private set; } = 0;
        public int MaxDepth { get; private set; } = 0;

        public Tree(T value)
        {
            Root = new Node<T>(value);
            Root.Depth = 0;
        }

        public void Add(T value)
        {

            new_node = new Node<T>(value);
            node_reference = Root;

            while (true)
            {

                if (node_reference.Value.CompareTo(value) < 0)
                {

                    if (node_reference.Right != null) node_reference = node_reference.Right;
                    else
                    {
                        node_reference.Right = new_node;
                        new_node.Depth = node_reference.Depth + 1;
                        new_node.Prev = node_reference;
                        Count++;
                        break;
                    }

                }
                else
                {

                    if (node_reference.Left != null) node_reference = node_reference.Left;
                    else
                    {
                        node_reference.Left = new_node;
                        new_node.Depth = node_reference.Depth + 1;
                        new_node.Prev = node_reference;
                        Count++;
                        break;
                    }

                }

            }

            if (new_node.Depth > MaxDepth) MaxDepth = new_node.Depth;

        }

        public void Clear()
        {

            Root.Right = null;
            Root.Left = null;

        }

        public T MaxValueItr()
        {

            node_reference = Root;

            while (true)
            {

                if (node_reference.Right != null) node_reference = node_reference.Right;
                else break;

            }

            return node_reference.Value;

        }
        
        public T MinValueItr()
        {

            node_reference = Root;

            while (true)
            {

                if (node_reference.Left != null) node_reference = node_reference.Left;
                else break;

            }

            return node_reference.Value;

        }
        
        public bool Contains(T value)
        {

            node_reference = Root;

            while (true)
            {

                if (node_reference.Value.CompareTo(value) > 0)
                {

                    if (node_reference.Right != null) node_reference = node_reference.Right;
                    else return false;

                }
                else if (node_reference.Value.CompareTo(value) < 0)
                {

                    if (node_reference.Left != null) node_reference = node_reference.Left;
                    else return false;

                }
                else return true;

            }

        }

        
        public void Print()
        {
            node_reference = Root;
            List<NodeInform<T>> nodes = new List<NodeInform<T>>();
            String treeToPrint = "";
            if (node_reference.Left != null)
            {
                Print(node_reference.Left, nodes);
            }
            nodes.Add(new NodeInform<T>(Root));
            if (node_reference.Right != null)
            {
                Print(node_reference.Right, nodes);
            }
            for (int i = 0; i <= MaxDepth; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (nodes[j].NodeDepth == i) treeToPrint += nodes[j].NodeValue.ToString();
                    else treeToPrint += "  ";
                }
                treeToPrint += "\n";
            }
            for (int i = 0; i < treeToPrint.Length; i++)
            {
                Console.Write(treeToPrint[i]);
            }
        }

        private void Print(Node<T> node, List<NodeInform<T>> nodes_list)
        {
            if (node.Left != null)
            {
                Print(node.Left, nodes_list);
            }
            nodes_list.Add(new NodeInform<T>(node));
            if (node.Right != null)
            {
                Print(node.Right, nodes_list);
            }
        }

        class NodeInform<T> where T : IComparable<T>
        {
            public T NodeValue { get; set; }
            public int NodeDepth { get; set; }
            public NodeInform(Node<T> node)
            {
                this.NodeValue = node.Value;
                this.NodeDepth = node.Depth;
            }
        }

    }






    class AVLTree<K, V> : IDictionary<K, V> where K : IComparable<K> where V : IComparable<V>
    {
        public class Node
        {
            public Node(K key, V value)
            {
                Key = key;
                Value = value;
            }
            public K Key { get; }
            public V Value { get; }
            public int Depth { get; set; } = 0;
            public Node Right { get; set; }
            public Node Left { get; set; }
            public Node Prev { get; set; }
        }

        Node root;

        public bool IsReadOnly { get; private set; } = false;
        public bool IsFixedSize { get; private set; } = false;

        public int Count { get; private set; } = 0;
        public int MaxDepth { get; private set; } = 0;

        public Stack<Node> EnumeratorOrder { get; private set; } = new Stack<Node>();

        public void Clear() => root = null;

        public void Add(K key, V value)
        {
            if (root == null)
            {
                root = new Node(key, value);
                Count++;
                return;
            }
            Node currentNode = root;
            Node newNode = new Node(key, value);

            while (true)
            {
                if (currentNode.Value.CompareTo(value) < 0)
                {
                    if (currentNode.Right != null)
                        currentNode = currentNode.Right;
                    else
                    {
                        currentNode.Right = newNode;
                        newNode.Prev = currentNode;
                        newNode.Depth = currentNode.Depth + 1;
                        Count++;
                        break;
                    }
                }
                else
                {
                    if (currentNode.Left != null)
                        currentNode = currentNode.Left;
                    else
                    {
                        currentNode.Left = newNode;
                        newNode.Prev = currentNode;
                        newNode.Depth = currentNode.Depth + 1;
                        Count++;
                        break;
                    }
                }
            }
            if (newNode.Depth > MaxDepth)
                MaxDepth = newNode.Depth;

            if (currentNode.Prev != null)
                Balance(currentNode.Prev);
        }

        public void Add(KeyValuePair<K, V> pair)
        {
            bool isFind = FindValue(pair.Key).Item1;
            if (!isFind)
                Add(pair.Key, pair.Value);
        }

        public bool Remove(K key)
        {
            Node currentNode = root;

            while (true)
            {
                if (currentNode.Key.CompareTo(key) < 0)
                {
                    if (currentNode.Right != null)
                        currentNode = currentNode.Right;
                    else
                        return false;
                }
                else if (currentNode.Key.CompareTo(key) > 0)
                {
                    if (currentNode.Left != null)
                        currentNode = currentNode.Left;
                    else
                        return false;
                }
                else
                {
                    if (currentNode.Right != null)
                    {
                        Node min = GetMinNode(currentNode);
                        min.Prev.Left = min.Right;
                        min.Left = currentNode.Left;
                        min.Right = currentNode.Right;
                        currentNode = min;
                        Balance(currentNode.Prev);
                    }
                    else if (currentNode.Left != null)
                    {
                        Node max = GetMaxNode(currentNode);
                        max.Prev.Right = max.Left;
                        max.Left = currentNode.Left;
                        max.Right = currentNode.Right;
                        currentNode = max;
                        Balance(currentNode.Prev);
                    }
                    else
                    {
                        Node prev = currentNode.Prev;
                        currentNode = null;
                        Balance(prev.Prev.Prev);
                    }
                    Count--;
                    return true;
                }
            }
        }

        public bool Remove(KeyValuePair<K, V> pair)
        {
            bool isFind = FindValue(pair.Key).Item1;
            if (isFind)
            {
                Remove(pair.Key);
                return true;
            }
            else
                return false;
        }

        public bool Contains(KeyValuePair<K, V> pair)
        {
            var (isFind, value) = FindValue(pair.Key);
            return isFind && value.Equals(pair.Value);
        }

        public bool ContainsKey(K key) => FindValue(key).Item1;

        private static void Balance(Node node)
        {
            switch (BalanceFactor(node))
            {
                case 2:
                    {
                        if (node.Prev.Left != null && BalanceFactor(node.Prev.Left) < 0)
                            RotateRight(node.Prev.Left);
                        RotateLeft(node);
                        break;
                    }
                case -2:
                    {
                        if (node.Prev.Right != null && BalanceFactor(node.Prev.Right) > 0)
                            RotateLeft(node.Prev.Right);
                        RotateRight(node);
                        break;
                    }
                default:
                    return;
            }
        }

        private static int BalanceFactor(Node node)
        {
            int leftDepth = 0;
            int rightDepth = 0;

            if (node.Left != null)
                GetDepth(node.Left, leftDepth);
            else
                leftDepth = 0;

            if (node.Right != null)
                GetDepth(node.Right, rightDepth);
            else
                rightDepth = 0;

            return leftDepth - rightDepth;
        }

        private static void GetDepth(Node node, int depth)
        {
            if (node.Depth > depth)
                depth = node.Depth;
            if (node.Left != null)
                GetDepth(node.Left, depth);
            if (node.Right != null)
                GetDepth(node.Right, depth);
        }

        private static void RotateLeft(Node node)
        {
            Node right = node.Right;
            node.Right = right.Left;
            right.Left = node;

            node.Depth--;
            right.Depth++;
        }

        private static void RotateRight(Node node)
        {
            Node left = node.Left;
            node.Left = left.Right;
            left.Right = node;

            node.Depth--;
            left.Depth++;
        }

        private (bool, V) FindValue(K key)
        {
            Node currentNode = root;

            while (currentNode != null)
            {
                int result = key.CompareTo(currentNode.Key);

                if (result < 0)
                    currentNode = currentNode.Left;
                else if (result > 0)
                    currentNode = currentNode.Right;
                else
                    return (true, currentNode.Value);
            }

            return (false, default(V));
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            if (EnumeratorOrder.Count == 0)
            {
                Node currentNode = root;
                GetNodeOrder(currentNode, EnumeratorOrder);
            }

            Node node = EnumeratorOrder.Pop();
            yield return new KeyValuePair<K, V>(node.Key, node.Value);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void GetNodeOrder(Node node, Stack<Node> order)
        {
            if (node.Right != null)
                GetNodeOrder(node.Right, order);
            order.Push(node);
            if (node.Left != null)
                GetNodeOrder(node.Left, order);
        }

        public V this[K key]
        {
            get
            {
                var (find, value) = FindValue(key);

                if (!find)
                    throw new KeyNotFoundException("Nema");

                return value;
            }
            set
            {
                var (isFind, _value) = FindValue(key);

                if (!isFind && !value.Equals(_value))
                    Add(key, value);
            }
        }

        ICollection<K> IDictionary<K, V>.Keys
        {
            get
            {
                var result = new List<K>();
                foreach (var iter in this)
                {
                    result.Add(iter.Key);
                }
                return result;
            }
        }

        ICollection<V> IDictionary<K, V>.Values
        {
            get
            {
                var result = new List<V>();
                foreach (var iter in this)
                {
                    result.Add(iter.Value);
                }
                return result;
            }
        }

        public bool TryGetValue(K key, out V value)
        {
            var (isFind, _value) = FindValue(key);
            value = _value;
            return isFind;
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            List<KeyValuePair<K, V>> inArray = new List<KeyValuePair<K, V>>(this);
            Array.Copy(inArray.ToArray(), arrayIndex, array, 0, array.Length - 1);
        }

        private static Node GetMinNode(Node node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        private static Node GetMaxNode(Node node)
        {
            while (node.Right != null)
                node = node.Right;
            return node;
        }
    }
 }

