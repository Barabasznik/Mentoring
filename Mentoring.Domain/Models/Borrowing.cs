namespace Mentoring.Domain.Models;
public class Borrowing
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
}

