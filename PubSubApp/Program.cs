// See https://aka.ms/new-console-template for more information

using PubSubCore;

Console.WriteLine("--- PubSub Rigorous Test Cases ---");

// 1. Setup: Create publishers and subscribers
var pubSports = new Publisher(10, "Sports");
var pubFinance = new Publisher(20, "Finance");
var pubTech = new Publisher(30, "Tech");

var subAlice = new Subscriber(100, "Alice");
var subBob = new Subscriber(101, "Bob");
var subCharlie = new Subscriber(102, "Charlie");

Console.WriteLine("\n--- Scenario 1: Publish then Subscribe ---");
// Publisher publishes news before any subscriber is interested
pubSports.Publish("Sports News 1", "Early bird gets the worm!");
pubFinance.Publish("Finance News 1", "Pre-market analysis.");

Console.WriteLine("Publishers published news before subscriptions.");

subAlice.Subscribe(pubSports);
Console.WriteLine("Alice subscribed to Sports.");
subBob.Subscribe(pubFinance);
Console.WriteLine("Bob subscribed to Finance.");

// Verify if Alice and Bob receive the pre-published news
Console.WriteLine("\nAlice's Feed (after subscribing to pre-published Sports):");
foreach (var item in subAlice.GetFeed(5))
{
    Console.WriteLine($"- [{item.Id}] {item.Title}: {item.Body}");
}

Console.WriteLine("\nBob's Feed (after subscribing to pre-published Finance):");
foreach (var item in subBob.GetFeed(5))
{
    Console.WriteLine($"- [{item.Id}] {item.Title}: {item.Body}");
}

Console.WriteLine("\n--- Scenario 2: Interleaved Publish, Subscribe, Unsubscribe ---");

// Initial subscriptions
subCharlie.Subscribe(pubSports);
subCharlie.Subscribe(pubTech);
Console.WriteLine("Charlie subscribed to Sports and Tech.");

// Publishers publish more news
pubSports.Publish("Sports News 2", "Mid-day game updates.");
pubTech.Publish("Tech News 1", "New software release.");
pubFinance.Publish("Finance News 2", "Mid-day market report.");

// Alice unsubscribes from Sports, then subscribes to Tech
Console.WriteLine("\nAlice unsubscribes from Sports, subscribes to Tech.");
subAlice.Unsubscribe(pubSports);
subAlice.Subscribe(pubTech);

// Bob publishes some tech news (Bob is not subscribed to Tech)
pubTech.Publish("Tech News 2", "AI ethics debate.");

// Charlie unsubscribes from Tech
Console.WriteLine("Charlie unsubscribes from Tech.");
subCharlie.Unsubscribe(pubTech);

// Publishers publish even more news
pubSports.Publish("Sports News 3", "Evening highlights.");
pubFinance.Publish("Finance News 3", "Closing bell.");
pubTech.Publish("Tech News 3", "Future of computing.");

// Bob subscribes to Tech
Console.WriteLine("Bob subscribes to Tech.");
subBob.Subscribe(pubTech);

// Final publications
pubSports.Publish("Sports News 4", "Late night scores.");
pubFinance.Publish("Finance News 4", "After-hours trading.");
pubTech.Publish("Tech News 4", "Quantum computing breakthrough.");

Console.WriteLine("\n--- Final News Feeds ---");

Console.WriteLine("\nAlice's Feed (expected: only Tech news after subscription, no Sports after unsubscribe):");
foreach (var item in subAlice.GetFeed(10))
{
    Console.WriteLine($"- [{item.Id}] {item.Title}: {item.Body}");
}

Console.WriteLine("\nBob's Feed (expected: Finance news, and Tech news after subscription):");
foreach (var item in subBob.GetFeed(10))
{
    Console.WriteLine($"- [{item.Id}] {item.Title}: {item.Body}");
}

Console.WriteLine("\nCharlie's Feed (expected: Sports news, and Tech news before unsubscribe):");
foreach (var item in subCharlie.GetFeed(10))
{
    Console.WriteLine($"- [{item.Id}] {item.Title}: {item.Body}");
}

Console.WriteLine("\n--- End of Rigorous Test Cases ---");
