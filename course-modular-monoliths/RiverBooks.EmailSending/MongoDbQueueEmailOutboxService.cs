using MongoDB.Driver;

namespace RiverBooks.EmailSending;

internal class MongoDbQueueEmailOutboxService : IQueueEmailsInOutboxService
{
    private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

    public MongoDbQueueEmailOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection)
    {
        _emailCollection = emailCollection;
    }

    public async Task QueueEmailForSending(EmailOutboxEntity entity)
    {
        await _emailCollection.InsertOneAsync(entity);
    }
}