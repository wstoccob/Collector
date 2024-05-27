using System.ComponentModel.DataAnnotations.Schema;

namespace Collector.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [ForeignKey("Collections")]
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }

    public DateOnly CreatedDate { get; set; }

    public int? CustomInt1 { get; set; }
    public int? CustomInt2 { get; set; }
    public int? CustomInt3 { get; set; }

    public string? CustomString1 { get; set; }
    public string? CustomString2 { get; set; }
    public string? CustomString3 { get; set; }

    public string? CustomMultilineText1 { get; set; }
    public string? CustomMultilineText2 { get; set; }
    public string? CustomMultilineText3 { get; set; }

    public bool? CustomCheckbox1 { get; set; }
    public bool? CustomCheckbox2 { get; set; }
    public bool? CustomCheckbox3 { get; set; }

    public DateOnly? CustomDate1 { get; set; }
    public DateOnly? CustomDate2 { get; set; }
    public DateOnly? CustomDate3 { get; set; }
}