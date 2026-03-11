using Ardalis.Result;

namespace RiverBooks.EmailSending;

internal interface IGetEmailsFromOutboxService
{
    Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity();
}