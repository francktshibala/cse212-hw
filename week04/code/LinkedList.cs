using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    // Problem 1: InsertTail
    public void InsertTail(int value)
    {
        // Make a new box to hold our number
        Node newNode = new(value);
        // If there are no boxes yet, this is the first and last box
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If there are already boxes, add this one to the end
        else
        {
            newNode.Prev = _tail; // New box points back to the old last box
            _tail.Next = newNode; // Old last box points forward to new box
            _tail = newNode; // Now the new box is the last box
        }
    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    // Problem 2: RemoveTail
    public void RemoveTail()
    {
        // If there's only one box (or no boxes), remove everything
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If there are multiple boxes, remove just the last one
        else if (_tail is not null)
        {
            _tail.Prev!.Next = null; // Tell the second-to-last box it's now the last
            _tail = _tail.Prev; // Make the second-to-last box our new last box
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    // Problem 3: Remove
    public void Remove(int value)
    {
        // Look through all the boxes starting from the first one
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If this is the only box, remove everything
                if (curr == _head && curr == _tail)
                {
                    _head = null;
                    _tail = null;
                }
                // If this is the first box, use our RemoveHead helper
                else if (curr == _head)
                {
                    RemoveHead();
                }
                // If this is the last box, use our RemoveTail helper
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                // If this box is somewhere in the middle
                else
                {
                    curr.Prev!.Next = curr.Next; // Connect the box before to the box after
                    curr.Next!.Prev = curr.Prev; // Connect the box after to the box before
                }
                return; // Stop looking once we find and remove the first match
            }
            curr = curr.Next; // Move to the next box
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    // Problem 4: Replace
    public void Replace(int oldValue, int newValue)
    {
        // Look through every single box starting from the first
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue; // Change the number in this box
            }
            curr = curr.Next; // Keep going to check every box
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    // Problem 5: Reverse Iterator
    public IEnumerable Reverse()
    {
        var curr = _tail; // Start from the last box instead of the first
        while (curr is not null)
        {
            yield return curr.Data; // Give back the number in this box
            curr = curr.Prev; // Move backwards to the previous box
        }
    }
    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}