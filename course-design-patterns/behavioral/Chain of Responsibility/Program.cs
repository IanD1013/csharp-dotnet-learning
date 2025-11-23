using Chain_of_Responsibility;

Handler handler1 = new ConcreteHandler1();
Handler handler2 = new ConcreteHandler2();
Handler handler3 = new ConcreteHandler3();

handler1.SetSuccessor(handler2);
handler2.SetSuccessor(handler3);

handler1.Handle("1");
handler1.Handle("2");
handler1.Handle("3");
handler1.Handle("4");
