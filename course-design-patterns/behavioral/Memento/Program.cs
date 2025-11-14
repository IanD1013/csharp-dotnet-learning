using Memento;

Originator originator = new();
Caretaker caretaker = new();

originator.SetState("state 1");
caretaker.AddMemento(originator.CreateMemento());

originator.SetState("state 2");
caretaker.AddMemento(originator.CreateMemento());

originator.SetState("state 3");
caretaker.AddMemento(originator.CreateMemento());

originator.SetState("state 4");
caretaker.AddMemento(originator.CreateMemento());

originator.Restore(caretaker.GetMemento(index: 0));
Console.WriteLine(originator.GetState());

originator.Restore(caretaker.GetMemento(index: 2));
Console.WriteLine(originator.GetState());

originator.Restore(caretaker.GetMemento(index: 1));
Console.WriteLine(originator.GetState());

originator.Restore(caretaker.GetMemento(index: 3));
Console.WriteLine(originator.GetState());