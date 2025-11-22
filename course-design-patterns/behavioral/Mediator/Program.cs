using Mediator;

ChatRoom chatRoom = new();

RegularUser alice = new("Alice");
RegularUser bob = new("Bob");
AdminUser charlie = new("Charlie");

chatRoom.AddUser(alice);
chatRoom.AddUser(bob);
chatRoom.AddUser(charlie);

charlie.Send("Welcome, everyone!");

alice.Send("Hello everyone!");
bob.Send("Hi Alice, how are you?");
alice.Send("I'm fine, thanks!");

charlie.BroadcastAlert("Keep it friendly guys!");
