using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class TakingTurnsQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3) and
    // run until the queue is empty
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: I found that the PersonQueue class was adding people to the wrong end of the queue. 
    // Instead of using Add() to add to the back, it was using Insert(0, person) which put them at the front. 
    // Also, the GetNextPerson method was only checking for turns > 1 and not handling infinite turns correctly.
    public void TestTakingTurnsQueue_FiniteRepetition()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (5), Sue (3)
    // After running 5 times, add George with 3 turns.  Run until the queue is empty.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, George, Sue, Tim, George, Tim, George
    // Defect(s) Found: I noticed the same issues as in the first test. The queue was not maintaining 
    // proper First-In-First-Out (FIFO) order because people weren't being added to the correct end of the queue.
    // This messed up the order of turns.
    public void TestTakingTurnsQueue_AddPlayerMidway()
    {
        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", 5);
        var sue = new Person("Sue", 3);
        var george = new Person("George", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, george, sue, tim, george, tim, george];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        int i = 0;
        for (; i < 5; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        players.AddPerson("George", 3);

        while (players.Length > 0)
        {
            if (i >= expectedResult.Length)
            {
                Assert.Fail("Queue should have ran out of items by now.");
            }

            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);

            i++;
        }
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Bob (2), Tim (Forever), Sue (3)
    // Run 10 times.
    // Expected Result: Bob, Tim, Sue, Bob, Tim, Sue, Tim, Sue, Tim, Tim
    // Defect(s) Found: I discovered that the GetNextPerson method wasn't correctly handling people 
    // with infinite turns (turns = 0). It should keep them in the queue forever without changing 
    // their turns value, but it wasn't doing that.
    public void TestTakingTurnsQueue_ForeverZero()
    {
        var timTurns = 0;

        var bob = new Person("Bob", 2);
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [bob, tim, sue, bob, tim, sue, tim, sue, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(bob.Name, bob.Turns);
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns.
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Create a queue with the following people and turns: Tim (Forever), Sue (3)
    // Run 10 times.
    // Expected Result: Tim, Sue, Tim, Sue, Tim, Sue, Tim, Tim, Tim, Tim
    // Defect(s) Found: Similar to the previous test, I found that negative turns weren't being 
    // handled correctly. Negative turns should also count as infinite, but the code wasn't 
    // re-enqueueing people with negative turns.
    public void TestTakingTurnsQueue_ForeverNegative()
    {
        var timTurns = -3;
        var tim = new Person("Tim", timTurns);
        var sue = new Person("Sue", 3);

        Person[] expectedResult = [tim, sue, tim, sue, tim, sue, tim, tim, tim, tim];

        var players = new TakingTurnsQueue();
        players.AddPerson(tim.Name, tim.Turns);
        players.AddPerson(sue.Name, sue.Turns);

        for (int i = 0; i < 10; i++)
        {
            var person = players.GetNextPerson();
            Assert.AreEqual(expectedResult[i].Name, person.Name);
        }

        // Verify that the people with infinite turns really do have infinite turns.
        var infinitePerson = players.GetNextPerson();
        Assert.AreEqual(timTurns, infinitePerson.Turns, "People with infinite turns should not have their turns parameter modified to a very big number. A very big number is not infinite.");
    }

    [TestMethod]
    // Scenario: Try to get the next person from an empty queue
    // Expected Result: Exception should be thrown with appropriate error message.
    // Defect(s) Found: The error handling for empty queues worked fine. It correctly threw 
    // an exception when I tried to get a person from an empty queue.
    public void TestTakingTurnsQueue_Empty()
    {
        var players = new TakingTurnsQueue();

        try
        {
            players.GetNextPerson();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("No one in the queue.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}