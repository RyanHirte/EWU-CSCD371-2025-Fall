using System;
using System.Collections.Generic;

namespace Assignment;

// Taken from Ryan Hirte and Yoko Parks assignment 5
public class NodeCollection<T>
{
    public T Value { get; set; }

    public NodeCollection<T> Next { get; private set; }

    public NodeCollection(T value)
    {
        Value = value;
        Next = this;
    }

    public override string ToString()
    {
        return Value?.ToString() ?? String.Empty;
    }

    public void Append(T value)
    {
        // Check for duplicates before appending
        if (Exists(value))
        {
            throw new InvalidOperationException($"Value '{value}' already exists in the list.");
        }

        NodeCollection<T> newNode = new NodeCollection<T>(value);

        // Find the last node in the circular list (the one that points back to this)
        NodeCollection<T> current = this;
        while (current.Next != this)
        {
            current = current.Next;
        }

        // Insert new node between last node and this (first) node
        current.Next = newNode;
        newNode.Next = this;
    }

    public bool Exists(T value)
    {
        NodeCollection<T> current = this;

        do
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
                return true;
            }
            current = current.Next;
        } while (current != this);

        return false;
    }

    /// <summary>
    /// Removes all nodes from the list except the current node.
    /// Sets Next to point to itself, effectively isolating this node.
    /// </summary>
    /// <remarks>
    /// Garbage Collection Note:
    /// -----------------------------------------------------------------------
    /// We only need to set Next to itself because in C#, the garbage collector
    /// can detect and collect circular references. Even though removed nodes
    /// form a circular chain pointing to each other, they will be collected
    /// once there are no external references to any node in that chain.
    /// The GC uses a mark-and-sweep algorithm that traces from GC roots,
    /// so unreachable circular structures are properly collected.
    /// Therefore, we don't need to manually break the removed nodes' circular
    /// references - the GC will handle them automatically.
    /// </remarks>
    public void Clear()
    {
        // Simply set Next to this, breaking connection to other nodes
        // The removed nodes will form their own circular chain with no external references
        Next = this;

        // No need to traverse & break chain
        // The GC will collect them as they're now unreachable
    }

}
