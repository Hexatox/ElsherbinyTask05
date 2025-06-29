namespace ElsherbinyTask05.API.Contracts.Req;

public class BookAddReqDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty; 

    public DateTime PublishedDate { get; set; }
}
