using Chain_of_Responsibility;

SpamDetectionSystem spamDetectionSystem = new();

var email = new Email(
    Sender: "random@spam.com",
    Subject: "Nigerian Prince needs help",
    Body: "Hello, World!");

bool isSpam = spamDetectionSystem.CheckForSpam(email);

Console.WriteLine($"{email} is spam: {isSpam}");