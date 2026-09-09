using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities: "Low" (priority 1), "High" (priority 5), "Medium" (priority 3).
    // Dequeue three times.
    // Expected Result: High, Medium, Low (highest priority comes out first each time)
    // Defect(s) Found: (1) The Dequeue loop stopped one index early (used _queue.Count - 1 instead of _queue.Count), so it never checked the last item in the queue, causing the wrong item to be picked as highest priority. (2) Dequeue never actually removed the item from the list after finding it, so items stayed in the queue after being dequeued.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with the SAME highest priority: "First" (priority 5) then "Second" (priority 5).
    // Dequeue twice.
    // Expected Result: First, Second (tie goes to whichever was added first / closer to front)
    // Defect(s) Found: The comparison used >= instead of >, causing ties to incorrectly favor the later item instead of the earlier one.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None - this behaved correctly once the exception message text matched exactly.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}