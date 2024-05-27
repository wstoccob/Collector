using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Collector.Models;

public class Collection
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    
    [ForeignKey("User")]
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public DateOnly CreatedDate { get; set; }

    public bool CustomInteger1State { get; set; }
    public string? CustomInteger1Name { get; set; }
    public bool CustomInteger2State { get; set; }
    public string? CustomInteger2Name { get; set; }
    public bool CustomInteger3State { get; set; }
    public string? CustomInteger3Name { get; set; }
    
    public bool CustomString1State { get; set; }
    public string? CustomString1Name { get; set; }
    public bool CustomString2State { get; set; }
    public string? CustomString2Name { get; set; }
    public bool CustomString3State { get; set; }
    public string? CustomString3Name { get; set; }
    
    public bool CustomMultilineText1State { get; set; }
    public string? CustomMultilineText1Name { get; set; }
    public bool CustomMultilineText2State { get; set; }
    public string? CustomMultilineText2Name { get; set; }
    public bool CustomMultilineText3State { get; set; }
    public string? CustomMultilineText3Name { get; set; }

    public bool CustomCheckbox1State { get; set; }
    public string? CustomCheckbox1Name { get; set; }
    public bool CustomCheckbox2State { get; set; }
    public string? CustomCheckbox2Name { get; set; }
    public bool CustomCheckbox3State { get; set; }
    public string? CustomCheckbox3Name { get; set; }

    public bool CustomDate1State { get; set; }
    public string? CustomDate1Name { get; set; }
    public bool CustomDate2State { get; set; }
    public string? CustomDate2Name { get; set; }
    public bool CustomDate3State { get; set; }
    public string? CustomDate3Name { get; set; }
}