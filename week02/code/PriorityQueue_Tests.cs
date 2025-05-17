using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: I added just one item to the queue and then took it out to see if it works
    // Expected Result: I should get back exactly what I put in
    // Defect(s) Found: The basic operation worked fine - this simple test passed without issues
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First item", 5);
        
        string result = priorityQueue.Dequeue();
        Assert.AreEqual("First item", result);
    }

    [TestMethod]
    // Scenario: I put in items with different priority levels to see if it correctly picks the highest one
    // Expected Result: It should take out the item with the highest priority number first
    // Defect(s) Found: I found that the loop in Dequeue() only went up to _queue.Count - 1 instead of _queue.Count,
    // which meant it completely skipped checking the last item! If the highest priority item was last, it wouldn't be found.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low priority", 1);
        priorityQueue.Enqueue("Medium priority", 5);
        priorityQueue.Enqueue("High priority", 10);
        
        string result = priorityQueue.Dequeue();
        Assert.AreEqual("High priority", result);
    }

    [TestMethod]
    // Scenario: I added multiple items with the same priority to make sure it handles them in the right order
    // Expected Result: If multiple items have the same priority, it should take out the first one I added
    // Defect(s) Found: I discovered it was using >= instead of > when comparing priorities, which made it 
    // prefer the last item with the same priority instead of the first one. This broke the FIFO behavior.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First high priority", 10);
        priorityQueue.Enqueue("Second high priority", 10);
        
        string result = priorityQueue.Dequeue();
        Assert.AreEqual("First high priority", result);
    }

    [TestMethod]
    // Scenario: I tried taking an item from an empty queue to see how it handles errors
    // Expected Result: It should throw an exception telling me the queue is empty
    // Defect(s) Found: After looking at the code carefully, I noticed it was finding the right item to return
    // but never actually removing it from the queue! It was missing the _queue.RemoveAt() call.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should have thrown an exception for empty queue");
        }
        catch (InvalidOperationException)
        {
            // This is expected
        }
    }
}