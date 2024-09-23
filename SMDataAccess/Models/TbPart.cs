namespace SMDataAccess.Models;
public class TbPart
{
    public int PartId { get; set; }
    public string PartName { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public string? Notes { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsDeleted { get; set; }
    public List<TbInventory>? Inventory { get; set; }
}
