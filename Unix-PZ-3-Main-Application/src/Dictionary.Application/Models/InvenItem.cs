using System.ComponentModel.DataAnnotations.Schema;

namespace Dictionary.Application.Models;

public class InvenItem
{
    [Column("inven_item_id")]
    public Guid InvenItemId { get; set; }
    
    [Column("last_mod_date")]
    public DateTime LastModDateTime { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [Column("plu")]
    public int Plu { get; set; }
    
    [Column("eid")]
    public string Eid { get; set; } = "";
    
    [Column("vendor")]
    public string Vendor { get; set; } = "";
}