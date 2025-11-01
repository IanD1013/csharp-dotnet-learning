using Destructurama.Attributed;

namespace SerilogConsoleApp;

public class Payment
{
    public int PaymentId { get; set; }
    
    // [LogMasked]
    // [LogMasked(Text = "_MASKED_")]
    // [LogMasked(PreserveLength = true)]
    // [NotLogged]
    [LogMasked(ShowFirst = 3, PreserveLength = true)]
    public string Email { get; set; }
    
    public Guid UserId { get; set; }
    public DateTime OccuredAt { get; set; }
}